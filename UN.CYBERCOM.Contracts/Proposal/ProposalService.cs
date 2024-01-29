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
using UN.CYBERCOM.Contracts.Proposal.ContractDefinition;

namespace UN.CYBERCOM.Contracts.Proposal
{
    public partial class ProposalService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, ProposalDeployment proposalDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<ProposalDeployment>().SendRequestAndWaitForReceiptAsync(proposalDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, ProposalDeployment proposalDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<ProposalDeployment>().SendRequestAsync(proposalDeployment);
        }

        public static async Task<ProposalService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, ProposalDeployment proposalDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, proposalDeployment, cancellationTokenSource);
            return new ProposalService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public ProposalService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public ProposalService(Nethereum.Web3.IWeb3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<BigInteger> DurationQueryAsync(DurationFunction durationFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DurationFunction, BigInteger>(durationFunction, blockParameter);
        }

        
        public Task<BigInteger> DurationQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DurationFunction, BigInteger>(null, blockParameter);
        }

        public Task<GetVotesOutputDTO> GetVotesQueryAsync(GetVotesFunction getVotesFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetVotesFunction, GetVotesOutputDTO>(getVotesFunction, blockParameter);
        }

        public Task<GetVotesOutputDTO> GetVotesQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetVotesFunction, GetVotesOutputDTO>(null, blockParameter);
        }

        public Task<BigInteger> IdQueryAsync(IdFunction idFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<IdFunction, BigInteger>(idFunction, blockParameter);
        }

        
        public Task<BigInteger> IdQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<IdFunction, BigInteger>(null, blockParameter);
        }

        public Task<bool> IsProcessingQueryAsync(IsProcessingFunction isProcessingFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<IsProcessingFunction, bool>(isProcessingFunction, blockParameter);
        }

        
        public Task<bool> IsProcessingQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<IsProcessingFunction, bool>(null, blockParameter);
        }

        public Task<byte> ProposalTypeQueryAsync(ProposalTypeFunction proposalTypeFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ProposalTypeFunction, byte>(proposalTypeFunction, blockParameter);
        }

        
        public Task<byte> ProposalTypeQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ProposalTypeFunction, byte>(null, blockParameter);
        }

        public Task<BigInteger> RandomNumberQueryAsync(RandomNumberFunction randomNumberFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<RandomNumberFunction, BigInteger>(randomNumberFunction, blockParameter);
        }

        
        public Task<BigInteger> RandomNumberQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<RandomNumberFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> SetProcessingRequestAsync(SetProcessingFunction setProcessingFunction)
        {
             return ContractHandler.SendRequestAsync(setProcessingFunction);
        }

        public Task<TransactionReceipt> SetProcessingRequestAndWaitForReceiptAsync(SetProcessingFunction setProcessingFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setProcessingFunction, cancellationToken);
        }

        public Task<string> SetProcessingRequestAsync(bool processing)
        {
            var setProcessingFunction = new SetProcessingFunction();
                setProcessingFunction.Processing = processing;
            
             return ContractHandler.SendRequestAsync(setProcessingFunction);
        }

        public Task<TransactionReceipt> SetProcessingRequestAndWaitForReceiptAsync(bool processing, CancellationTokenSource cancellationToken = null)
        {
            var setProcessingFunction = new SetProcessingFunction();
                setProcessingFunction.Processing = processing;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setProcessingFunction, cancellationToken);
        }

        public Task<string> SetRandomNumberRequestAsync(SetRandomNumberFunction setRandomNumberFunction)
        {
             return ContractHandler.SendRequestAsync(setRandomNumberFunction);
        }

        public Task<TransactionReceipt> SetRandomNumberRequestAndWaitForReceiptAsync(SetRandomNumberFunction setRandomNumberFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setRandomNumberFunction, cancellationToken);
        }

        public Task<string> SetRandomNumberRequestAsync(BigInteger random)
        {
            var setRandomNumberFunction = new SetRandomNumberFunction();
                setRandomNumberFunction.Random = random;
            
             return ContractHandler.SendRequestAsync(setRandomNumberFunction);
        }

        public Task<TransactionReceipt> SetRandomNumberRequestAndWaitForReceiptAsync(BigInteger random, CancellationTokenSource cancellationToken = null)
        {
            var setRandomNumberFunction = new SetRandomNumberFunction();
                setRandomNumberFunction.Random = random;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setRandomNumberFunction, cancellationToken);
        }

        public Task<byte> StatusQueryAsync(StatusFunction statusFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<StatusFunction, byte>(statusFunction, blockParameter);
        }

        
        public Task<byte> StatusQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<StatusFunction, byte>(null, blockParameter);
        }

        public Task<BigInteger> TimestampQueryAsync(TimestampFunction timestampFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TimestampFunction, BigInteger>(timestampFunction, blockParameter);
        }

        
        public Task<BigInteger> TimestampQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TimestampFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> UpdateStatusRequestAsync(UpdateStatusFunction updateStatusFunction)
        {
             return ContractHandler.SendRequestAsync(updateStatusFunction);
        }

        public Task<TransactionReceipt> UpdateStatusRequestAndWaitForReceiptAsync(UpdateStatusFunction updateStatusFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(updateStatusFunction, cancellationToken);
        }

        public Task<string> UpdateStatusRequestAsync(byte status)
        {
            var updateStatusFunction = new UpdateStatusFunction();
                updateStatusFunction.Status = status;
            
             return ContractHandler.SendRequestAsync(updateStatusFunction);
        }

        public Task<TransactionReceipt> UpdateStatusRequestAndWaitForReceiptAsync(byte status, CancellationTokenSource cancellationToken = null)
        {
            var updateStatusFunction = new UpdateStatusFunction();
                updateStatusFunction.Status = status;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(updateStatusFunction, cancellationToken);
        }

        public Task<string> VoteRequestAsync(VoteFunction voteFunction)
        {
             return ContractHandler.SendRequestAsync(voteFunction);
        }

        public Task<TransactionReceipt> VoteRequestAndWaitForReceiptAsync(VoteFunction voteFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(voteFunction, cancellationToken);
        }

        public Task<string> VoteRequestAsync(bool voteCasted, string member)
        {
            var voteFunction = new VoteFunction();
                voteFunction.VoteCasted = voteCasted;
                voteFunction.Member = member;
            
             return ContractHandler.SendRequestAsync(voteFunction);
        }

        public Task<TransactionReceipt> VoteRequestAndWaitForReceiptAsync(bool voteCasted, string member, CancellationTokenSource cancellationToken = null)
        {
            var voteFunction = new VoteFunction();
                voteFunction.VoteCasted = voteCasted;
                voteFunction.Member = member;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(voteFunction, cancellationToken);
        }
    }
}
