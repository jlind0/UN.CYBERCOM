using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace UN.CYBERCOM.Contracts.VotingParametersManager.ContractDefinition
{
    public partial class ChangeVotingParametersRole : ChangeVotingParametersRoleBase { }

    public class ChangeVotingParametersRoleBase 
    {
        [Parameter("bytes32", "council", 1)]
        public virtual byte[] Council { get; set; }
        [Parameter("tuple", "parameters", 2)]
        public virtual VotingParameters Parameters { get; set; }
    }
}
