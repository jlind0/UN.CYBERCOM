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
    uint64 s_subscriptionId;
    
    uint MIN_VOTE_DURATION = 1 minutes;
    address public votingAddress;
    address public councilManagementAddress;
    mapping(address => address) membershipProposals;
    mapping(uint => address) proposals;
    address[] membershipProposalAddresses;
    uint proposalCount = 0;
    modifier isMember(string memory message) {
        CouncilManager manager = CouncilManager(councilManagementAddress);
        require(hasRole(manager.BROKER_ROLE(), msg.sender) ||
        hasRole(manager.POWER_ROLE(), msg.sender) ||
        hasRole(manager.CENTRAL_ROLE(), msg.sender) ||
        hasRole(manager.EMERGING_ROLE(), msg.sender) ||
        hasRole(manager.GENERAL_ROLE(), msg.sender) ||
        hasRole(manager.LESSER_ROLE(), msg.sender) ||
        hasRole(manager.INDUSTRY_ROLE(), msg.sender), message);
        _;
    }
    constructor(uint64 subscriptionId){
        s_subscriptionId = subscriptionId;
        CouncilManager manager = new CouncilManager(address(this));
        councilManagementAddress = address(manager);
        Voting voting = new Voting(subscriptionId, address(this), councilManagementAddress);
        votingAddress = address(voting);
    }
    function getCouncils() external view returns(MembershipManagement.Council[] memory){
        CouncilManager manager = CouncilManager(councilManagementAddress);
        return manager.getCouncils();
    }
    error GroupNotFound();
    error NationAlreadyMember();
    error OutstandingProposal();
    error LogicError();
    function submitMembershipProposal(MembershipManagement.MembershipProposalRequest memory request)
        external returns(address)
    {
        CouncilManager manager = CouncilManager(councilManagementAddress);

        if(!manager.doesCouncilGroupExist(request.groupId))
            revert GroupNotFound();
        if(manager.totalNations() == 0){
            address proposalAddress = constructMembershipProposal(request);
            MembershipProposal prop = MembershipProposal(proposalAddress);
            prop.updateStatus(MembershipManagement.ApprovalStatus.Approved);
            acceptMember(proposalAddress);
            return proposalAddress;
        }
        else if(manager.doesNationExist(request.member))
            revert NationAlreadyMember();
        else if(membershipProposals[request.member] != address(0)){
            MembershipProposal prop = MembershipProposal(membershipProposals[request.member]);
            MembershipManagement.ApprovalStatus aprStatus = prop.status();
            if(aprStatus == MembershipManagement.ApprovalStatus.Entered || aprStatus == MembershipManagement.ApprovalStatus.Pending || aprStatus == MembershipManagement.ApprovalStatus.Ready)
                revert OutstandingProposal();
            else if(aprStatus == MembershipManagement.ApprovalStatus.Approved)
                return address(prop);
            else if(aprStatus == MembershipManagement.ApprovalStatus.Rejected)
                return constructMembershipProposal(request);
        }
        else
            return constructMembershipProposal(request);
        revert LogicError();
    }
    function constructMembershipProposal(MembershipManagement.MembershipProposalRequest memory request)
        private returns(address)
    {
        if(request.duration < MIN_VOTE_DURATION){
                request.duration = MIN_VOTE_DURATION;
            }
        MembershipProposal prop = new MembershipProposal(msg.sender,
            address(this),
            votingAddress,
            councilManagementAddress, 
            ++proposalCount,
            MembershipManagement.ProposalTypes.Membership,
            request.duration,
            request.newNation,
            request.groupId
        );
        Voting v = Voting(votingAddress);
        address propAddress = address(prop);
        v.addProposal(propAddress);
        
        membershipProposals[request.newNation.id] = propAddress;
        proposals[prop.id()] = propAddress;
        membershipProposalAddresses.push(propAddress);
        return propAddress;
    }
    
    function getCouncil(bytes32 role) 
        external view returns (MembershipManagement.Council memory)
    {
        return CouncilManager(councilManagementAddress).getCouncil(role);
    }
    error NotOwner();
    function startVoting(uint proposalId) external{
        if(proposals[proposalId] == address(0))
            revert InvalidProposal();
        Proposal prop = Proposal(proposals[proposalId]);
        if(prop.owner() != msg.sender)
            revert NotOwner();
        prop.startVoting(msg.sender);
    }
    function performVote(uint proposalId, bool voteCast) isMember("Must be a member to vote") external{
        if(proposals[proposalId] == address(0))
            revert InvalidProposal();
        Proposal prop = Proposal(proposals[proposalId]);
        prop.vote(voteCast, msg.sender);
    }
    function prepareTally(uint proposalId)
        external isMember("Prepare Tally can only be called by a member"){
            Voting v = Voting(votingAddress);
            v.prepareTally(proposalId);
        }
    error InvalidProposal();
    error ProposalNotReadyForTally();
    function doCompleteVoting(uint proposalId)
        private
    {
       if(proposals[proposalId] == address(0))
            revert InvalidProposal();
        Proposal proposal = Proposal(proposals[proposalId]);
        if(proposal.status() != MembershipManagement.ApprovalStatus.Ready)
            revert ProposalNotReadyForTally();
        Voting vtg = Voting(votingAddress);
        MembershipManagement.ApprovalStatus status = vtg.tallyVotes(proposalId);
        if(status == MembershipManagement.ApprovalStatus.Approved){
            proposal.updateStatus(MembershipManagement.ApprovalStatus.Approved);
            if(proposal.proposalType() == MembershipManagement.ProposalTypes.Membership){
                acceptMember(proposals[proposalId]);
            }
        }
        else if(status == MembershipManagement.ApprovalStatus.Rejected){
            proposal.updateStatus(MembershipManagement.ApprovalStatus.Rejected);
        }
    }
    function completeVoting(uint proposalId)
        isMember("Must be member to complete voting")
        external 
    {
        doCompleteVoting(proposalId);
    }
    function getMembershipResponse(MembershipProposal prop)
        private view returns(MembershipManagement.MembershipProposalResponse memory)
    {
        return prop.getMembershipResponse();
    }
    function acceptMember(address proposalAddress) private{
        CouncilManager manager = CouncilManager(councilManagementAddress);
        (address memberId, bytes32 role) = manager.acceptNewMember(proposalAddress);
        _grantRole(role, memberId);
    }
    function getProposalVotes(uint proposalId) 
        external view returns(MembershipManagement.Vote[] memory)
    {
        if(proposals[proposalId] == address(0))
            revert();
        Proposal p = Proposal(proposals[proposalId]);
        return p.getVotes();
    }
    function getMembershipRequests(MembershipManagement.ApprovalStatus status) 
        private view returns(MembershipManagement.MembershipProposalResponse[] memory)
    {
        MembershipProposal[] memory props = new MembershipProposal[](membershipProposalAddresses.length);
            uint i = 0;
            uint j = 0;
            while(i < membershipProposalAddresses.length){
                MembershipProposal mp = MembershipProposal(membershipProposalAddresses[i]);
                if(mp.status() == status)
                {
                    props[j] = mp;
                    j++;
                }
                i++;
            }
            MembershipManagement.MembershipProposalResponse[] memory rtn = new MembershipManagement.MembershipProposalResponse[](j);
            i = 0;
            while(i < j){
                rtn[i] = getMembershipResponse(props[i]);
                i++;
            }
            return rtn;
    }
    function getEnteredMembershipRequests() external view
    returns(MembershipManagement.MembershipProposalResponse[] memory){
        return getMembershipRequests(MembershipManagement.ApprovalStatus.Entered);
    }
    function getPendingMembershipRequests()
        external view returns(MembershipManagement.MembershipProposalResponse[] memory)
    {
        return getMembershipRequests(MembershipManagement.ApprovalStatus.Pending);
    }
    function getReadyMembershipRequests()
        external view returns(MembershipManagement.MembershipProposalResponse[] memory)
    {
        return getMembershipRequests(MembershipManagement.ApprovalStatus.Ready);
    }
    function getApprovedMembershipRequests()
        external view returns(MembershipManagement.MembershipProposalResponse[] memory)
    {
        return getMembershipRequests(MembershipManagement.ApprovalStatus.Approved);
    }
    function getRejectedMembershipRequests()
        external view returns(MembershipManagement.MembershipProposalResponse[] memory)
    {
        return getMembershipRequests(MembershipManagement.ApprovalStatus.Rejected);
    }           
}