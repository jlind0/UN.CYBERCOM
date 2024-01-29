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
using UN.CYBERCOM.Contracts.Voting.ContractDefinition;

namespace UN.CYBERCOM.Contracts.Voting
{
    public partial class VotingService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, VotingDeployment votingDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<VotingDeployment>().SendRequestAndWaitForReceiptAsync(votingDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, VotingDeployment votingDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<VotingDeployment>().SendRequestAsync(votingDeployment);
        }

        public static async Task<VotingService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, VotingDeployment votingDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, votingDeployment, cancellationTokenSource);
            return new VotingService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public VotingService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public VotingService(Nethereum.Web3.IWeb3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> AddProposalRequestAsync(AddProposalFunction addProposalFunction)
        {
             return ContractHandler.SendRequestAsync(addProposalFunction);
        }

        public Task<TransactionReceipt> AddProposalRequestAndWaitForReceiptAsync(AddProposalFunction addProposalFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addProposalFunction, cancellationToken);
        }

        public Task<string> AddProposalRequestAsync(string proposalAddress)
        {
            var addProposalFunction = new AddProposalFunction();
                addProposalFunction.ProposalAddress = proposalAddress;
            
             return ContractHandler.SendRequestAsync(addProposalFunction);
        }

        public Task<TransactionReceipt> AddProposalRequestAndWaitForReceiptAsync(string proposalAddress, CancellationTokenSource cancellationToken = null)
        {
            var addProposalFunction = new AddProposalFunction();
                addProposalFunction.ProposalAddress = proposalAddress;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addProposalFunction, cancellationToken);
        }

        public Task<GetVoteTallyOutputDTO> GetVoteTallyQueryAsync(GetVoteTallyFunction getVoteTallyFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetVoteTallyFunction, GetVoteTallyOutputDTO>(getVoteTallyFunction, blockParameter);
        }

        public Task<GetVoteTallyOutputDTO> GetVoteTallyQueryAsync(BigInteger proposalId, BlockParameter blockParameter = null)
        {
            var getVoteTallyFunction = new GetVoteTallyFunction();
                getVoteTallyFunction.ProposalId = proposalId;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetVoteTallyFunction, GetVoteTallyOutputDTO>(getVoteTallyFunction, blockParameter);
        }

        public Task<string> PrepareTallyRequestAsync(PrepareTallyFunction prepareTallyFunction)
        {
             return ContractHandler.SendRequestAsync(prepareTallyFunction);
        }

        public Task<TransactionReceipt> PrepareTallyRequestAndWaitForReceiptAsync(PrepareTallyFunction prepareTallyFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(prepareTallyFunction, cancellationToken);
        }

        public Task<string> PrepareTallyRequestAsync(BigInteger proposalId)
        {
            var prepareTallyFunction = new PrepareTallyFunction();
                prepareTallyFunction.ProposalId = proposalId;
            
             return ContractHandler.SendRequestAsync(prepareTallyFunction);
        }

        public Task<TransactionReceipt> PrepareTallyRequestAndWaitForReceiptAsync(BigInteger proposalId, CancellationTokenSource cancellationToken = null)
        {
            var prepareTallyFunction = new PrepareTallyFunction();
                prepareTallyFunction.ProposalId = proposalId;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(prepareTallyFunction, cancellationToken);
        }

        public Task<string> RawFulfillRandomWordsRequestAsync(RawFulfillRandomWordsFunction rawFulfillRandomWordsFunction)
        {
             return ContractHandler.SendRequestAsync(rawFulfillRandomWordsFunction);
        }

        public Task<TransactionReceipt> RawFulfillRandomWordsRequestAndWaitForReceiptAsync(RawFulfillRandomWordsFunction rawFulfillRandomWordsFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(rawFulfillRandomWordsFunction, cancellationToken);
        }

        public Task<string> RawFulfillRandomWordsRequestAsync(BigInteger requestId, List<BigInteger> randomWords)
        {
            var rawFulfillRandomWordsFunction = new RawFulfillRandomWordsFunction();
                rawFulfillRandomWordsFunction.RequestId = requestId;
                rawFulfillRandomWordsFunction.RandomWords = randomWords;
            
             return ContractHandler.SendRequestAsync(rawFulfillRandomWordsFunction);
        }

        public Task<TransactionReceipt> RawFulfillRandomWordsRequestAndWaitForReceiptAsync(BigInteger requestId, List<BigInteger> randomWords, CancellationTokenSource cancellationToken = null)
        {
            var rawFulfillRandomWordsFunction = new RawFulfillRandomWordsFunction();
                rawFulfillRandomWordsFunction.RequestId = requestId;
                rawFulfillRandomWordsFunction.RandomWords = randomWords;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(rawFulfillRandomWordsFunction, cancellationToken);
        }

        public Task<string> TallyVotesRequestAsync(TallyVotesFunction tallyVotesFunction)
        {
             return ContractHandler.SendRequestAsync(tallyVotesFunction);
        }

        public Task<TransactionReceipt> TallyVotesRequestAndWaitForReceiptAsync(TallyVotesFunction tallyVotesFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tallyVotesFunction, cancellationToken);
        }

        public Task<string> TallyVotesRequestAsync(BigInteger proposalId)
        {
            var tallyVotesFunction = new TallyVotesFunction();
                tallyVotesFunction.ProposalId = proposalId;
            
             return ContractHandler.SendRequestAsync(tallyVotesFunction);
        }

        public Task<TransactionReceipt> TallyVotesRequestAndWaitForReceiptAsync(BigInteger proposalId, CancellationTokenSource cancellationToken = null)
        {
            var tallyVotesFunction = new TallyVotesFunction();
                tallyVotesFunction.ProposalId = proposalId;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tallyVotesFunction, cancellationToken);
        }
    }
}
