using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace UN.CYBERCOM.Contracts.CybercomDAO.ContractDefinition
{
    public partial class MembershipRemovalResponse : MembershipRemovalResponseBase { }

    public class MembershipRemovalResponseBase 
    {
        [Parameter("uint256", "id", 1)]
        public virtual BigInteger Id { get; set; }
        [Parameter("tuple", "nationToRemove", 2)]
        public virtual Nation NationToRemove { get; set; }
        [Parameter("tuple[]", "votes", 3)]
        public virtual List<Vote> Votes { get; set; }
        [Parameter("uint256", "duration", 4)]
        public virtual BigInteger Duration { get; set; }
        [Parameter("uint8", "status", 5)]
        public virtual byte Status { get; set; }
        [Parameter("bool", "isProcessing", 6)]
        public virtual bool IsProcessing { get; set; }
        [Parameter("bool", "votingStarted", 7)]
        public virtual bool VotingStarted { get; set; }
        [Parameter("address", "owner", 8)]
        public virtual string Owner { get; set; }
        [Parameter("address", "proposalAddress", 9)]
        public virtual string ProposalAddress { get; set; }
    }
}
