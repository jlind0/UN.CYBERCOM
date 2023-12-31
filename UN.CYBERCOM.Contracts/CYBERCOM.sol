// SPDX-License-Identifier: MIT
pragma solidity ^0.8.7;

import "./node_modules/@openzeppelin/contracts/access/AccessControl.sol";
import "./node_modules/@openzeppelin/contracts/utils/ReentrancyGuard.sol";
import "./node_modules/@openzeppelin/contracts/utils/math/Math.sol";
import "./node_modules/@openzeppelin/contracts/utils/Strings.sol";
import "./node_modules/@chainlink/contracts/src/v0.8/interfaces/VRFCoordinatorV2Interface.sol";
import "./node_modules/@chainlink/contracts/src/v0.8/vrf/VRFConsumerBaseV2.sol";

contract CYBERCOM is ReentrancyGuard, AccessControl, VRFConsumerBaseV2  {
    uint256 private constant ROLL_IN_PROGRESS = 42;
    uint64 s_subscriptionId;
    address s_owner;
    VRFCoordinatorV2Interface COORDINATOR;
    address vrfCoordinator = 0x8103B0A8A00be2DDC778e6e7eaa21791Cd364625;
    bytes32 s_keyHash = 0x474e34a077df58807dbe9c96d3c009b23b3c6d0cce433e59bbf5b34f823bc56c;
    uint32 callbackGasLimit = 1000000;
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
    function fulfillRandomWords(uint256 requestId, uint256[] memory randomWords) 
    internal override {
        Proposal storage proposal = proposals[requestToProposal[requestId]];
        proposal.randomNumber = randomWords[0];
        proposal.status = ApprovalStatus.Ready;
        /*TallyResult memory tally = tallyVotes(proposal.id, randomWords[0]);
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
        }*/
    }
    function completeVoting(uint proposalId)
        isMember("Must be member to complete voting")
        external 
    {
        Proposal storage proposal = proposals[proposalId];
        if(proposal.status != ApprovalStatus.Ready)
            revert("Proposal is not ready to tally on yet");
        TallyResult memory tally = tallyVotes(proposal.id, proposal.randomNumber);
        if(tally.status == ApprovalStatus.Approved){
            proposal.status = ApprovalStatus.Approved;
            if(proposal.proposalType == ProposalTypes.Membership){
                MembershipProposal storage mp = membershipProposals[proposalToMembershipProposal[proposal.id]];
                acceptNewMember(mp);
            }
        }
        else if(tally.status == ApprovalStatus.Rejected){
            proposal.status = ApprovalStatus.Rejected;
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
    mapping(address => bytes32) nationsCouncil;
    mapping(uint => bytes32) councilGroups;
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
    uint32 MIN_VOTE_DURATION = 1 minutes;
    mapping(uint => Proposal) proposals;
    uint[] proposalIds;
    mapping (uint => MembershipProposal) membershipProposals;
    mapping(uint => uint) membershipProposalToProposal;
    mapping(uint => uint) proposalToMembershipProposal;
    uint[] membershipProposalIds;
    mapping(address => uint) memberMembershipProposal;
    struct MembershipProposalRequest{
        address member;
        Nation newNation;
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
        uint randomNumber;
    }
    enum ApprovalStatus{
        Pending,
        Ready,
        Approved,
        Rejected
    }
    struct MembershipProposalResponse{
        uint id;
        Proposal proposal;
        address member;
        Nation newNation;
        bytes32 council;
        uint groupId;
    }
    struct MembershipProposal{
        uint id;
        uint proposalId;
        Nation newNation;
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
        councils[BROKER_ROLE].groups[0].id = ++totalCouncilGroups;
        councils[BROKER_ROLE].groups[0].name = "Primary";
        councils[BROKER_ROLE].groupsCount = 1;
        councilGroups[totalCouncilGroups] = BROKER_ROLE;
        
        councilRoles.push(BROKER_ROLE);
        councils[POWER_ROLE].name = "Power";
        councils[POWER_ROLE].role = POWER_ROLE;
        councils[POWER_ROLE].votingParameters = VotingParameters(false, true, 0, 1, 1, 6, 1, 1, false);
        councils[POWER_ROLE].groups.push();
        councils[POWER_ROLE].groups[0].id = ++totalCouncilGroups;
        councils[POWER_ROLE].groups[0].name = "Primary";
        councils[POWER_ROLE].groupsCount = 1;
        councilGroups[totalCouncilGroups] = POWER_ROLE;
        
        councilRoles.push(POWER_ROLE);
        councils[CENTRAL_ROLE].name = "Central";
        councils[CENTRAL_ROLE].role = CENTRAL_ROLE;
        councils[CENTRAL_ROLE].votingParameters = VotingParameters(false, false, 0, 1, 3, 1, 1, 2, false);
        councils[CENTRAL_ROLE].groups.push();
        councils[CENTRAL_ROLE].groups[0].name = "Primary";
        councils[CENTRAL_ROLE].groups[0].id = ++totalCouncilGroups;
        councils[CENTRAL_ROLE].groupsCount = 1;
        councilGroups[totalCouncilGroups] = CENTRAL_ROLE;
        
        councilRoles.push(CENTRAL_ROLE);
        councils[EMERGING_ROLE].name = "Emerging";
        councils[EMERGING_ROLE].role = EMERGING_ROLE;
        councils[EMERGING_ROLE].votingParameters = VotingParameters(true, false, 1, 0, 4, 1, 1, 3, false);
        councils[EMERGING_ROLE].groups.push();
        councils[EMERGING_ROLE].groupsCount = 2;
        councils[EMERGING_ROLE].groups[0].id = ++totalCouncilGroups;
        councils[EMERGING_ROLE].groups[0].name = "Group A";
        councilGroups[totalCouncilGroups] = EMERGING_ROLE;
        councils[EMERGING_ROLE].groups.push();
        councils[EMERGING_ROLE].groups[1].id = ++totalCouncilGroups;
        councils[EMERGING_ROLE].groups[1].name = "Group B";
        councilGroups[totalCouncilGroups] = EMERGING_ROLE;
        
        councilRoles.push(EMERGING_ROLE);
        councils[GENERAL_ROLE].name = "General";
        councils[GENERAL_ROLE].role = GENERAL_ROLE;
        councils[GENERAL_ROLE].votingParameters = VotingParameters(false, false, 0, 0, 1, 1, 1, 2, true);
        councils[GENERAL_ROLE].groups.push();
        councils[GENERAL_ROLE].groups[0].name = "Primary";
        councils[GENERAL_ROLE].groups[0].id = ++totalCouncilGroups;
        councils[GENERAL_ROLE].groupsCount = 1;
        councilGroups[totalCouncilGroups] = GENERAL_ROLE;
        
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
        external view returns(MembershipProposalResponse[] memory)
    {
        MembershipProposal[] memory props = new MembershipProposal[](totalMembershipProposals);
        uint i = 0; 
        uint j = 0;
        while(i < totalMembershipProposals)
        {
            if(proposals[proposalToMembershipProposal[membershipProposalIds[i]]].status == ApprovalStatus.Pending)
            {
                props[j] = membershipProposals[membershipProposalIds[i]];
                j++;
            }
            i++;
        }
        MembershipProposalResponse[] memory rtn = new MembershipProposalResponse[](j);
        i = 0;
        while(i < j){
            rtn[i] = getMembershipResponse(props[i]);
            i++;
        }
        return rtn;
    }
    function getReadyMembershipRequests() 
        external view returns(MembershipProposalResponse[] memory)
    {
        MembershipProposal[] memory props = new MembershipProposal[](totalMembershipProposals);
        uint i = 0; 
        uint j = 0;
        while(i < totalMembershipProposals)
        {
            if(proposals[proposalToMembershipProposal[membershipProposalIds[i]]].status == ApprovalStatus.Ready)
            {
                props[j] = membershipProposals[membershipProposalIds[i]];
                j++;
            }
            i++;
        }
        MembershipProposalResponse[] memory rtn = new MembershipProposalResponse[](j);
        i = 0;
        while(i < j){
            rtn[i] = getMembershipResponse(props[i]);
            i++;
        }
        return rtn;
    }
    function getRejectedMembershipRequests() 
        external view returns(MembershipProposalResponse[] memory)
    {
        MembershipProposal[] memory props = new MembershipProposal[](totalMembershipProposals);
        uint i = 0; 
        uint j = 0;
        while(i < totalMembershipProposals)
        {
            if(proposals[proposalToMembershipProposal[membershipProposalIds[i]]].status == ApprovalStatus.Rejected)
            {
                props[j] = membershipProposals[membershipProposalIds[i]];
                j++;
            }
            i++;
        }
        MembershipProposalResponse[] memory rtn = new MembershipProposalResponse[](j);
        i = 0;
        while(i < j){
            rtn[i] = getMembershipResponse(props[i]);
            i++;
        }
        return rtn;
    }
    function getApprovedMembershipRequests() 
        external view returns(MembershipProposalResponse[] memory)
    {
        MembershipProposal[] memory props = new MembershipProposal[](totalMembershipProposals);
        uint i = 0; 
        uint j = 0;
        while(i < totalMembershipProposals)
        {
            if(proposals[proposalToMembershipProposal[membershipProposalIds[i]]].status == ApprovalStatus.Approved)
            {
                props[j] = membershipProposals[membershipProposalIds[i]];
                j++;
            }
            i++;
        }
        MembershipProposalResponse[] memory rtn = new MembershipProposalResponse[](j);
        i = 0;
        while(i < j){
            rtn[i] = getMembershipResponse(props[i]);
            i++;
        }
        return rtn;
    }
    function getMembershipResponse(MembershipProposal memory prop)
        private view returns(MembershipProposalResponse memory)
    {
        return MembershipProposalResponse(
            prop.id,
            proposals[prop.proposalId],
            prop.newNation.id,
            prop.newNation,
            councilGroups[prop.groupId],
            prop.groupId
        );
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
        private view returns(TallyResult memory)
    {
        if(proposalTallyResults[proposalId].proposalId == proposalId)
            revert("The proposal has already been tallies");
        Vote[] memory vs = proposalVotes[proposalId];
        if(vs.length == 0)
            return TallyResult(new CouncilVotes[](0), 0, ApprovalStatus.Rejected, proposalId);
        CouncilVotes[] memory cvs = getCouncilVotes(proposalId);
        uint i = 0;
        uint k = 0;
        while(i < cvs.length){
            CouncilVotes memory cv = cvs[i];
            uint j = 0;
            uint m = 0;
            while(j < cv.votes.length){
                if(cv.votes[j].votes.length > 0)
                    m++;
                j++;
            }
            CouncilGroupVotes[] memory cgv = new CouncilGroupVotes[](m);
            j = 0;
            m = 0;
            while(j < cv.votes.length){
                if(cv.votes[j].votes.length > 0)
                {
                    cgv[m] = cv.votes[j];
                    m++;
                }
                j++;
            }
            if(m > 0)
                k++;
            cv.votes = cgv;
            i++;
        }
        CouncilVotes[] memory cvs2 = new CouncilVotes[](k);
        i = 0;
        k = 0;
        while(i < cvs.length){
            if(cvs[i].votes.length > 0){
                cvs2[k] = cvs[i];
                k++;
            }
            i++;
        }
        cvs = cvs2;
        i = 0;
        while(i < cvs.length){
            CouncilVotes memory cv = cvs[i];
            if(cv.votingParameters.randomizeByGroup){
                if(cv.votes.length > cv.votingParameters.outputCountForGroup){
                    CouncilGroupVotes[] memory cgv = new CouncilGroupVotes[](cv.votingParameters.outputCountForGroup);
                    uint[] memory randoms = getRandomIndices(randomNumber, cv.votingParameters.outputCountForGroup, cv.votes.length);
                    uint x = 0;
                    while(x < randoms.length){
                        cgv[x] = cv.votes[randoms[x]];
                        x++;
                    }
                    cv.votes = cgv; 
                }
            }
            uint d = 0;
            while(d < cv.votes.length){
                CouncilGroupVotes memory cgv = cv.votes[d];
                if(cv.votingParameters.randomizeByMember){
                    if(cgv.votes.length > cv.votingParameters.outputCountForMember){
                        Vote[] memory vts = new Vote[](cv.votingParameters.outputCountForMember);
                        uint[] memory randoms = getRandomIndices(randomNumber, cv.votingParameters.outputCountForMember, cgv.votes.length);
                        uint x = 0;
                        while(x < randoms.length){
                            vts[x] = cgv.votes[randoms[x]];
                            x++;
                        }
                        cgv.votes = vts;
                    } 
                }
                cv.votes[d] = cgv;
                d++;
            }
            cvs[i] = cv;
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
    // Function to calculate the number of bits required to represent a number
    function bitLength(uint number) private pure returns (uint) {
        uint count = 0;
        while (number > 0) {
            count++;
            number >>= 1;
        }
        return count;
    }

    // Function to get N unique random indices from an array of length M
    function getRandomIndices(uint R, uint N, uint M) private pure returns (uint[] memory) {
        require(M > 1, "Array length must be greater than 1");
        require(N <= M, "Number of indices must be less than or equal to array length");

        uint[] memory indices = new uint[](N);
        uint bitsNeeded = bitLength(M - 1);
        uint maxVal = (1 << bitsNeeded) - 1;

        for (uint i = 0; i < N; ++i) {
            uint index;
            uint bitsToShift = i * bitsNeeded;

            // Extract bits for the index
            index = (R >> bitsToShift) & maxVal;

            // Adjust index if it's out of range
            while (index >= M) {
                index = (index + 1) % M;
            }

            // Check for duplicate
            for (uint j = 0; j < i; ++j) {
                if (indices[j] == index) {
                    index = (index + 1) % M;
                    j = 0; // Restart checking for duplicates
                }
            }

            indices[i] = index;
        }

        return indices;
    }
    function getCouncilVotes(uint proposalId)
        private view returns(CouncilVotes[] memory){
        uint i = 0;
        Vote[] memory vs = proposalVotes[proposalId];
        CouncilVotes[] memory cvs = new CouncilVotes[](5);
        while(i < vs.length){
            Vote memory vt = vs[i];
            Council memory council = councils[nationsCouncil[vt.member]];
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
                uint b = 0;
                int rrs = 0;
                int[] memory rds = new int[](cgv.votes.length);
                while(b < cgv.votes.length){
                    Vote memory vv = cgv.votes[b];
                    int vvn = 0;
                    if(vv.voteCasted)
                        vvn = 1;
                    else
                        vvn = -1;
                    rrs = multiply(Math.mulDiv(100000, cv.votingParameters.voteNumerator, cv.votingParameters.voteDenominator), vvn);
                    rds[b] = rrs;
                    b++;
                }
                if(cv.votingParameters.avgVotes)
                    cgv.score += calculateAverage(rds);
                else
                    cgv.score += rrs;
                
                int v = cgv.score;
                int rr = multiply(Math.mulDiv(100000, cv.votingParameters.sumNumerator, cv.votingParameters.sumDenominator), v);
                results[g] = rr;
                result += rr;
                cv.votes[g] = cgv;
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
        if(councils[councilGroups[request.groupId]].groupsCount == 0)
            revert(string(abi.encodePacked("Invalid Group", Strings.toString(request.groupId))));
        if(totalNations == 0){
            MembershipProposal memory prop = constructMembershipProposal(request);
            acceptNewMember(prop);
            return prop.id;
        }
        else if(nations[request.member].id == request.member)
            revert("Nation Already a Member");
        else if(memberMembershipProposal[request.member] > 0)
        {
            MembershipProposal memory prop = membershipProposals[memberMembershipProposal[request.member]];
            Proposal memory proposal = proposals[proposalToMembershipProposal[prop.id]];
            if(proposal.status == ApprovalStatus.Pending || proposal.status == ApprovalStatus.Ready)
                revert("Outstanding proposal");
            else if(proposal.status == ApprovalStatus.Approved)
                return prop.id;
            else if(proposal.status == ApprovalStatus.Rejected)
                return constructMembershipProposal(request).id;
        }
        else
            return constructMembershipProposal(request).id;
        return 0;
    }
    function populateProposal(Proposal memory prop)
        private
    {
        proposals[prop.id] = prop;
         proposalIds.push(prop.id);
    }
    function constructMembershipProposal(MembershipProposalRequest memory request) 
        private returns(MembershipProposal memory)
        {
            if(request.duration < MIN_VOTE_DURATION){
                request.duration = MIN_VOTE_DURATION;
            }
            Proposal memory proposal = Proposal(
                    ++totalProposals, 
                    ProposalTypes.Membership,
                    block.timestamp + request.duration,
                    ApprovalStatus.Pending,
                    block.timestamp,
                    false,
                    0);
            populateProposal(proposal);
            MembershipProposal memory prop = MembershipProposal(
                ++totalMembershipProposals,
                proposal.id,
                request.newNation,
                request.groupId
                );
         
         membershipProposals[prop.id] = prop;
         membershipProposalIds.push(prop.id);
         memberMembershipProposal[prop.newNation.id] = prop.id;
         proposalToMembershipProposal[proposal.id] = prop.id;
         membershipProposalToProposal[prop.id] = proposal.id;
         return prop;
    }
    function acceptNewMember(MembershipProposal memory proposal) 
    private {
        Council storage targetCouncil = councils[councilGroups[proposal.groupId]];
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
        nationsCouncil[proposal.newNation.id] = targetCouncil.role;
        group.members.push(proposal.newNation);
        _grantRole(targetCouncil.role, proposal.newNation.id);
    }
}