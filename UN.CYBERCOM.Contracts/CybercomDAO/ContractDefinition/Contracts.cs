using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace UN.CYBERCOM.Contracts.CybercomDAO.ContractDefinition
{
    public partial class Contracts : ContractsBase { }

    public class ContractsBase 
    {
        [Parameter("address", "daoAddress", 1)]
        public virtual string DaoAddress { get; set; }
        [Parameter("address", "votingAddress", 2)]
        public virtual string VotingAddress { get; set; }
        [Parameter("address", "councilManagementAddress", 3)]
        public virtual string CouncilManagementAddress { get; set; }
        [Parameter("address", "proposalStorageAddress", 4)]
        public virtual string ProposalStorageAddress { get; set; }
        [Parameter("address", "membershipRemovalAddress", 5)]
        public virtual string MembershipRemovalAddress { get; set; }
        [Parameter("address", "membershipManagerAddress", 6)]
        public virtual string MembershipManagerAddress { get; set; }
    }
}
