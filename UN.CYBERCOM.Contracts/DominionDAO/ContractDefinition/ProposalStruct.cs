using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace UN.CYBERCOM.Contracts.DominionDAO.ContractDefinition
{
    public partial class ProposalStruct : ProposalStructBase { }

    public class ProposalStructBase 
    {
        [Parameter("uint256", "id", 1)]
        public virtual BigInteger Id { get; set; }
        [Parameter("uint256", "amount", 2)]
        public virtual BigInteger Amount { get; set; }
        [Parameter("uint256", "duration", 3)]
        public virtual BigInteger Duration { get; set; }
        [Parameter("uint256", "upvotes", 4)]
        public virtual BigInteger Upvotes { get; set; }
        [Parameter("uint256", "downvotes", 5)]
        public virtual BigInteger Downvotes { get; set; }
        [Parameter("string", "title", 6)]
        public virtual string Title { get; set; }
        [Parameter("string", "description", 7)]
        public virtual string Description { get; set; }
        [Parameter("bool", "passed", 8)]
        public virtual bool Passed { get; set; }
        [Parameter("bool", "paid", 9)]
        public virtual bool Paid { get; set; }
        [Parameter("address", "beneficiary", 10)]
        public virtual string Beneficiary { get; set; }
        [Parameter("address", "proposer", 11)]
        public virtual string Proposer { get; set; }
        [Parameter("address", "executor", 12)]
        public virtual string Executor { get; set; }
    }
}
