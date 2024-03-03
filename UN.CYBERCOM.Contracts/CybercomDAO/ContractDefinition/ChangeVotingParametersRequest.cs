using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace UN.CYBERCOM.Contracts.CybercomDAO.ContractDefinition
{
    public partial class ChangeVotingParametersRequest : ChangeVotingParametersRequestBase { }

    public class ChangeVotingParametersRequestBase 
    {
        [Parameter("tuple[]", "parameters", 1)]
        public virtual List<ChangeVotingParametersRole> Parameters { get; set; }
        [Parameter("uint256", "duration", 2)]
        public virtual BigInteger Duration { get; set; }
        [Parameter("address", "owner", 3)]
        public virtual string Owner { get; set; }
    }
}
