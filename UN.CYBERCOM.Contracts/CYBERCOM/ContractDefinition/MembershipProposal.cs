using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace UN.CYBERCOM.Contracts.CYBERCOM.ContractDefinition
{
    public partial class MembershipProposal : MembershipProposalBase { }

    public class MembershipProposalBase 
    {
        [Parameter("uint256", "id", 1)]
        public virtual BigInteger Id { get; set; }
        [Parameter("uint256", "proposalId", 2)]
        public virtual BigInteger ProposalId { get; set; }
        [Parameter("address", "member", 3)]
        public virtual string Member { get; set; }
        [Parameter("tuple", "newNation", 4)]
        public virtual Nation NewNation { get; set; }
        [Parameter("bytes32", "council", 5)]
        public virtual byte[] Council { get; set; }
        [Parameter("uint256", "groupId", 6)]
        public virtual BigInteger GroupId { get; set; }
        [Parameter("uint256", "duration", 7)]
        public virtual BigInteger Duration { get; set; }
        [Parameter("uint8", "status", 8)]
        public virtual byte Status { get; set; }
        [Parameter("uint256", "timestamp", 9)]
        public virtual BigInteger Timestamp { get; set; }
    }
}
