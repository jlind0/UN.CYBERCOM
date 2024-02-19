// SPDX-License-Identifier: MIT
pragma solidity ^0.8.7;

import "./MembershipManagement.sol";
import "./Proposal.sol";

contract CouncilManager{

    bytes32   public immutable BROKER_ROLE   = keccak256("BROKER");
    bytes32   public immutable POWER_ROLE    = keccak256("POWER");
    bytes32   public immutable CENTRAL_ROLE  = keccak256("CENTRAL");
    bytes32   public immutable EMERGING_ROLE = keccak256("EMERGING");
    bytes32   public immutable GENERAL_ROLE  = keccak256("GENERAL");
    bytes32   public immutable LESSER_ROLE   = keccak256("LESSER");
    bytes32   public immutable INDUSTRY_ROLE = keccak256("INDUSTRY");

    uint      public totalNations;
    uint      public totalCouncilGroups;
    address   public daoAddress;
    address[] public nationAddresses;

    mapping(address => MembershipManagement.Nation) public nations;
    mapping(address => bytes32) public nationsCouncil;
    mapping(uint => bytes32) public councilGroups;
    bytes32[] public councilRoles;
    mapping(bytes32 => MembershipManagement.Council) public councils;

    // Events
    event NewMemberAccepted(address indexed memberId, bytes32 role);
    event CouncilGroupExistsCheck(uint groupId, bool exists);
    event NationExistenceCheck(address memberId, bool exists);
    event CouncilRetrieved(bytes32 role);
    event CouncilGroupRoleRetrieved(uint groupId, bytes32 role);

    constructor(address _daoAddress) {
        daoAddress = _daoAddress;
        setupCouncil(BROKER_ROLE, "Broker", MembershipManagement.VotingParameters(false, false, 0, 0, 1, 0, 1, 0, false), "Primary");
        setupCouncil(POWER_ROLE, "Power", MembershipManagement.VotingParameters(false, true, 0, 1, 1, 6, 1, 1, false), "Primary");
        setupCouncil(CENTRAL_ROLE, "Central", MembershipManagement.VotingParameters(false, false, 0, 1, 3, 1, 1, 2, false), "Primary");
        setupCouncil(EMERGING_ROLE, "Emerging", MembershipManagement.VotingParameters(true, false, 1, 0, 5, 3, 1, 4, false), "Group A", "Group B");
        setupCouncil(GENERAL_ROLE, "General", MembershipManagement.VotingParameters(false, false, 0, 0, 1, 1, 1, 2, true), "Primary");
        setupCouncil(LESSER_ROLE, "Lesser", MembershipManagement.VotingParameters(false, false, 0, 1, 3, 1, 1, 5, false), "Primary");
        setupCouncil(INDUSTRY_ROLE, "Industry", MembershipManagement.VotingParameters(false, false, 0, 1, 7, 1, 1, 1, false), "Primary");
    }

    function setupCouncil(bytes32 role, string memory name, MembershipManagement.VotingParameters memory votingParams, string memory groupName, string memory secondaryGroupName) internal {
        councils[role].name = name;
        councils[role].role = role;
        councils[role].votingParameters = votingParams;

        councils[role].groups.push(MembershipManagement.CouncilGroup({
            id: ++totalCouncilGroups,
            name: groupName
        }));

        if (bytes(secondaryGroupName).length > 0) {
            councils[role].groups.push(MembershipManagement.CouncilGroup({
                id: ++totalCouncilGroups,
                name: secondaryGroupName
            }));
        }

        councilGroups[totalCouncilGroups] = role;
        councilRoles.push(role);
    }
    /**
     * @dev Retrieves a list of all councils.
     * @return An array of Council structures.
     */
    function getCouncils() public view returns(MembershipManagement.Council[] memory) {
        MembershipManagement.Council[] memory cs = new MembershipManagement.Council[](councilRoles.length);
        for (uint i = 0; i < councilRoles.length; i++) {
            cs[i] = councils[councilRoles[i]];
        }
        return cs;
    }
    function getCouncilForGroupId(uint groupId) public view returns(MembershipManagement.Council memory){
        return councils[councilGroups[groupId]];
    }
    modifier isFromDAO() {
        require(msg.sender == daoAddress, "Must be called from DAO");
        _;
    }

    /**
     * @dev Accepts a new member into a council based on the proposal.
     * @param proposalAddress The address of the membership proposal contract.
     */
    function acceptNewMember(address proposalAddress)
        public isFromDAO() returns(address memberId, bytes32 role) {
        MembershipProposal prop = MembershipProposal(proposalAddress);
        uint groupId = prop.groupId();
        bytes32 role = councilGroups[groupId];
        MembershipManagement.Council storage targetCouncil = councils[role];
        
        totalNations++;
        MembershipManagement.Nation memory nat = prop.getNation();
        nations[nat.id] = nat;
        nationAddresses.push(nat.id);
        nationsCouncil[nat.id] = role;
        targetCouncil.groups[0].members.push(nat); // Assuming first group for simplicity

        emit NewMemberAccepted(nat.id, role);
        return (nat.id, role);
    }

    /**
     * @dev Finds the council group for a given nation ID within a council.
     * @param council The council to search within.
     * @param nationId The ID of the nation.
     * @return The council group the nation belongs to.
     */
    function findCouncilGroup(MembershipManagement.Council memory council, address nationId)
        private pure returns(MembershipManagement.CouncilGroup memory) {
        uint j = 0;
        while(j < council.groups.length)
        {
            uint k = 0;
            while(k < council.groups[j].members.length){
                if(council.groups[j].members[k].id == nationId){
                    return council.groups[j];
                }
                k++;
            }
            j++;
        }
        revert("Council not found");
    }

    /**
     * @dev Returns the index associated with a council role.
     * @param role The bytes32 hash of the role.
     * @return The index of the council in the array.
     */
    function getIndexForCouncil(bytes32 role) private view returns (uint) {
        for (uint i = 0; i < councilRoles.length; i++) {
            if (councilRoles[i] == role) {
                return i;
            }
        }
        revert("Council role not found");
    }

    /**
     * @dev Processes an array of votes and organizes them into council votes.
     * @param vs Array of individual votes.
     * @return Array of council votes.
     */
    function getCouncilVotes(MembershipManagement.Vote[] memory vs)
        public view returns(MembershipManagement.CouncilVotes[] memory)
    {
        uint i = 0;
        MembershipManagement.CouncilVotes[] memory cvs = new MembershipManagement.CouncilVotes[](7);
        while(i < vs.length){
            MembershipManagement.Vote memory vt = vs[i];
            MembershipManagement.Council memory council = getCouncilForNation(vt.member);
            MembershipManagement.CouncilGroup memory cg = findCouncilGroup(council, vt.member);
            uint idx = getIndexForCouncil(council.role);
            MembershipManagement.CouncilVotes memory cv = cvs[idx];
            cv.councilId = council.role;
            cv.votingParameters = council.votingParameters;
            if(cv.votes.length == 0)
                cv.votes = new MembershipManagement.CouncilGroupVotes[](council.groups.length);
            uint groupVotes = 0;
            while(groupVotes < cv.votes.length)
            {
                cv.votes[groupVotes] = MembershipManagement.CouncilGroupVotes(council.groups[groupVotes].id, new MembershipManagement.Vote[](vs.length), 0);
                groupVotes++;
            }
            uint targetGroup = 0;
            while(targetGroup < cv.votes.length){
                if(cv.votes[targetGroup].groupId == cg.id)
                    break;
                targetGroup++;
            }
            if(targetGroup > cv.votes.length)
                revert("Logic error");
            cv.votes[targetGroup].votes[i] = vt;
            cvs[idx] = cv;
            i++;
        }
        return cvs;
    }

    /**
     * @dev Retrieves the council associated with a given role.
     */
    function getCouncil(bytes32 role) 
        public view returns (MembershipManagement.Council memory) {
        emit CouncilRetrieved(role);
        return councils[role];
    }

    /**
     * @dev Gets the role associated with a specific council group.
     */
    function getCouncilRoleForGroup(uint groupId) public view returns(bytes32) {
        emit CouncilGroupRoleRetrieved(groupId, councilGroups[groupId]);
        return councilGroups[groupId];
    }

    /**
     * @dev Checks if a council group exists.
     */
    function doesCouncilGroupExist(uint groupId) public view returns(bool) {
        bool exists = councils[councilGroups[groupId]].groups.length > 0;
        emit CouncilGroupExistsCheck(groupId, exists);
        return exists;
    }

    /**
     * @dev Checks if a nation is already registered.
     */
    function doesNationExist(address memberId) public view returns(bool) {
        bool exists = nations[memberId].id == memberId;
        emit NationExistenceCheck(memberId, exists);
        return exists;
    }
}
