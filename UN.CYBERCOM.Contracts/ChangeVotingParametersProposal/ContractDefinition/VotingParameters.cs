using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace UN.CYBERCOM.Contracts.ChangeVotingParametersProposal.ContractDefinition
{
    public partial class VotingParameters : VotingParametersBase { }

    public class VotingParametersBase 
    {
        [Parameter("bool", "randomizeByGroup", 1)]
        public virtual bool RandomizeByGroup { get; set; }
        [Parameter("bool", "randomizeByMember", 2)]
        public virtual bool RandomizeByMember { get; set; }
        [Parameter("uint32", "outputCountForGroup", 3)]
        public virtual uint OutputCountForGroup { get; set; }
        [Parameter("uint32", "outputCountForMember", 4)]
        public virtual uint OutputCountForMember { get; set; }
        [Parameter("uint256", "voteDenominator", 5)]
        public virtual BigInteger VoteDenominator { get; set; }
        [Parameter("uint256", "voteNumerator", 6)]
        public virtual BigInteger VoteNumerator { get; set; }
        [Parameter("uint256", "sumDenominator", 7)]
        public virtual BigInteger SumDenominator { get; set; }
        [Parameter("uint256", "sumNumerator", 8)]
        public virtual BigInteger SumNumerator { get; set; }
        [Parameter("bool", "avgVotes", 9)]
        public virtual bool AvgVotes { get; set; }
    }
}
