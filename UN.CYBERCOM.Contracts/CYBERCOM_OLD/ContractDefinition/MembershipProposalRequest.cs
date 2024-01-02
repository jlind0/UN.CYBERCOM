using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace UN.CYBERCOM.Contracts.CYBERCOM.OLD.ContractDefinition
{
    public partial class MembershipProposalRequest : MembershipProposalRequestBase { }

    public class MembershipProposalRequestBase 
    {
        [Parameter("address", "member", 1)]
        public virtual string Member { get; set; }
        [Parameter("tuple", "newNation", 2)]
        public virtual Nation NewNation { get; set; }
        [Parameter("uint256", "groupId", 3)]
        public virtual BigInteger GroupId { get; set; }
        [Parameter("uint256", "duration", 4)]
        public virtual BigInteger Duration { get; set; }
    }
}
