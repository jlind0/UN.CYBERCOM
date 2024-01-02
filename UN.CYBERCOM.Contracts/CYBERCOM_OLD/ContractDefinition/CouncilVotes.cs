using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace UN.CYBERCOM.Contracts.CYBERCOM.OLD.ContractDefinition
{
    public partial class CouncilVotes : CouncilVotesBase { }

    public class CouncilVotesBase 
    {
        [Parameter("bytes32", "councilId", 1)]
        public virtual byte[] CouncilId { get; set; }
        [Parameter("tuple", "votingParameters", 2)]
        public virtual VotingParameters VotingParameters { get; set; }
        [Parameter("tuple[]", "votes", 3)]
        public virtual List<CouncilGroupVotes> Votes { get; set; }
        [Parameter("int256", "score", 4)]
        public virtual BigInteger Score { get; set; }
    }
}
