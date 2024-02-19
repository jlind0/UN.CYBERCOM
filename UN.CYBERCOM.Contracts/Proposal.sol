// SPDX-License-Identifier: MIT
pragma solidity ^0.8.7;

import "./MembershipManagement.sol";
import "./CouncilManager.sol";
import "./Document.sol";

/**
 * @title Proposal
 * @dev Abstract contract for managing proposals within a DAO.
 *      Includes functionalities for voting, updating status, and managing documents.
 */
abstract contract Proposal is DocumentsHolder {
    uint public id;
    MembershipManagement.ProposalTypes public poposalType;
    uint public duration;
    MembershipManagement.ApprovalStatus public status;
    uint public timestamp;
    bool public isProcessing = false;
    uint public randomNumber;
    bool public votingStarted = false;
    address public owner;
    address[] voters;
    mapping(address => MembershipManagement.Vote) votes;
    address daoAddress;
    address votingAddress;
    address councilManagementAddress;

    event VotingStarted(uint proposalId);
    event VotingCompleted(uint proposalId);
    event StatusUpdated(uint proposalId, MembershipManagement.ApprovalStatus newStatus);
    event VoteCasted(uint proposalId, address member, bool vote);

    modifier isFromDAOorVoting() {
        require(msg.sender == daoAddress || msg.sender == votingAddress || msg.sender == owner, "Must be called from DAO or Voting");
        _;
    }

    modifier isFromDAO() {
        require(msg.sender == daoAddress, "Must be called from DAO");
        _;
    }

    modifier isFromVoting() {
        require(msg.sender == votingAddress, "Must be called from Voting");
        _;
    }

    /**
     * @dev Constructor to initialize the proposal contract.
     * @param _owner Address of the proposal owner.
     * @param _daoAddress Address of the DAO contract.
     * @param _votingAddress Address of the Voting contract.
     * @param _councilManager Address of the CouncilManager contract.
     * @param _id Proposal identifier.
     * @param _proposalType Type of the proposal.
     * @param _duration Duration of the voting period.
     */
    constructor(
        address _owner, 
        address _daoAddress, 
        address _votingAddress, 
        address _councilManager, 
        uint _id, 
        MembershipManagement.ProposalTypes _proposalType, 
        uint _duration
    ) {
        owner = _owner;
        daoAddress = _daoAddress;
        votingAddress = _votingAddress;
        councilManagementAddress = _councilManager;
        id = _id;
        proposalType = _proposalType;
        duration = _duration < 1 minutes ? 1 minutes : _duration;
        status = MembershipManagement.ApprovalStatus.Entered;
        timestamp = block.timestamp;
    }
    
    /**
     * @dev Updates the status of the proposal.
     * @param _status The new status to be set for the proposal.
     */
    function updateStatus(MembershipManagement.ApprovalStatus _status) public isFromDAOorVoting() {
        status = _status;
        emit StatusUpdated(id, _status);
    }

    /**
     * @dev Sets a random number for the proposal, typically used in voting processes.
     * @param random The random number to be set.
     */
    function setRandomNumber(uint random) public isFromVoting() {
        randomNumber = random;
    }

    /**
     * @dev Marks the proposal as processing or not.
     * @param processing The new state of processing to be set.
     */
    function setProcessing(bool processing) public isFromVoting() {
        isProcessing = processing;
    }

    /**
     * @dev Returns the list of votes cast for the proposal.
     * @return An array of Vote structs containing the details of each vote.
     */
    function getVotes() public view returns(MembershipManagement.Vote[] memory) {
        MembershipManagement.Vote[] memory vs = new MembershipManagement.Vote[](voters.length);
        for (uint i = 0; i < voters.length; i++) {
            vs[i] = votes[voters[i]];
        }
        return vs;
    }

    /**
     * @dev Casts a vote for the proposal.
     * @param voteCasted The vote cast by the member (true for approve, false for reject).
     * @param member The address of the member casting the vote.
     */
    function vote(bool voteCasted, address member) public isFromDAO() {
        require(votingStarted, "Voting not started yet");
        require(block.timestamp <= duration, "Voting has closed");
        if (votes[member].proposalId != id) {
            voters.push(member);
        }
        votes[member] = MembershipManagement.Vote(member, voteCasted, block.timestamp, id);
        emit VoteCasted(id, member, voteCasted);
    }

    /**
     * @dev Initiates the voting process for the proposal.
     * @param sender The address initiating the voting process.
     */
    function startVoting(address sender) public isFromDAO() {
        require(sender == owner, "Only owner can start voting");
        require(!votingStarted, "Voting already started");
        votingStarted = true;
        duration += block.timestamp;
        status = MembershipManagement.ApprovalStatus.Pending;
        emit VotingStarted(id);
    }

    // Other functions such as addDocument() are unchanged
}

/**
 * @title MembershipProposal
 * @dev Contract for managing membership proposals within a DAO.
 *      Inherits from Proposal and adds specific functionality for membership management.
 */
contract MembershipProposal is Proposal {
    MembershipManagement.Nation nation;
    uint public groupId;

    /**
     * @dev Constructor for creating a new MembershipProposal.
     * @param _nation Details of the nation associated with the proposal.
     * @param _groupId Identifier of the group within the council.
     * @param _owner, _daoAddress, _votingAddress, _councilManager, _id, _proposalType, _duration Inherited from Proposal.
     */
    constructor(
        address _owner, 
        address _daoAddress, 
        address _votingAddress, 
        address _councilManager, 
        uint _id, 
        MembershipManagement.ProposalTypes _proposalType, 
        uint _duration, 
        MembershipManagement.Nation memory _nation, 
        uint _groupId
    ) 
        Proposal(_owner, _daoAddress, _votingAddress, _councilManager, _id, _proposalType, _duration)
    {
        nation = _nation;
        groupId = _groupId;
    }

    /**
     * @dev Retrieves the nation associated with the membership proposal.
     * @return The Nation struct representing the nation in the proposal.
     */
    function getNation() public view returns(MembershipManagement.Nation memory) {
        return nation;
    }

    /**
     * @dev Generates a response struct for the membership proposal.
     * @return A MembershipProposalResponse struct with proposal details.
     */
    function getMembershipResponse() public view returns(MembershipManagement.MembershipProposalResponse memory) {
        CouncilManager manager = CouncilManager(councilManagementAddress);
        return MembershipManagement.MembershipProposalResponse(
            id, 
            nation.id, 
            nation, 
            manager.getCouncilRoleForGroup(groupId), 
            groupId, 
            getVotes(), 
            duration, 
            status, 
            isProcessing, 
            votingStarted, 
            owner, 
            address(this)
        );
    }

    // Other functions and code specific to MembershipProposal are unchanged
}
