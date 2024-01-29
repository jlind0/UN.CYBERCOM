using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace UN.CYBERCOM.Contracts.CYBERCOM.ContractDefinition
{
    public partial class Proposal : ProposalBase { }

    public class ProposalBase 
    {
        [Parameter("uint256", "id", 1)]
        public virtual BigInteger Id { get; set; }
        [Parameter("uint8", "proposalType", 2)]
        public virtual byte ProposalType { get; set; }
        [Parameter("uint256", "duration", 3)]
        public virtual BigInteger Duration { get; set; }
        [Parameter("uint8", "status", 4)]
        public virtual byte Status { get; set; }
        [Parameter("uint256", "timestamp", 5)]
        public virtual BigInteger Timestamp { get; set; }
        [Parameter("bool", "isProcessing", 6)]
        public virtual bool IsProcessing { get; set; }
        [Parameter("uint256", "randomNumber", 7)]
        public virtual BigInteger RandomNumber { get; set; }
    }
}
