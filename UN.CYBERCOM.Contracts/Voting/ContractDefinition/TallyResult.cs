using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace UN.CYBERCOM.Contracts.Voting.ContractDefinition
{
    public partial class TallyResult : TallyResultBase { }

    public class TallyResultBase 
    {
        [Parameter("tuple[]", "acceptedVotes", 1)]
        public virtual List<CouncilVotes> AcceptedVotes { get; set; }
        [Parameter("int256", "score", 2)]
        public virtual BigInteger Score { get; set; }
        [Parameter("uint8", "status", 3)]
        public virtual byte Status { get; set; }
        [Parameter("uint256", "proposalId", 4)]
        public virtual BigInteger ProposalId { get; set; }
    }
}
