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
using UN.CYBERCOM.Contracts.VotingParametersManager.ContractDefinition;

namespace UN.CYBERCOM.Contracts.VotingParametersManager
{
    public partial class VotingParametersManagerService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, VotingParametersManagerDeployment votingParametersManagerDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<VotingParametersManagerDeployment>().SendRequestAndWaitForReceiptAsync(votingParametersManagerDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, VotingParametersManagerDeployment votingParametersManagerDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<VotingParametersManagerDeployment>().SendRequestAsync(votingParametersManagerDeployment);
        }

        public static async Task<VotingParametersManagerService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, VotingParametersManagerDeployment votingParametersManagerDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, votingParametersManagerDeployment, cancellationTokenSource);
            return new VotingParametersManagerService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public VotingParametersManagerService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public VotingParametersManagerService(Nethereum.Web3.IWeb3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<GetRequestsOutputDTO> GetRequestsQueryAsync(GetRequestsFunction getRequestsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetRequestsFunction, GetRequestsOutputDTO>(getRequestsFunction, blockParameter);
        }

        public Task<GetRequestsOutputDTO> GetRequestsQueryAsync(byte status, BlockParameter blockParameter = null)
        {
            var getRequestsFunction = new GetRequestsFunction();
                getRequestsFunction.Status = status;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetRequestsFunction, GetRequestsOutputDTO>(getRequestsFunction, blockParameter);
        }

        public Task<string> SubmitProposalRequestAsync(SubmitProposalFunction submitProposalFunction)
        {
             return ContractHandler.SendRequestAsync(submitProposalFunction);
        }

        public Task<TransactionReceipt> SubmitProposalRequestAndWaitForReceiptAsync(SubmitProposalFunction submitProposalFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(submitProposalFunction, cancellationToken);
        }

        public Task<string> SubmitProposalRequestAsync(ChangeVotingParametersRequest request)
        {
            var submitProposalFunction = new SubmitProposalFunction();
                submitProposalFunction.Request = request;
            
             return ContractHandler.SendRequestAsync(submitProposalFunction);
        }

        public Task<TransactionReceipt> SubmitProposalRequestAndWaitForReceiptAsync(ChangeVotingParametersRequest request, CancellationTokenSource cancellationToken = null)
        {
            var submitProposalFunction = new SubmitProposalFunction();
                submitProposalFunction.Request = request;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(submitProposalFunction, cancellationToken);
        }
    }
}
