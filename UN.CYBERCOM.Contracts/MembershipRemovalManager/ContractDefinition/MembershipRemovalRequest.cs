using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace UN.CYBERCOM.Contracts.MembershipRemovalManager.ContractDefinition
{
    public partial class MembershipRemovalRequest : MembershipRemovalRequestBase { }

    public class MembershipRemovalRequestBase 
    {
        [Parameter("address", "nationToRemove", 1)]
        public virtual string NationToRemove { get; set; }
        [Parameter("uint256", "duration", 2)]
        public virtual BigInteger Duration { get; set; }
        [Parameter("address", "owner", 3)]
        public virtual string Owner { get; set; }
    }
}
