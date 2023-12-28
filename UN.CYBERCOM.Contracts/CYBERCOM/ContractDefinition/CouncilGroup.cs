using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace UN.CYBERCOM.Contracts.CYBERCOM.ContractDefinition
{
    public partial class CouncilGroup : CouncilGroupBase { }

    public class CouncilGroupBase 
    {
        [Parameter("uint256", "id", 1)]
        public virtual BigInteger Id { get; set; }
        [Parameter("string", "name", 2)]
        public virtual string Name { get; set; }
        [Parameter("tuple[]", "members", 3)]
        public virtual List<Nation> Members { get; set; }
    }
}
