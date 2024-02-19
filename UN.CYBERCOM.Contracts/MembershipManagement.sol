// SPDX-License-Identifier: MIT
pragma solidity ^0.8.7;

import "@openzeppelin/contracts/utils/Strings.sol";

library MembershipManagement {
    struct MembershipProposalRequest {
        address member;
        Nation newNation;
        uint groupId;
        uint duration;
    }

    struct Nation {
        address id;
        string name;
    }
    struct Vote{
        address member;
        bool voteCasted;
        uint timestamp;
        uint proposalId;
    }
    struct Council{
        string name;
        bytes32 role;
        VotingParameters votingParameters;
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
        uint32 outputCountForGroup;
        uint32 outputCountForMember;
        uint voteDenominator;
        uint voteNumerator;
        uint sumDenominator;
        uint sumNumerator;
        bool avgVotes;
    }
    struct MembershipProposalResponse{
        uint id;
        address member;
        Nation newNation;
        bytes32 council;
        uint groupId;
        Vote[] votes;
        uint duration;
        ApprovalStatus status;
        bool isProcessing;
        bool votingStarted;
        address owner;
        address proposalAddress;
    }
    struct Doc{
        string  title;
        string  url;
        bytes32  dochash;
        bytes  signature;
        address  signer;
        address docAddress;
    }
    struct CouncilVotes{
        bytes32 councilId;
        MembershipManagement.VotingParameters votingParameters;
        CouncilGroupVotes[] votes;
        int score;
    }
    struct CouncilGroupVotes{
        uint groupId;
        Vote[] votes;
        int score;
    }
    enum ProposalTypes {
        Membership,
        MinVoteDuration,
        UpdateVotingParameters
    }
    enum ApprovalStatus {
        Entered,
        Pending,
        Ready,
        Approved,
        Rejected
    }
    struct TallyResult{
        CouncilVotes[] acceptedVotes;
        int score;
        ApprovalStatus status;
        uint proposalId;
    }
}
