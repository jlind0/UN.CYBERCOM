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
using UN.CYBERCOM.Contracts.MembershipRemovalManager.ContractDefinition;

namespace UN.CYBERCOM.Contracts.MembershipRemovalManager
{
    public partial class MembershipRemovalManagerService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, MembershipRemovalManagerDeployment membershipRemovalManagerDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<MembershipRemovalManagerDeployment>().SendRequestAndWaitForReceiptAsync(membershipRemovalManagerDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, MembershipRemovalManagerDeployment membershipRemovalManagerDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<MembershipRemovalManagerDeployment>().SendRequestAsync(membershipRemovalManagerDeployment);
        }

        public static async Task<MembershipRemovalManagerService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, MembershipRemovalManagerDeployment membershipRemovalManagerDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, membershipRemovalManagerDeployment, cancellationTokenSource);
            return new MembershipRemovalManagerService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public MembershipRemovalManagerService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public MembershipRemovalManagerService(Nethereum.Web3.IWeb3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<GetMembershipRemovalRequestsOutputDTO> GetMembershipRemovalRequestsQueryAsync(GetMembershipRemovalRequestsFunction getMembershipRemovalRequestsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetMembershipRemovalRequestsFunction, GetMembershipRemovalRequestsOutputDTO>(getMembershipRemovalRequestsFunction, blockParameter);
        }

        public Task<GetMembershipRemovalRequestsOutputDTO> GetMembershipRemovalRequestsQueryAsync(byte status, BlockParameter blockParameter = null)
        {
            var getMembershipRemovalRequestsFunction = new GetMembershipRemovalRequestsFunction();
                getMembershipRemovalRequestsFunction.Status = status;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetMembershipRemovalRequestsFunction, GetMembershipRemovalRequestsOutputDTO>(getMembershipRemovalRequestsFunction, blockParameter);
        }

        public Task<string> SubmitProposalRequestAsync(SubmitProposalFunction submitProposalFunction)
        {
             return ContractHandler.SendRequestAsync(submitProposalFunction);
        }

        public Task<TransactionReceipt> SubmitProposalRequestAndWaitForReceiptAsync(SubmitProposalFunction submitProposalFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(submitProposalFunction, cancellationToken);
        }

        public Task<string> SubmitProposalRequestAsync(MembershipRemovalRequest request)
        {
            var submitProposalFunction = new SubmitProposalFunction();
                submitProposalFunction.Request = request;
            
             return ContractHandler.SendRequestAsync(submitProposalFunction);
        }

        public Task<TransactionReceipt> SubmitProposalRequestAndWaitForReceiptAsync(MembershipRemovalRequest request, CancellationTokenSource cancellationToken = null)
        {
            var submitProposalFunction = new SubmitProposalFunction();
                submitProposalFunction.Request = request;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(submitProposalFunction, cancellationToken);
        }
    }
}
