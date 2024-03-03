using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;

namespace UN.CYBERCOM.Contracts.MembershipManagement.ContractDefinition
{


    public partial class MembershipManagementDeployment : MembershipManagementDeploymentBase
    {
        public MembershipManagementDeployment() : base(BYTECODE) { }
        public MembershipManagementDeployment(string byteCode) : base(byteCode) { }
    }

    public class MembershipManagementDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "60556032600b8282823980515f1a607314602657634e487b7160e01b5f525f60045260245ffd5b305f52607381538281f3fe730000000000000000000000000000000000000000301460806040525f80fdfea2646970667358221220204396cc44946c15842ad630d446b2a5e6ebd34bb045bf89880bab1a879bb51d64736f6c63430008170033";
        public MembershipManagementDeploymentBase() : base(BYTECODE) { }
        public MembershipManagementDeploymentBase(string byteCode) : base(byteCode) { }

    }
}
