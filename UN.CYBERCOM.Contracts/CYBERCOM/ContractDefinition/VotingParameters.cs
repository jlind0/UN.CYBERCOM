using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace UN.CYBERCOM.Contracts.CYBERCOM.ContractDefinition
{
    public partial class VotingParameters : VotingParametersBase { }

    public class VotingParametersBase 
    {
        [Parameter("bool", "randomizeByGroup", 1)]
        public virtual bool RandomizeByGroup { get; set; }
        [Parameter("bool", "randomizeByMember", 2)]
        public virtual bool RandomizeByMember { get; set; }
        [Parameter("uint256", "outputCountForGroup", 3)]
        public virtual BigInteger OutputCountForGroup { get; set; }
        [Parameter("uint256", "outputCountForMember", 4)]
        public virtual BigInteger OutputCountForMember { get; set; }
        [Parameter("int32", "voteDenominator", 5)]
        public virtual int VoteDenominator { get; set; }
        [Parameter("int32", "voteNumerator", 6)]
        public virtual int VoteNumerator { get; set; }
        [Parameter("int32", "sumDenominator", 7)]
        public virtual int SumDenominator { get; set; }
        [Parameter("int32", "sumNumerator", 8)]
        public virtual int SumNumerator { get; set; }
        [Parameter("bool", "avgVotes", 9)]
        public virtual bool AvgVotes { get; set; }
    }
}
