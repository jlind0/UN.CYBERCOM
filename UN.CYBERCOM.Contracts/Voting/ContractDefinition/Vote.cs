using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace UN.CYBERCOM.Contracts.Voting.ContractDefinition
{
    public partial class Vote : VoteBase { }

    public class VoteBase 
    {
        [Parameter("address", "member", 1)]
        public virtual string Member { get; set; }
        [Parameter("bool", "voteCasted", 2)]
        public virtual bool VoteCasted { get; set; }
        [Parameter("uint256", "timestamp", 3)]
        public virtual BigInteger Timestamp { get; set; }
        [Parameter("uint256", "proposalId", 4)]
        public virtual BigInteger ProposalId { get; set; }
    }
}
