using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace UN.CYBERCOM.Contracts.CouncilManager.ContractDefinition
{
    public partial class CouncilGroupVotes : CouncilGroupVotesBase { }

    public class CouncilGroupVotesBase 
    {
        [Parameter("uint256", "groupId", 1)]
        public virtual BigInteger GroupId { get; set; }
        [Parameter("tuple[]", "votes", 2)]
        public virtual List<Vote> Votes { get; set; }
        [Parameter("int256", "score", 3)]
        public virtual BigInteger Score { get; set; }
    }
}
