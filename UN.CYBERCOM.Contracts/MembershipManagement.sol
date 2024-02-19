/// SPDX-License-Identifier: MIT
pragma solidity ^0.8.7;

import "@openzeppelin/contracts/utils/Strings.sol";

library MembershipManagement {
    /**
     * @dev Represents a request to join a council or nation.
     */
    struct MembershipProposalRequest {
        address member;
        Nation newNation;
        uint groupId;
        uint duration;
    }

    /**
     * @dev Represents a nation with an identifier and a name.
     */
    struct Nation {
        address id;
        string name;
    }

    /**
     * @dev Represents a vote cast by a member.
     */
    struct Vote {
        address member;
        bool voteCasted;
        uint timestamp;
        uint proposalId;
    }

    /**
     * @dev Represents a council with its name, role, voting parameters, and member groups.
     */
    struct Council {
        string name;
        bytes32 role;
        VotingParameters votingParameters;
        CouncilGroup[] groups;
    }

    /**
     * @dev Represents a group within a council, consisting of multiple nations.
     */
    struct CouncilGroup {
        uint id;
        string name;
        Nation[] members;
    }

    /**
     * @dev Parameters defining how voting is conducted within a council.
     */
    struct VotingParameters {
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

    /**
     * @dev Response to a membership proposal, including the result and votes.
     */
    struct MembershipProposalResponse {
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

    /**
     * @dev Represents a document with its metadata.
     */
    struct Doc {
        string title;
        string url;
        bytes32 dochash;
        bytes signature;
        address signer;
        address docAddress;
    }

    /**
     * @dev Represents the votes and score for a specific council.
     */
    struct CouncilVotes {
        bytes32 councilId;
        VotingParameters votingParameters;
        CouncilGroupVotes[] votes;
        int score;
    }

    /**
     * @dev Represents the votes and score for a council group.
     */
    struct CouncilGroupVotes {
        uint groupId;
        Vote[] votes;
        int score;
    }

    /**
     * @dev Enumerates the different types of proposals.
     */
    enum ProposalTypes {
        Membership,
        MinVoteDuration,
        UpdateVotingParameters
    }

    /**
     * @dev Enumerates the different statuses of a proposal.
     */
    enum ApprovalStatus {
        Entered,
        Pending,
        Ready,
        Approved,
        Rejected
    }

    /**
     * @dev Represents the result of vote tallying for a proposal.
     */
    struct TallyResult {
        CouncilVotes[] acceptedVotes;
        int score;
        ApprovalStatus status;
        uint proposalId;
    }
}
