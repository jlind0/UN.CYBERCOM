// SPDX-License-Identifier: MIT
pragma solidity ^0.8.7;
import "./MembershipManagement.sol";
import "./CouncilManager.sol";
import "./Document.sol";
abstract contract Proposal is DocumentsHolder{
    
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
    address daoAddress;
    address votingAddress;
    address councilManagementAddress;
    modifier isFromDAOorVoting(){
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
    function updateStatus(MembershipManagement.ApprovalStatus _status) public isFromDAOorVoting(){
        status = _status;
    }
    function setRandomNumber(uint random) public isFromVoting(){
        randomNumber = random;
    }
    function setProcessing(bool processing) isFromVoting() public{
        isProcessing = processing;
    }
    function getVotes() public view returns(MembershipManagement.Vote[] memory){
        uint i = 0;
        MembershipManagement.Vote[] memory vs = new MembershipManagement.Vote[](voters.length);
        while(i < voters.length){
            vs[i] = votes[voters[i]];
            i++;
        }
        return vs;
    }
    function vote(bool voteCasted, address member)
        public isFromDAO()
    {
        require(votingStarted);
        if(duration < block.timestamp)
            revert("Voting has closed");
        if(votes[member].proposalId != id)
            voters.push(member);
        votes[member] = MembershipManagement.Vote(member, voteCasted, block.timestamp, id);
    }
    function startVoting(address sender) public isFromDAO(){
        require(sender == owner);
        require(!votingStarted);
        votingStarted = true;
        duration = duration + block.timestamp;
        status = MembershipManagement.ApprovalStatus.Pending;
    }
    constructor(address _owner, address _daoAddress, address _votingAddress, address _councilManager, uint _id, MembershipManagement.ProposalTypes _proposalType, uint _duration){
        owner = _owner;
        daoAddress = _daoAddress;
        votingAddress = _votingAddress;
        councilManagementAddress = _councilManager;
        id = _id;
        proposalType = _proposalType;
        if(_duration < 1 minutes)
            _duration = 1 minutes;
        duration = _duration;
        status = MembershipManagement.ApprovalStatus.Entered;
        timestamp = block.timestamp;
    }
    function addDocument(address signer, string memory title, string memory url, bytes32 docHash, bytes memory signature) isFromDAOorVoting() public override{
        super.addDocument(signer, title, url, docHash, signature);
    }
    
}

contract MembershipProposal is Proposal{
    MembershipManagement.Nation nation;
    uint public groupId;
    constructor(address _owner, address _daoAddress, address _votingAddress, address _councilManager, uint _id, MembershipManagement.ProposalTypes _proposalType, uint _duration, 
        MembershipManagement.Nation memory _nation, uint _groupId) 
        Proposal(_owner, _daoAddress,_votingAddress,_councilManager, _id, _proposalType, _duration)
    {
        nation = _nation;
        groupId = _groupId;
    }
    function getNation() public view returns(MembershipManagement.Nation memory){
        return nation;
    }
    function getMembershipResponse() public view returns(MembershipManagement.MembershipProposalResponse memory){
        CouncilManager manager = CouncilManager(councilManagementAddress);
        return MembershipManagement.MembershipProposalResponse(id, nation.id, nation, manager.getCouncilRoleForGroup(groupId), groupId, getVotes(), duration, status, isProcessing, votingStarted, owner, address(this));
    }
}