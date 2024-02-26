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
using UN.CYBERCOM.Contracts.MembershipManager.ContractDefinition;

namespace UN.CYBERCOM.Contracts.MembershipManager
{
    public partial class MembershipManagerService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, MembershipManagerDeployment membershipManagerDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<MembershipManagerDeployment>().SendRequestAndWaitForReceiptAsync(membershipManagerDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, MembershipManagerDeployment membershipManagerDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<MembershipManagerDeployment>().SendRequestAsync(membershipManagerDeployment);
        }

        public static async Task<MembershipManagerService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, MembershipManagerDeployment membershipManagerDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, membershipManagerDeployment, cancellationTokenSource);
            return new MembershipManagerService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public MembershipManagerService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public MembershipManagerService(Nethereum.Web3.IWeb3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<GetMembershipRequestsOutputDTO> GetMembershipRequestsQueryAsync(GetMembershipRequestsFunction getMembershipRequestsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetMembershipRequestsFunction, GetMembershipRequestsOutputDTO>(getMembershipRequestsFunction, blockParameter);
        }

        public Task<GetMembershipRequestsOutputDTO> GetMembershipRequestsQueryAsync(byte status, BlockParameter blockParameter = null)
        {
            var getMembershipRequestsFunction = new GetMembershipRequestsFunction();
                getMembershipRequestsFunction.Status = status;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetMembershipRequestsFunction, GetMembershipRequestsOutputDTO>(getMembershipRequestsFunction, blockParameter);
        }

        public Task<string> SubmitMembershipProposalRequestAsync(SubmitMembershipProposalFunction submitMembershipProposalFunction)
        {
             return ContractHandler.SendRequestAsync(submitMembershipProposalFunction);
        }

        public Task<TransactionReceipt> SubmitMembershipProposalRequestAndWaitForReceiptAsync(SubmitMembershipProposalFunction submitMembershipProposalFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(submitMembershipProposalFunction, cancellationToken);
        }

        public Task<string> SubmitMembershipProposalRequestAsync(MembershipProposalRequest request)
        {
            var submitMembershipProposalFunction = new SubmitMembershipProposalFunction();
                submitMembershipProposalFunction.Request = request;
            
             return ContractHandler.SendRequestAsync(submitMembershipProposalFunction);
        }

        public Task<TransactionReceipt> SubmitMembershipProposalRequestAndWaitForReceiptAsync(MembershipProposalRequest request, CancellationTokenSource cancellationToken = null)
        {
            var submitMembershipProposalFunction = new SubmitMembershipProposalFunction();
                submitMembershipProposalFunction.Request = request;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(submitMembershipProposalFunction, cancellationToken);
        }
    }
}
