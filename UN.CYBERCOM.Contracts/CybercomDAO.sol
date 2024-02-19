// SPDX-License-Identifier: MIT
pragma solidity ^0.8.7;

import "@openzeppelin/contracts/access/AccessControl.sol";
import "@openzeppelin/contracts/utils/ReentrancyGuard.sol";
import "@openzeppelin/contracts/utils/math/Math.sol";
import "@openzeppelin/contracts/utils/Strings.sol";
import "@chainlink/contracts/src/v0.8/interfaces/VRFCoordinatorV2Interface.sol";
import "@chainlink/contracts/src/v0.8/vrf/VRFConsumerBaseV2.sol";
import "./Utils.sol";
import "./MembershipManagement.sol";
import "./Proposal.sol";
import "./Voting.sol";
import "./CouncilManager.sol";

contract CybercomDAO is ReentrancyGuard, AccessControl{
    
    // State variables
    uint64 s_subscriptionId;
    uint private constant MIN_VOTE_DURATION = 1 minutes;
    address public votingAddress;
    address public councilManagementAddress;
    mapping(address => address) private membershipProposals;
    mapping(uint => address) private proposals;
    address[] private membershipProposalAddresses;
    uint private proposalCount = 0;

    event ProposalCreated(uint proposalId, address proposalAddress);
    event VoteStarted(uint proposalId, address startedBy);
    event VoteCast(uint proposalId, address voter, bool vote);
    event TallyPrepared(uint proposalId);
    event VotingCompleted(uint proposalId, MembershipManagement.ApprovalStatus status);
    event MemberAccepted(address memberId);

    // Custom errors for better readability and gas efficiency
    error GroupNotFound();
    error NationAlreadyMember();
    error OutstandingProposal();
    error LogicError();
    error InvalidProposal();
    error ProposalNotReadyForTally();
    error NotOwner();

    // Modifier to check if the caller is a member
    modifier isMember(string memory message) {
        CouncilManager manager = CouncilManager(councilManagementAddress);
        require(
            hasRole(manager.BROKER_ROLE(), msg.sender) ||
            hasRole(manager.POWER_ROLE(), msg.sender) ||
            hasRole(manager.CENTRAL_ROLE(), msg.sender) ||
            hasRole(manager.EMERGING_ROLE(), msg.sender) ||
            hasRole(manager.GENERAL_ROLE(), msg.sender) ||
            hasRole(manager.LESSER_ROLE(), msg.sender) ||
            hasRole(manager.INDUSTRY_ROLE(), msg.sender),
            message
        );
        _;
    }

    /**
     * @dev Constructor to initialize the DAO with necessary components.
     * @param subscriptionId The Chainlink VRF subscription ID for randomness.
     */
    constructor(uint64 subscriptionId) {
        s_subscriptionId = subscriptionId;
        CouncilManager manager = new CouncilManager(address(this));
        councilManagementAddress = address(manager);
        Voting voting = new Voting(subscriptionId, address(this), councilManagementAddress);
        votingAddress = address(voting);
    }

    /**
     * @dev Retrieves a list of all councils managed by the CouncilManager.
     * @return Array of Council structs.
     */
    function getCouncils() external view returns(MembershipManagement.Council[] memory) {
        CouncilManager manager = CouncilManager(councilManagementAddress);
        return manager.getCouncils();
    }

    /**
     * @dev Submits a proposal for membership to the DAO.
     * @param request Details of the membership proposal request.
     * @return Address of the newly created MembershipProposal contract.
     */
    function submitMembershipProposal(MembershipManagement.MembershipProposalRequest memory request)
        external returns(address) {
        CouncilManager manager = CouncilManager(councilManagementAddress);

        if(!manager.doesCouncilGroupExist(request.groupId)) {
            revert GroupNotFound();
        }

        // Auto-approve proposal if there are no nations
        if(manager.totalNations() == 0) {
            address proposalAddress = constructMembershipProposal(request);
            MembershipProposal prop = MembershipProposal(proposalAddress);
            prop.updateStatus(MembershipManagement.ApprovalStatus.Approved);
            acceptMember(proposalAddress);
            return proposalAddress;
        } else if(manager.doesNationExist(request.member)) {
            revert NationAlreadyMember();
        } else if(membershipProposals[request.member] != address(0)) {
            MembershipProposal prop = MembershipProposal(membershipProposals[request.member]);
            MembershipManagement.ApprovalStatus aprStatus = prop.status();
            if(aprStatus == MembershipManagement.ApprovalStatus.Entered || aprStatus == MembershipManagement.ApprovalStatus.Pending || aprStatus == MembershipManagement.ApprovalStatus.Ready) {
                revert OutstandingProposal();
            } else if(aprStatus == MembershipManagement.ApprovalStatus.Approved) {
                return address(prop);
            } else if(aprStatus == MembershipManagement.ApprovalStatus.Rejected) {
                return constructMembershipProposal(request);
            }
        } else {
            return constructMembershipProposal(request);
        }
        revert LogicError();
    }

    /**
     * @dev Constructs a new MembershipProposal contract.
     * @param request The membership proposal request details.
     * @return Address of the newly created MembershipProposal contract.
     */
    function constructMembershipProposal(MembershipManagement.MembershipProposalRequest memory request)
        private returns(address) {
        if(request.duration < MIN_VOTE_DURATION) {
            request.duration = MIN_VOTE_DURATION;
        }

        MembershipProposal prop = new MembershipProposal(
            msg.sender, address(this), votingAddress, councilManagementAddress, 
            ++proposalCount, MembershipManagement.ProposalTypes.Membership,
            request.duration, request.newNation, request.groupId
        );
        Voting v = Voting(votingAddress);
        address propAddress = address(prop);
        v.addProposal(propAddress);
        
        membershipProposals[request.newNation.id] = propAddress;
        proposals[prop.id()] = propAddress;
        membershipProposalAddresses.push(propAddress);
        emit ProposalCreated(proposalCount, propAddress);
        return propAddress;
    }
    
    /**
     * @dev Retrieves a specific council based on its role.
     * @param role The role associated with the council to retrieve.
     * @return Council struct corresponding to the specified role.
     */
    function getCouncil(bytes32 role) 
        external view returns (MembershipManagement.Council memory) {
        return CouncilManager(councilManagementAddress).getCouncil(role);
    }

    /**
     * @dev Starts the voting process for a specified proposal.
     * @param proposalId The ID of the proposal to start voting on.
     */
    function startVoting(uint proposalId) external {
        if(proposals[proposalId] == address(0)) {
            revert InvalidProposal();
        }
        Proposal prop = Proposal(proposals[proposalId]);
        if(prop.owner() != msg.sender) {
            revert NotOwner();
        }
        prop.startVoting(msg.sender);
        emit VoteStarted(proposalId, msg.sender); 
    }

    /**
     * @dev Casts a vote on a specific proposal by a member.
     * @param proposalId The ID of the proposal to vote on.
     * @param voteCast The vote to be cast (true for approve, false for reject).
     */
    function performVote(uint proposalId, bool voteCast) 
        isMember("Must be a member to vote") external {
        if(proposals[proposalId] == address(0)) {
            revert InvalidProposal();
        }
        Proposal prop = Proposal(proposals[proposalId]);
        prop.vote(voteCast, msg.sender);
        emit VoteCast(proposalId, msg.sender, voteCast); 
    }

    /**
     * @dev Prepares the tally for a specific proposal after the voting period has ended.
     * @param proposalId The ID of the proposal to prepare the tally for.
     */
    function prepareTally(uint proposalId)
        external isMember("Prepare Tally can only be called by a member") {
        Voting v = Voting(votingAddress);
        v.prepareTally(proposalId);
        emit TallyPrepared(proposalId);
    }

    /**
     * @dev Completes the voting process for a specific proposal.
     * This is a private function called by 'completeVoting'.
     * @param proposalId The ID of the proposal to complete voting on.
     */
    function doCompleteVoting(uint proposalId) private {
       if(proposals[proposalId] == address(0)) {
            revert InvalidProposal();
       }
       Proposal proposal = Proposal(proposals[proposalId]);
       if(proposal.status() != MembershipManagement.ApprovalStatus.Ready) {
            revert ProposalNotReadyForTally();
       }
       Voting vtg = Voting(votingAddress);
       MembershipManagement.ApprovalStatus status = vtg.tallyVotes(proposalId);
       if(status == MembershipManagement.ApprovalStatus.Approved) {
           proposal.updateStatus(MembershipManagement.ApprovalStatus.Approved);
           if(proposal.proposalType() == MembershipManagement.ProposalTypes.Membership) {
               acceptMember(proposals[proposalId]);
           }
       } else if(status == MembershipManagement.ApprovalStatus.Rejected) {
           proposal.updateStatus(MembershipManagement.ApprovalStatus.Rejected);
       }
       emit VotingCompleted(proposalId, status);
    }

    /**
     * @dev Allows a member to complete the voting process for a proposal.
     * @param proposalId The ID of the proposal to complete voting on.
     */
    function completeVoting(uint proposalId)
        isMember("Must be member to complete voting") external {
        doCompleteVoting(proposalId);
    }

    /**
     * @dev Retrieves the response for a specific MembershipProposal.
     * @param prop The MembershipProposal contract to retrieve the response from.
     * @return MembershipProposalResponse struct with the proposal details.
     */
    function getMembershipResponse(MembershipProposal prop)
        private view returns(MembershipManagement.MembershipProposalResponse memory) {
        return prop.getMembershipResponse();
    }

   /**
     * @dev Accepts a member into the DAO based on a membership proposal.
     * @param proposalAddress Address of the MembershipProposal contract.
     */
    function acceptMember(address proposalAddress) private {
        CouncilManager manager = CouncilManager(councilManagementAddress);
        (address memberId, bytes32 role) = manager.acceptNewMember(proposalAddress);
        _grantRole(role, memberId);
        emit MemberAccepted(memberId); 
    }

    /**
     * @dev Retrieves the votes for a specific proposal.
     * @param proposalId ID of the proposal to get votes for.
     * @return Array of Vote structs associated with the proposal.
     */
    function getProposalVotes(uint proposalId) 
        external view returns(MembershipManagement.Vote[] memory) {
        if (proposals[proposalId] == address(0)) {
            revert("Invalid Proposal");
        }
        Proposal p = Proposal(proposals[proposalId]);
        return p.getVotes();
    }

    /**
     * @dev Retrieves membership proposals based on their approval status.
     * @param status Approval status to filter the membership proposals.
     * @return Array of MembershipProposalResponse structs.
     */
    function getMembershipRequests(MembershipManagement.ApprovalStatus status) 
        private view returns(MembershipManagement.MembershipProposalResponse[] memory) {
        MembershipProposal[] memory props = new MembershipProposal[](membershipProposalAddresses.length);
        uint j = 0;
        for (uint i = 0; i < membershipProposalAddresses.length; i++) {
            MembershipProposal mp = MembershipProposal(membershipProposalAddresses[i]);
            if (mp.status() == status) {
                props[j] = mp;
                j++;
            }
        }

        MembershipManagement.MembershipProposalResponse[] memory rtn = new MembershipManagement.MembershipProposalResponse[](j);
        for (uint i = 0; i < j; i++) {
            rtn[i] = getMembershipResponse(props[i]);
        }
        return rtn;
    }

    /**
     * @dev Retrieves entered membership requests.
     * @return Array of MembershipProposalResponse structs for entered requests.
     */
    function getEnteredMembershipRequests() external view
    returns(MembershipManagement.MembershipProposalResponse[] memory) {
        return getMembershipRequests(MembershipManagement.ApprovalStatus.Entered);
    }

    /**
     * @dev Retrieves all membership proposals with the status 'Pending'.
     * @return Array of MembershipProposalResponse structs for pending proposals.
     */
    function getPendingMembershipRequests()
        external view returns(MembershipManagement.MembershipProposalResponse[] memory)
    {
        return getMembershipRequests(MembershipManagement.ApprovalStatus.Pending);
    }

    /**
     * @dev Retrieves all membership proposals with the status 'Ready'.
     * @return Array of MembershipProposalResponse structs for ready proposals.
     */
    function getReadyMembershipRequests()
        external view returns(MembershipManagement.MembershipProposalResponse[] memory)
    {
        return getMembershipRequests(MembershipManagement.ApprovalStatus.Ready);
    }

    /**
     * @dev Retrieves all membership proposals with the status 'Approved'.
     * @return Array of MembershipProposalResponse structs for approved proposals.
     */
    function getApprovedMembershipRequests()
        external view returns(MembershipManagement.MembershipProposalResponse[] memory)
    {
        return getMembershipRequests(MembershipManagement.ApprovalStatus.Approved);
    }

    /**
     * @dev Retrieves all membership proposals with the status 'Rejected'.
     * @return Array of MembershipProposalResponse structs for rejected proposals.
     */
    function getRejectedMembershipRequests()
        external view returns(MembershipManagement.MembershipProposalResponse[] memory)
    {
        return getMembershipRequests(MembershipManagement.ApprovalStatus.Rejected);
    }
}
