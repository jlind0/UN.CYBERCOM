using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace UN.CYBERCOM.Contracts.MembershipManager.ContractDefinition
{
    public partial class MembershipProposalResponse : MembershipProposalResponseBase { }

    public class MembershipProposalResponseBase 
    {
        [Parameter("uint256", "id", 1)]
        public virtual BigInteger Id { get; set; }
        [Parameter("address", "member", 2)]
        public virtual string Member { get; set; }
        [Parameter("tuple", "newNation", 3)]
        public virtual Nation NewNation { get; set; }
        [Parameter("bytes32", "council", 4)]
        public virtual byte[] Council { get; set; }
        [Parameter("uint256", "groupId", 5)]
        public virtual BigInteger GroupId { get; set; }
        [Parameter("tuple[]", "votes", 6)]
        public virtual List<Vote> Votes { get; set; }
        [Parameter("uint256", "duration", 7)]
        public virtual BigInteger Duration { get; set; }
        [Parameter("uint8", "status", 8)]
        public virtual byte Status { get; set; }
        [Parameter("bool", "isProcessing", 9)]
        public virtual bool IsProcessing { get; set; }
        [Parameter("bool", "votingStarted", 10)]
        public virtual bool VotingStarted { get; set; }
        [Parameter("address", "owner", 11)]
        public virtual string Owner { get; set; }
        [Parameter("address", "proposalAddress", 12)]
        public virtual string ProposalAddress { get; set; }
    }
}
