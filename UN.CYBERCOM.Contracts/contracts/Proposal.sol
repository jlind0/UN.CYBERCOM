// SPDX-License-Identifier: MIT
pragma solidity ^0.8.7;

import "./MembershipManagement.sol";
import "./CouncilManager.sol";
import "./Document.sol";
import "./Utils.sol";

/**
 * @title Proposal
 * @dev Abstract contract for managing proposals within a DAO.
 *      Includes functionalities for voting, updating status, and managing documents.
 */
abstract contract Proposal is DocumentsHolder {
    uint public id;
    MembershipManagement.ProposalTypes public proposalType;
    uint public duration;
    MembershipManagement.ApprovalStatus public status;
    uint public timestamp;
    bool public isProcessing = false;
    uint public randomNumber;
    bool public votingStarted = false;
    address public owner;
    address[] voters;
    mapping(address => MembershipManagement.Vote) votes;
    MembershipManagement.ContractAddresses contractAddresses;
    event VotingStarted(uint indexed proposalId);
    event VotingCompleted(uint indexed proposalId);
    event StatusUpdated(uint indexed proposalId, MembershipManagement.ApprovalStatus newStatus);
    event VoteCasted(uint indexed proposalId, address member, bool vote);

    error AuthorizationError();
    modifier isFromDAOorVoting() {
        if(msg.sender != contractAddresses.daoAddress && msg.sender != contractAddresses.votingAddress && msg.sender != contractAddresses.membershipManagerAddress && msg.sender != contractAddresses.membershipRemovalAddress && msg.sender != owner) revert AuthorizationError();
        _;
    }

    modifier isFromDAO() {
        if(msg.sender != contractAddresses.daoAddress && msg.sender != contractAddresses.votingAddress && msg.sender != contractAddresses.membershipManagerAddress && msg.sender != contractAddresses.membershipRemovalAddress)revert AuthorizationError();
        _;
    }

    modifier isFromVoting() {
        if(msg.sender != contractAddresses.votingAddress)revert AuthorizationError();
        _;
    }
    constructor(
        address _owner, 
        MembershipManagement.ContractAddresses memory _contractAddresses,
        uint _id, 
        MembershipManagement.ProposalTypes _proposalType, 
        uint _duration
    ) {
        owner = _owner;
        contractAddresses = _contractAddresses;
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
    error VotingNotStarted();
    error VotingClosed();
    /**
     * @dev Casts a vote for the proposal.
     * @param voteCasted The vote cast by the member (true for approve, false for reject).
     * @param member The address of the member casting the vote.
     */
    function vote(bool voteCasted, address member) public isFromDAO() {
        if(!votingStarted)revert VotingNotStarted();
        if(block.timestamp > duration)revert VotingClosed();
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
    function addDocument(address signer, string memory title, string memory url, bytes32 docHash, bytes memory signature) isFromDAOorVoting() public override{
        super.addDocument(signer, title, url, docHash, signature);
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
        MembershipManagement.ContractAddresses memory _contractAddresses,
        uint _id,
        uint _duration, 
        MembershipManagement.Nation memory _nation, 
        uint _groupId
    ) 
        Proposal(_owner, _contractAddresses, _id, MembershipManagement.ProposalTypes.Membership, _duration)
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
        CouncilManager manager = CouncilManager(contractAddresses.councilManagementAddress);
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

contract MembershipRemovalProposal is Proposal{
    MembershipManagement.Nation nationToRemove;
     constructor(
        address _owner, 
        MembershipManagement.ContractAddresses memory _contractAddresses,
        uint _id,
        uint _duration, 
        MembershipManagement.Nation memory _nation
    ) 
        Proposal(_owner, _contractAddresses , _id, MembershipManagement.ProposalTypes.MembershipRemoval, _duration)
    {
        nationToRemove = _nation;
    }
    function getNation() public view returns(MembershipManagement.Nation memory) {
        return nationToRemove;
    }
    function getMembershipResponse() public view returns(MembershipManagement.MembershipRemovalResponse memory) {
        return MembershipManagement.MembershipRemovalResponse(
            id, 
            nationToRemove, 
            getVotes(), 
            duration, 
            status, 
            isProcessing, 
            votingStarted, 
            owner, 
            address(this)
        );
    }
}
contract ChangeVotingParametersProposal is Proposal{
    MembershipManagement.ChangeVotingParametersRole[] parameters;
    constructor(
        address _owner, 
        MembershipManagement.ContractAddresses memory _contractAddresses,
        uint _id,
        uint _duration, 
        MembershipManagement.ChangeVotingParametersRole[] memory _parameters
    ) 
        Proposal(_owner, _contractAddresses , _id, MembershipManagement.ProposalTypes.UpdateVotingParameters, _duration)
    {
        uint i = 0;
        while(i < _parameters.length){
            parameters.push();
            parameters[i].council = _parameters[i].council;
            parameters[i].parameters.randomizeByGroup = _parameters[i].parameters.randomizeByGroup;
            parameters[i].parameters.randomizeByMember = _parameters[i].parameters.randomizeByMember;
            parameters[i].parameters.outputCountForGroup = _parameters[i].parameters.outputCountForGroup;
            parameters[i].parameters.outputCountForMember = _parameters[i].parameters.outputCountForMember;
            parameters[i].parameters.voteDenominator = _parameters[i].parameters.voteDenominator;
            parameters[i].parameters.voteNumerator = _parameters[i].parameters.voteNumerator;
            parameters[i].parameters.sumDenominator = _parameters[i].parameters.sumDenominator;
            parameters[i].parameters.sumNumerator = _parameters[i].parameters.sumNumerator;
            parameters[i].parameters.avgVotes = _parameters[i].parameters.avgVotes;
            i++;
        }
    }
    function getChangeResponse() public view returns(MembershipManagement.ChangeVotingParametersResponse memory) {
        return MembershipManagement.ChangeVotingParametersResponse(
            id, 
            parameters, 
            getVotes(), 
            duration, 
            status, 
            isProcessing, 
            votingStarted, 
            owner, 
            address(this)
        );
    }
}
contract ProposalStorageManager{
    address daoAddress;
    mapping(address => address) membershipProposals;
    mapping(address => address) membershipRemovalProposals;
    mapping(uint => address) public proposals;
    address[] membershipProposalAddresses;
    address[] membershipRemovalProposalAddresses;
    address[] changeParametersProposalAddresses;
    uint public proposalCount = 0;
    constructor(address _daoAddress){
        daoAddress = _daoAddress;
    }
    modifier isFromDAO() {
        if(daoAddress == msg.sender) revert Utils.AuthorizationError();
        _;
    }
    function getNextProposalId() public isFromDAO() returns(uint){
        return ++proposalCount;
    }
    function getMembershipProposal(address key) public view returns(address){
        return membershipProposals[key];
    }
    function setMembershipProposal(address key, address value) public isFromDAO(){
        membershipProposals[key] = value;
    }
    function getProposal(uint key) public view returns(address){
        return proposals[key];
    }
    function setProposal(uint key, address value) public isFromDAO(){
        proposals[key] = value;
    }
    function getMembershipRemovalProposal(address key) public view returns(address){
        return membershipRemovalProposals[key];
    }
    function setMembershipRemovalProposal(address key, address value) isFromDAO() public{
        membershipRemovalProposals[key] = value;
    }
    function addMembershipProposal(address key) isFromDAO() public{
        membershipProposalAddresses.push(key);
    }
    function addMembershipRemovalProposal(address key) isFromDAO() public{
        membershipRemovalProposalAddresses.push(key);
    }
    function addChangeParametersProposal(address key) isFromDAO() public{
        changeParametersProposalAddresses.push(key);
    }
    function getChangeParametersProposalAddresses() public view returns(address[] memory){
        return changeParametersProposalAddresses;
    }
    function getMembershipProposalAddresses() public view returns(address[] memory){
        return membershipProposalAddresses;
    }
    function getMembershipRemovalProposalAddresses() public view returns(address[] memory){
        return membershipRemovalProposalAddresses;
    }
}
