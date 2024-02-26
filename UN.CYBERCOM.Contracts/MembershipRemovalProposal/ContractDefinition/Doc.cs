using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace UN.CYBERCOM.Contracts.MembershipRemovalProposal.ContractDefinition
{
    public partial class Doc : DocBase { }

    public class DocBase 
    {
        [Parameter("string", "title", 1)]
        public virtual string Title { get; set; }
        [Parameter("string", "url", 2)]
        public virtual string Url { get; set; }
        [Parameter("bytes32", "dochash", 3)]
        public virtual byte[] Dochash { get; set; }
        [Parameter("bytes", "signature", 4)]
        public virtual byte[] Signature { get; set; }
        [Parameter("address", "signer", 5)]
        public virtual string Signer { get; set; }
        [Parameter("address", "docAddress", 6)]
        public virtual string DocAddress { get; set; }
    }
}
