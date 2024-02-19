using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace UN.CYBERCOM.Contracts.CouncilManager.ContractDefinition
{
    public partial class Council : CouncilBase { }

    public class CouncilBase 
    {
        [Parameter("string", "name", 1)]
        public virtual string Name { get; set; }
        [Parameter("bytes32", "role", 2)]
        public virtual byte[] Role { get; set; }
        [Parameter("tuple", "votingParameters", 3)]
        public virtual VotingParameters VotingParameters { get; set; }
        [Parameter("tuple[]", "groups", 4)]
        public virtual List<CouncilGroup> Groups { get; set; }
    }
}