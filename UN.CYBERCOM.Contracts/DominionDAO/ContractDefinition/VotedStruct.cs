using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace UN.CYBERCOM.Contracts.DominionDAO.ContractDefinition
{
    public partial class VotedStruct : VotedStructBase { }

    public class VotedStructBase 
    {
        [Parameter("address", "voter", 1)]
        public virtual string Voter { get; set; }
        [Parameter("uint256", "timestamp", 2)]
        public virtual BigInteger Timestamp { get; set; }
        [Parameter("bool", "choosen", 3)]
        public virtual bool Choosen { get; set; }
    }
}
