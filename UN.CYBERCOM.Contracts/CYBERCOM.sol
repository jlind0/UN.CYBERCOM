// SPDX-License-Identifier: MIT
pragma solidity ^0.8.7;

import "./node_modules/@openzeppelin/contracts/access/AccessControl.sol";
import "./node_modules/@openzeppelin/contracts/utils/ReentrancyGuard.sol";
import "./node_modules/@openzeppelin/contracts/utils/math/Math.sol";
import "./node_modules/@chainlink/contracts/src/v0.8/interfaces/VRFCoordinatorV2Interface.sol";
import "./node_modules/@chainlink/contracts/src/v0.8/vrf/VRFConsumerBaseV2.sol";

contract CYBERCOM is ReentrancyGuard, AccessControl, VRFConsumerBaseV2  {
    uint256 private constant ROLL_IN_PROGRESS = 42;
    uint64 s_subscriptionId;
    address s_owner;
    VRFCoordinatorV2Interface COORDINATOR;
    address vrfCoordinator = 0x8103B0A8A00be2DDC778e6e7eaa21791Cd364625;
    bytes32 s_keyHash = 0x474e34a077df58807dbe9c96d3c009b23b3c6d0cce433e59bbf5b34f823bc56c;
    uint32 callbackGasLimit = 4000000;
    uint16 requestConfirmations = 3;
    uint32 numWords =  1;
    mapping(uint => uint) requestToProposal;
    function tallyScore(CouncilVotes[] memory tally) 
        private pure returns(int)
    {
        int score = 0;
        uint i = 0;
        while(i < tally.length){
            CouncilVotes memory cv = tally[i];
            score += cv.score;
            i++;
        }
        return score;
    }
    function fulfillRandomWords(uint256 requestId, uint256[] memory randomWords) internal override {
        Proposal storage proposal = proposals[requestToProposal[requestId]];
        TallyResult memory tally = tallyVotes(proposal.id, randomWords[0]);
        if(tally.status == ApprovalStatus.Approved){
            proposal.status = ApprovalStatus.Approved;
            if(proposal.proposalType == ProposalTypes.Membership){
                MembershipProposal storage mp = membershipProposals[proposal.id];
                mp.proposal = proposal;
                acceptNewMember(mp);
            }
        }
        else if(tally.status == ApprovalStatus.Rejected){
            proposal.status = ApprovalStatus.Rejected;
            if(proposal.proposalType == ProposalTypes.Membership){
                MembershipProposal storage mp = membershipProposals[proposal.id];
                mp.proposal = proposal;
            }
        }
    }
    modifier isMember(string memory message) {
        require(hasRole(BROKER_ROLE, msg.sender) ||
        hasRole(POWER_ROLE, msg.sender) ||
        hasRole(CENTRAL_ROLE, msg.sender) ||
        hasRole(EMERGING_ROLE, msg.sender) ||
        hasRole(GENERAL_ROLE, msg.sender), message);
        _;
    } 
    function prepareTally(uint proposalId)
        isMember("Only members can trigger a Tally") 
        external returns (uint256 requestId) 
    {
        Proposal storage p = proposals[proposalId];
        if(!p.isProcessing && 
            p.status == ApprovalStatus.Pending && 
            p.duration < block.timestamp)
        {
            // Will revert if subscription is not set and funded.
            requestId = COORDINATOR.requestRandomWords(
            s_keyHash,
            s_subscriptionId,
            requestConfirmations,
            callbackGasLimit,
            numWords
            );
            p.isProcessing = true;
            requestToProposal[requestId] = proposalId;
        }
        else
            revert("Voting has not closed");
    }
    bytes32 public immutable BROKER_ROLE = keccak256("BROKER");
    bytes32 public immutable POWER_ROLE = keccak256("POWER");
    bytes32 public immutable CENTRAL_ROLE = keccak256("CENTRAL");
    bytes32 public immutable EMERGING_ROLE = keccak256("EMERGING");
    bytes32 public immutable GENERAL_ROLE = keccak256("GENERAL");
    uint totalNations;
    uint totalCouncilGroups;
    address[] nationAddresses;
    mapping(address => Nation) nations;
    mapping(address => Council) nationsCouncil;
    mapping(uint => CouncilGroup) councilGroups;
    struct Nation{
        address id;
        string name;
    }
    bytes32[] councilRoles;
    mapping(bytes32 => Council) councils;
    struct Council{
        string name;
        bytes32 role;
        VotingParameters votingParameters;
        uint groupsCount;
        CouncilGroup[] groups;
    }
    struct CouncilGroup{
        uint id;
        string name;
        Nation[] members;
    }
    struct VotingParameters{
        bool randomizeByGroup;
        bool randomizeByMember;
        uint outputCountForGroup;
        uint outputCountForMember;
        uint voteDenominator;
        uint voteNumerator;
        uint sumDenominator;
        uint sumNumerator;
        bool avgVotes;
    }
    function getCouncils()
        external view returns(Council[] memory)
    {
        uint i = 0;
        Council[] memory cs = new Council[](councilRoles.length);
        while(i < councilRoles.length){
            cs[i] = councils[councilRoles[i]];
            i++;
        }
        return cs;
    }
    function getNations()
        external view returns(Nation[] memory)
    {
        Nation[] memory ns = new Nation[](nationAddresses.length);
        uint i = 0;
        while(i < nationAddresses.length){
            ns[i] = nations[nationAddresses[i]];
            i++;
        }
        return ns;
    }
    uint totalMembershipProposals;
    uint totalProposals;
    uint32 MIN_VOTE_DURATION = 5 minutes;
    mapping(uint => Proposal) proposals;
    uint[] proposalIds;
    mapping (uint => MembershipProposal) membershipProposals;
    uint[] membershipProposalIds;
    mapping(address => MembershipProposal) membershipProposalsByMember;
    struct MembershipProposalRequest{
        address member;
        Nation newNation;
        bytes32 council;
        uint groupId;
        uint duration;
    }
    struct Proposal{
        uint id;
        ProposalTypes proposalType;
        uint duration;
        ApprovalStatus status;
        uint timestamp;
        bool isProcessing;
    }
    enum ApprovalStatus{
        Pending,
        Approved,
        Rejected
    }
    struct MembershipProposal{
        uint id;
        Proposal proposal;
        address member;
        Nation newNation;
        bytes32 council;
        uint groupId;
    }
    enum ProposalTypes{
        Membership
    }
    mapping(uint => Vote[]) proposalVotes;
    struct Vote{
        address member;
        bool voteCasted;
        uint timestamp;
        uint proposalId;
    }
    function performVote(uint proposalId, bool voteCasted)
        isMember("Must be a member to vote")
        external
    {
        Proposal memory p = proposals[proposalId];
        if(p.id != proposalId)
            revert("Invalid proposal id");
        if(p.duration < block.timestamp)
            revert("Voting has closed");
        Vote[] storage votes = proposalVotes[proposalId];
        uint i = 0;
        bool voteUpdated = false;
        while(i < votes.length){
            if(votes[i].member == msg.sender){
                votes[i].voteCasted = voteCasted;
                votes[i].timestamp = block.timestamp;
                voteUpdated = true;
                break;
            }
            i++;
        }
        if(!voteUpdated)
            votes.push(Vote(msg.sender, voteCasted, block.timestamp, proposalId));
    }
    constructor(uint64 subscriptionId) VRFConsumerBaseV2(vrfCoordinator) {
        COORDINATOR = VRFCoordinatorV2Interface(vrfCoordinator);
        s_owner = msg.sender;
        s_subscriptionId = subscriptionId;
        councils[BROKER_ROLE].name = "Broker";
        councils[BROKER_ROLE].role = BROKER_ROLE;
        councils[BROKER_ROLE].votingParameters = VotingParameters(false, false, 0, 0, 1, 0, 1, 0, false);
        councils[BROKER_ROLE].groups.push();
        councils[BROKER_ROLE].groups[0].id = totalCouncilGroups++;
        councils[BROKER_ROLE].groups[0].name = "Primary";
        councilGroups[councils[BROKER_ROLE].groups[0].id] = councils[BROKER_ROLE].groups[0];
        councils[BROKER_ROLE].groupsCount = 1;
        councilRoles.push(BROKER_ROLE);
        councils[POWER_ROLE].name = "Power";
        councils[POWER_ROLE].role = POWER_ROLE;
        councils[POWER_ROLE].votingParameters = VotingParameters(false, true, 0, 1, 1, 6, 1, 1, false);
        councils[POWER_ROLE].groups.push();
        councils[POWER_ROLE].groups[0].id = totalCouncilGroups++;
        councils[POWER_ROLE].groups[0].name = "Primary";
        councilGroups[councils[POWER_ROLE].groups[0].id] = councils[POWER_ROLE].groups[0];
        councils[POWER_ROLE].groupsCount = 1;
        councilRoles.push(POWER_ROLE);
        councils[CENTRAL_ROLE].name = "Central";
        councils[CENTRAL_ROLE].role = CENTRAL_ROLE;
        councils[CENTRAL_ROLE].votingParameters = VotingParameters(false, false, 0, 1, 3, 1, 1, 2, false);
        councils[CENTRAL_ROLE].groups.push();
        councils[CENTRAL_ROLE].groups[0].name = "Primary";
        councils[CENTRAL_ROLE].groups[0].id = totalCouncilGroups++;
        councilGroups[councils[CENTRAL_ROLE].groups[0].id] = councils[CENTRAL_ROLE].groups[0];
        councils[CENTRAL_ROLE].groupsCount = 1;
        councilRoles.push(CENTRAL_ROLE);
        councils[EMERGING_ROLE].name = "Emerging";
        councils[EMERGING_ROLE].role = EMERGING_ROLE;
        councils[EMERGING_ROLE].votingParameters = VotingParameters(true, false, 1, 0, 4, 1, 1, 3, false);
        councils[EMERGING_ROLE].groups.push();
        councils[EMERGING_ROLE].groups[0].id = totalCouncilGroups++;
        councils[EMERGING_ROLE].groups[0].name = "Group A";
        councilGroups[councils[EMERGING_ROLE].groups[0].id] = councils[EMERGING_ROLE].groups[0];
        councils[EMERGING_ROLE].groups.push();
        councils[EMERGING_ROLE].groups[1].id = totalCouncilGroups++;
        councils[EMERGING_ROLE].groups[1].name = "Group B";
        councilGroups[councils[EMERGING_ROLE].groups[1].id] = councils[EMERGING_ROLE].groups[1];
        councils[EMERGING_ROLE].groupsCount = 2;
        councilRoles.push(EMERGING_ROLE);
        councils[GENERAL_ROLE].name = "General";
        councils[GENERAL_ROLE].role = GENERAL_ROLE;
        councils[GENERAL_ROLE].votingParameters = VotingParameters(false, false, 0, 0, 1, 1, 1, 2, true);
        councils[GENERAL_ROLE].groups.push();
        councils[GENERAL_ROLE].groups[0].name = "Primary";
        councils[GENERAL_ROLE].groups[0].id = totalCouncilGroups++;
        councilGroups[councils[GENERAL_ROLE].groups[0].id] = councils[GENERAL_ROLE].groups[0];
        councils[GENERAL_ROLE].groupsCount = 1;
        councilRoles.push(GENERAL_ROLE);
    }
    modifier onlyOwner() {
        require(msg.sender == s_owner);
        _;
    }
    function getCouncil(bytes32 role) 
        external view returns (Council memory)
    {
        return councils[role];
    }
    function getPendingMembershipRequests() 
        external view returns(MembershipProposal[] memory)
    {
        MembershipProposal[] memory props = new MembershipProposal[](totalMembershipProposals);
        uint i = 0; 
        uint j = 0;
        while(i < totalMembershipProposals)
        {
            if(membershipProposals[membershipProposalIds[i]].proposal.status == ApprovalStatus.Pending)
            {
                props[j] = membershipProposals[membershipProposalIds[i]];
                j++;
            }
            i++;
        }
        MembershipProposal[] memory rtn = new MembershipProposal[](j);
        i = 0;
        while(i < j){
            rtn[i] = props[i];
            i++;
        }
        return rtn;
    }
    function getRejectedMembershipRequests() 
        external view returns(MembershipProposal[] memory)
    {
        MembershipProposal[] memory props = new MembershipProposal[](totalMembershipProposals);
        uint i = 0; 
        uint j = 0;
        while(i < totalMembershipProposals)
        {
            if(membershipProposals[membershipProposalIds[i]].proposal.status == ApprovalStatus.Rejected)
            {
                props[j] = membershipProposals[membershipProposalIds[i]];
                j++;
            }
            i++;
        }
        MembershipProposal[] memory rtn = new MembershipProposal[](j);
        i = 0;
        while(i < j){
            rtn[i] = props[i];
            i++;
        }
        return rtn;
    }
    function getApprovedMembershipRequests() 
        external view returns(MembershipProposal[] memory)
    {
        MembershipProposal[] memory props = new MembershipProposal[](totalMembershipProposals);
        uint i = 0; 
        uint j = 0;
        while(i < totalMembershipProposals)
        {
            if(membershipProposals[membershipProposalIds[i]].proposal.status == ApprovalStatus.Approved)
            {
                props[j] = membershipProposals[membershipProposalIds[i]];
                j++;
            }
            i++;
        }
        MembershipProposal[] memory rtn = new MembershipProposal[](j);
        i = 0;
        while(i < j){
            rtn[i] = props[i];
            i++;
        }
        return rtn;
    }
    struct CouncilVotes{
        bytes32 councilId;
        VotingParameters votingParameters;
        CouncilGroupVotes[] votes;
        int score;
    }
    struct CouncilGroupVotes{
        uint groupId;
        Vote[] votes;
        int score;
    }
    struct TallyResult{
        CouncilVotes[] acceptedVotes;
        int score;
        ApprovalStatus status;
        uint proposalId;
    }
    mapping(uint => TallyResult) proposalTallyResults;
    function tallyVotes(uint proposalId, uint randomNumber) 
        private view returns(TallyResult memory tallyResult)
    {
        if(proposalTallyResults[proposalId].proposalId == proposalId)
            revert("The proposal has already been tallies");
        Vote[] memory vs = proposalVotes[proposalId];
        if(vs.length == 0)
            revert("No votes on proposal.");

        CouncilVotes[] memory cvs = getCouncilVotes(proposalId);
        uint i = 0;
        
        while(i < vs.length){

            uint rdn = randomNumber;
            CouncilVotes memory cv = cvs[i];
            Council memory c = councils[cv.councilId];
            uint colSize = c.groups.length;
            if(cv.votes.length >= cv.votingParameters.outputCountForGroup && cv.votingParameters.outputCountForGroup > 0)
                colSize = cv.votingParameters.outputCountForGroup;
            cv = buildGroupVotes(cv);
            uint rdn2 = rdn;
            CouncilGroupVotes[] memory cgv = new CouncilGroupVotes[](colSize);
            
            bool doAvg = cv.votingParameters.avgVotes;
            if(cv.votingParameters.randomizeByGroup){
                
                cgv = populateRandomGroupVotes(cv, cgv, colSize, rdn2);
            }
            else
                cgv = populateGroupVote(cv, cgv, colSize, rdn2, doAvg);
            cv.votes = cgv;
            i++;
        }
        cvs = populateGroupScore(cvs);
        int finalScore = tallyScore(cvs);
        ApprovalStatus status = ApprovalStatus.Pending;
        if(finalScore > 0)
            status = ApprovalStatus.Approved;
        else
            status = ApprovalStatus.Rejected;
        return TallyResult(cvs, finalScore, status, proposalId);

    }
    function getProposalVotes(uint proposalId)
        external view returns(CouncilVotes[] memory){
            return getCouncilVotes(proposalId);
    }
    function getCouncilVotes(uint proposalId)
        private view returns(CouncilVotes[] memory){
        uint i = 0;
        Vote[] memory vs = proposalVotes[proposalId];
        CouncilVotes[] memory cvs = new CouncilVotes[](5);
        while(i < vs.length){
            Vote memory vt = vs[i];
            Council memory council = nationsCouncil[vt.member];
            CouncilGroup memory cg = findCouncilGroup(council, vt.member);
            uint idx = getIndexForCouncil(council.role);
            CouncilVotes memory cv = cvs[idx];
            cv.councilId = council.role;
            cv.votingParameters = council.votingParameters;
            if(cv.votes.length == 0)
                cv.votes = new CouncilGroupVotes[](council.groups.length);
            uint groupVotes = 0;
            while(groupVotes < cv.votes.length)
            {
                cv.votes[groupVotes] = CouncilGroupVotes(council.groups[groupVotes].id, new Vote[](vs.length), 0);
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
    function buildGroupVotes(CouncilVotes memory cv)
        private pure returns(CouncilVotes memory rtn)
    {
        uint targetGroup = 0;
        while(targetGroup < cv.votes.length){
            uint k = 0;
            uint m = 0;
            while(k < cv.votes[targetGroup].votes.length){
                if(cv.votes[targetGroup].votes[k].timestamp != 0){
                    m++;
                }
                k++;
            }
            Vote[] memory cggv = new Vote[](m);
            k = 0;m = 0;
            while(k < cv.votes[targetGroup].votes.length){
                if(cv.votes[targetGroup].votes[k].timestamp != 0){
                    cggv[m] = cv.votes[targetGroup].votes[k];
                    m++;
                }
                k++;
            }          
            cv.votes[targetGroup].votes = cggv;
            targetGroup++;
        }
        rtn = cv;
    }
    function populateRandomGroupVotes(
        CouncilVotes memory cv,
        CouncilGroupVotes[] memory cgv, 
        uint colSize,
        uint rdn2)
        private pure returns(CouncilGroupVotes[] memory rtn)
    {
        uint[] memory previousIndexs = new uint[](cv.votes.length);
        uint j = 0;
        while(j < colSize){
            uint index = (rdn2 % cv.votes.length);
            uint n = 0;
            bool doAdd = true;
            while(n < j){
                if(previousIndexs[n] == index){
                    doAdd = false;
                    break;
                }
                n++;
            }
            if(doAdd)
            {
                cgv[j] = cv.votes[index - 1];
                previousIndexs[j] = index;
            }
            j++;
        }
        rtn = cgv;
    }
    function populateGroupVote(
        CouncilVotes memory cv, 
        CouncilGroupVotes[] memory cgv,
        uint colSize,
        uint rdn2,
        bool doAvg)
        private pure returns(CouncilGroupVotes[] memory rtn)
    {
        uint j = 0;
        while(j < colSize && j < cv.votes.length)
        {
            cgv[j] = cv.votes[j]; 
            j++;
        }
        uint r = 0;
        while(r < cgv.length){
            uint cvVoteNumerator = cv.votingParameters.voteNumerator;
            uint cvVoteDenom = cv.votingParameters.voteDenominator;
            CouncilGroupVotes memory cf = cgv[r];
            uint groupColSize = cf.votes.length;
            if(cv.votingParameters.outputCountForMember > 0 && groupColSize > cv.votingParameters.outputCountForMember)
                groupColSize = cv.votingParameters.outputCountForMember;
            Vote[] memory vts = new Vote[](groupColSize);
            vts = applyGroupRandominzation(cv, cf, vts, rdn2, groupColSize);
            cf = populateGroupVote(vts, cf, cvVoteNumerator, cvVoteDenom, doAvg);
            cgv[r] = cf;
            r++;
        }
        rtn = cgv;
    }
    function applyGroupRandominzation(
        CouncilVotes memory cv, 
        CouncilGroupVotes memory cf,
        Vote[] memory vts,
        uint rdn2, 
        uint groupColSize)
        private pure returns(Vote[] memory rtn)
    {
        if(cv.votingParameters.randomizeByMember)
        {
            uint[] memory prevIndexs = new uint[](cf.votes.length);
            uint j = 0;
            while(j < groupColSize){
                uint index = (rdn2 % cf.votes.length);
                uint m = 0;
                bool doAdd = true;
                while(m < j){
                    if(prevIndexs[m] == index){
                        doAdd = false;
                        break;
                    }
                    m++;
                }
                if(doAdd)
                {
                    vts[j] = cf.votes[index];
                    prevIndexs[j] = index;
                }
                j++;
            }
        }
        rtn = vts;
    }
    function populateGroupVote(Vote[] memory vts, 
        CouncilGroupVotes memory cf, 
        uint cvVoteNumerator, 
        uint cvVoteDenom,
        bool doAvg)
        private pure returns(CouncilGroupVotes memory rtn)
    {
        uint g = 0;
        int rs = 0;
        int[] memory rrs = new int[](vts.length);
        while(g < vts.length){
            int v = 0;
            if(vts[g].voteCasted)
                v = 1;
            else
                v = -1;
            int rr = multiply(Math.mulDiv(100000, cvVoteNumerator, cvVoteDenom), v);
            rs += rr;
            rrs[g] = rr;
            g++;
        }
        int ss = cf.score;
        if(doAvg)
            ss += calculateAverage(rrs);
        else
            ss+= rs;
        cf.score = ss;
        rtn = cf;
    }
    function populateGroupScore(CouncilVotes[] memory cvs)
        private pure returns(CouncilVotes[] memory rtn)
    {
        uint y = 0;
        int result = 0;
        while(y < cvs.length){
            uint g = 0;
            CouncilVotes memory cv = cvs[y];
            int[] memory results = new int[](cv.votes.length);

            while(g < cv.votes.length){
                CouncilGroupVotes memory cgv = cv.votes[g];
                int v = cgv.score;
                int rr = multiply(Math.mulDiv(100000, cv.votingParameters.voteNumerator, cv.votingParameters.voteDenominator), v);
                results[g] = rr;
                result += rr;
                g++;
            }
            if(cv.votingParameters.avgVotes)
                cv.score += calculateAverage(results);
            else
                cv.score += result;
            cvs[y] = cv;
            y++;
        }
        rtn = cvs;
    }
    function calculateAverage(int[] memory numbers) public pure returns (int) {
        require(numbers.length > 0, "Array is empty");

        int sum = 0;
        for (uint i = 0; i < numbers.length; i++) {
            sum += numbers[i];
        }

        return sum / int(numbers.length);
    }
    function multiply(uint a, int b) public pure returns (int) {
        if (b < 0) {
            return -int(a) * b;
        } else {
            return int(a) * b;
        }
    }
    function getIndexForCouncil(bytes32 id)
        private view returns(uint)
    {
        if(id == GENERAL_ROLE)
            return 0;
        if(id == POWER_ROLE)
            return 1;
        if(id == CENTRAL_ROLE)
            return 2;
        if(id == EMERGING_ROLE)
            return 3;
        if(id == BROKER_ROLE)
            return 4;
        revert("Council not found");
    }
    function findCouncilGroup(Council memory council, address nationId)
        private pure returns(CouncilGroup memory)
    {
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
    function submitMembershipProposal(MembershipProposalRequest memory request) 
        external returns(uint)
    {
        if(councilGroups[request.groupId].id != request.groupId)
            revert("Invalid Group");
        if(totalNations == 0){
            MembershipProposal memory prop = constructMembershipProposal(request);
            acceptNewMember(prop);
            return prop.id;
        }
        else if(nations[request.member].id == request.member)
            revert("Nation Already a Member");
        else if(membershipProposalsByMember[request.member].member == request.member)
        {
            MembershipProposal storage prop = membershipProposalsByMember[request.member];
            if(prop.proposal.status == ApprovalStatus.Pending)
            {
                //TODO: check status
            }
            else if(prop.proposal.status == ApprovalStatus.Approved)
                return prop.id;
            else if(prop.proposal.status == ApprovalStatus.Rejected)
                return constructMembershipProposal(request).id;
        }
        else
            return constructMembershipProposal(request).id;
        return 0;
    }
    
    function constructMembershipProposal(MembershipProposalRequest memory request) 
        private returns(MembershipProposal memory)
        {
            if(request.duration < MIN_VOTE_DURATION){
                request.duration = MIN_VOTE_DURATION;
            }
            MembershipProposal memory prop = MembershipProposal(
                totalMembershipProposals++,
                Proposal(
                    totalProposals++, 
                    ProposalTypes.Membership,
                    block.timestamp + request.duration,
                    ApprovalStatus.Pending,
                    block.timestamp,
                    false),
                request.member, 
                request.newNation, 
                request.council, 
                request.groupId
                );
         proposals[prop.proposal.id] = prop.proposal;
         proposalIds.push(prop.proposal.id);
         membershipProposals[prop.id] = prop;
         membershipProposalIds.push(prop.id);
         membershipProposalsByMember[prop.member] = prop;
         return prop;
    }
    function acceptNewMember(MembershipProposal memory proposal) 
    private {
        Council storage targetCouncil = councils[proposal.council];
        uint i = 0;
        while(i < targetCouncil.groups.length){
            if(targetCouncil.groups[i].id == proposal.groupId)
                break;
            i++;
        }
        CouncilGroup storage group = targetCouncil.groups[i];
        totalNations++;
        nations[proposal.newNation.id] = proposal.newNation;
        nationAddresses.push(proposal.newNation.id);
        nationsCouncil[proposal.newNation.id] = targetCouncil;
        group.members.push(proposal.newNation);
        _grantRole(targetCouncil.role, proposal.newNation.id);
    }
}