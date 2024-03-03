using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using UN.CYBERCOM.Contracts.MembershipManagement.ContractDefinition;

namespace UN.CYBERCOM.Contracts.MembershipManagement
{
    public partial class MembershipManagementService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, MembershipManagementDeployment membershipManagementDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<MembershipManagementDeployment>().SendRequestAndWaitForReceiptAsync(membershipManagementDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, MembershipManagementDeployment membershipManagementDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<MembershipManagementDeployment>().SendRequestAsync(membershipManagementDeployment);
        }

        public static async Task<MembershipManagementService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, MembershipManagementDeployment membershipManagementDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, membershipManagementDeployment, cancellationTokenSource);
            return new MembershipManagementService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public MembershipManagementService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public MembershipManagementService(Nethereum.Web3.IWeb3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }


    }
}
