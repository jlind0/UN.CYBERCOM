// SPDX-License-Identifier: MIT
pragma solidity ^0.8.7;
import "@openzeppelin/contracts/access/AccessControl.sol";
import "@openzeppelin/contracts/utils/ReentrancyGuard.sol";
import "./Utils.sol";
import "./MembershipManagement.sol";
import "./Proposal.sol";
import "./Voting.sol";
import "./CouncilManager.sol";
import "./Membership.sol";
contract CybercomDAO is ReentrancyGuard, AccessControl{
    uint MIN_VOTE_DURATION = 1 minutes;
    MembershipManagement.ContractAddresses public contracts;
    uint64 subscriptionId;
    bool isInitialized = false;
    function getContractAddresses() public view returns(MembershipManagement.ContractAddresses memory){
        return contracts;
    }
    modifier isMember() {
        CouncilManager manager = CouncilManager(contracts.councilManagementAddress);
        bytes32[] memory roles = manager.getAllRoles();
        uint i = 0;
        bool isValid = false;
        while(i < roles.length){
            if(hasRole(roles[i], msg.sender)){
                isValid = true;
                break;
            }
            i++;
        }
        if(!isValid)
            revert Utils.AuthorizationError();
        _;
    }
    modifier isFromMembership(){
        if(msg.sender != contracts.membershipManagerAddress)
            revert Utils.AuthorizationError();
        _;
    }
    function initialize(MembershipManagement.ContractAddresses memory _contracts) external{
        if(isInitialized)
            revert Utils.AlreadyInitialized();
        contracts = _contracts;
    }
    function closeInitialization() external{
        isInitialized = true;
    } 
    constructor(uint64 _subscriptionId){
        subscriptionId = _subscriptionId;
        
    }
    function startVoting(uint proposalId) external{
        ProposalStorageManager storageManager = ProposalStorageManager(contracts.proposalStorageAddress);
        address propId = storageManager.getProposal(proposalId);
        if(propId == address(0))
            revert Utils.InvalidProposal();
        Proposal prop = Proposal(propId);
        if(prop.owner() != msg.sender)
            revert Utils.NotOwner();
        prop.startVoting(msg.sender);
        emit Utils.VoteStarted(proposalId, msg.sender);
    }
    function submitMembershipProposal(MembershipManagement.MembershipProposalRequest memory request)
        external returns(address)
    {
        MembershipManager manager = MembershipManager(contracts.membershipManagerAddress);
        request.owner = msg.sender;
         if(request.duration < MIN_VOTE_DURATION){
                request.duration = MIN_VOTE_DURATION;
            }
        return manager.submitMembershipProposal(request);
    }
    
    function submitMembershipRemovalProposal(MembershipManagement.MembershipRemovalRequest memory request)
        isMember() external returns(address)
    {
        if(request.duration < MIN_VOTE_DURATION){
                request.duration = MIN_VOTE_DURATION;
            }
        request.owner = msg.sender;
        MembershipRemovalManager manager = MembershipRemovalManager(contracts.membershipRemovalAddress);
        return manager.submitProposal(request);
    }
    function submitChangeVotingParameters(MembershipManagement.ChangeVotingParametersRequest memory request)
        isMember() external returns(address){
        if(request.duration < MIN_VOTE_DURATION){
            request.duration = MIN_VOTE_DURATION;
        }
        request.owner = msg.sender;
        VotingParametersManager manager = VotingParametersManager(contracts.votingParametersManagerAddress);
        return manager.submitProposal(request);
    }
    function performVote(uint proposalId, bool voteCast) isMember() external{
         ProposalStorageManager memberManager = ProposalStorageManager(contracts.proposalStorageAddress);
         address propId = memberManager.getProposal(proposalId);
        if(propId == address(0))
            revert Utils.InvalidProposal();
        Proposal prop = Proposal(propId);
        prop.vote(voteCast, msg.sender);
        emit Utils.VoteCast(proposalId, msg.sender, voteCast);
    }
    function prepareTally(uint proposalId)
        external isMember(){
            Voting v = Voting(contracts.votingAddress);
            v.prepareTally(proposalId);
            emit Utils.TallyPrepared(proposalId);
        }
    
    
    function completeVoting(uint proposalId)
        external isMember()
    {
        ProposalStorageManager memberManager = ProposalStorageManager(contracts.proposalStorageAddress);
        address propId = memberManager.getProposal(proposalId);
       if(propId == address(0))
            revert Utils.InvalidProposal();
        Proposal proposal = Proposal(propId);
        if(proposal.status() != MembershipManagement.ApprovalStatus.Ready)
            revert Utils.ProposalNotReadyForTally();
        Voting vtg = Voting(contracts.votingAddress);
        MembershipManagement.ApprovalStatus status = vtg.tallyVotes(proposalId);
        if(status == MembershipManagement.ApprovalStatus.Approved){
            proposal.updateStatus(MembershipManagement.ApprovalStatus.Approved);
            if(proposal.proposalType() == MembershipManagement.ProposalTypes.Membership){
                acceptMember(propId);
            }
            else if(proposal.proposalType() == MembershipManagement.ProposalTypes.MembershipRemoval){
                removeMember(propId);
            }
            else if(proposal.proposalType() == MembershipManagement.ProposalTypes.UpdateVotingParameters){
                CouncilManager councilManager = CouncilManager(contracts.councilManagementAddress);
                councilManager.updateVotingParameters(propId);
            }
        }
        else if(status == MembershipManagement.ApprovalStatus.Rejected){
            proposal.updateStatus(MembershipManagement.ApprovalStatus.Rejected);
            if(proposal.proposalType() == MembershipManagement.ProposalTypes.Membership){
                MembershipProposal mp = MembershipProposal(address(proposal));
                emit Utils.MemberRejected(mp.getNation().id);
            }
            else if(proposal.proposalType() == MembershipManagement.ProposalTypes.MembershipRemoval){
                MembershipRemovalProposal mrp = MembershipRemovalProposal(address(proposal));
                emit Utils.MemberKept(mrp.getNation().id);
            }
        }
        emit Utils.VotingCompleted(proposalId, status);
    }
    function acceptMemberExt(address proposalAddress) isFromMembership() public{
        acceptMember(proposalAddress);
    }
    function acceptMember(address proposalAddress) private{
        CouncilManager manager = CouncilManager(contracts.councilManagementAddress);
        (address memberId, bytes32 role) = manager.acceptNewMember(proposalAddress);
        _grantRole(role, memberId);
        emit Utils.MemberAccepted(memberId);
    }
    function removeMember(address proposalAddress) private{
        CouncilManager manager = CouncilManager(contracts.councilManagementAddress);
        MembershipRemovalProposal prop = MembershipRemovalProposal(proposalAddress);
        address memberId = prop.getNation().id;
        bytes32 role = manager.removeNation(memberId);
        _revokeRole(role, memberId);
        emit Utils.MemberRemoved(memberId);
    }
   
   
}

