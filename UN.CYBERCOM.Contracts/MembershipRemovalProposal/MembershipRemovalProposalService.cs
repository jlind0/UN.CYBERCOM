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
using UN.CYBERCOM.Contracts.MembershipRemovalProposal.ContractDefinition;

namespace UN.CYBERCOM.Contracts.MembershipRemovalProposal
{
    public partial class MembershipRemovalProposalService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, MembershipRemovalProposalDeployment membershipRemovalProposalDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<MembershipRemovalProposalDeployment>().SendRequestAndWaitForReceiptAsync(membershipRemovalProposalDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, MembershipRemovalProposalDeployment membershipRemovalProposalDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<MembershipRemovalProposalDeployment>().SendRequestAsync(membershipRemovalProposalDeployment);
        }

        public static async Task<MembershipRemovalProposalService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, MembershipRemovalProposalDeployment membershipRemovalProposalDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, membershipRemovalProposalDeployment, cancellationTokenSource);
            return new MembershipRemovalProposalService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public MembershipRemovalProposalService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public MembershipRemovalProposalService(Nethereum.Web3.IWeb3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> AddDocumentRequestAsync(AddDocumentFunction addDocumentFunction)
        {
             return ContractHandler.SendRequestAsync(addDocumentFunction);
        }

        public Task<TransactionReceipt> AddDocumentRequestAndWaitForReceiptAsync(AddDocumentFunction addDocumentFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addDocumentFunction, cancellationToken);
        }

        public Task<string> AddDocumentRequestAsync(string signer, string title, string url, byte[] docHash, byte[] signature)
        {
            var addDocumentFunction = new AddDocumentFunction();
                addDocumentFunction.Signer = signer;
                addDocumentFunction.Title = title;
                addDocumentFunction.Url = url;
                addDocumentFunction.DocHash = docHash;
                addDocumentFunction.Signature = signature;
            
             return ContractHandler.SendRequestAsync(addDocumentFunction);
        }

        public Task<TransactionReceipt> AddDocumentRequestAndWaitForReceiptAsync(string signer, string title, string url, byte[] docHash, byte[] signature, CancellationTokenSource cancellationToken = null)
        {
            var addDocumentFunction = new AddDocumentFunction();
                addDocumentFunction.Signer = signer;
                addDocumentFunction.Title = title;
                addDocumentFunction.Url = url;
                addDocumentFunction.DocHash = docHash;
                addDocumentFunction.Signature = signature;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addDocumentFunction, cancellationToken);
        }

        public Task<BigInteger> DurationQueryAsync(DurationFunction durationFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DurationFunction, BigInteger>(durationFunction, blockParameter);
        }

        
        public Task<BigInteger> DurationQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DurationFunction, BigInteger>(null, blockParameter);
        }

        public Task<GetDocumentsOutputDTO> GetDocumentsQueryAsync(GetDocumentsFunction getDocumentsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetDocumentsFunction, GetDocumentsOutputDTO>(getDocumentsFunction, blockParameter);
        }

        public Task<GetDocumentsOutputDTO> GetDocumentsQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetDocumentsFunction, GetDocumentsOutputDTO>(null, blockParameter);
        }

        public Task<GetMembershipResponseOutputDTO> GetMembershipResponseQueryAsync(GetMembershipResponseFunction getMembershipResponseFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetMembershipResponseFunction, GetMembershipResponseOutputDTO>(getMembershipResponseFunction, blockParameter);
        }

        public Task<GetMembershipResponseOutputDTO> GetMembershipResponseQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetMembershipResponseFunction, GetMembershipResponseOutputDTO>(null, blockParameter);
        }

        public Task<GetNationOutputDTO> GetNationQueryAsync(GetNationFunction getNationFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetNationFunction, GetNationOutputDTO>(getNationFunction, blockParameter);
        }

        public Task<GetNationOutputDTO> GetNationQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetNationFunction, GetNationOutputDTO>(null, blockParameter);
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

        public Task<string> OwnerQueryAsync(OwnerFunction ownerFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OwnerFunction, string>(ownerFunction, blockParameter);
        }

        
        public Task<string> OwnerQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OwnerFunction, string>(null, blockParameter);
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

        public Task<string> StartVotingRequestAsync(StartVotingFunction startVotingFunction)
        {
             return ContractHandler.SendRequestAsync(startVotingFunction);
        }

        public Task<TransactionReceipt> StartVotingRequestAndWaitForReceiptAsync(StartVotingFunction startVotingFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(startVotingFunction, cancellationToken);
        }

        public Task<string> StartVotingRequestAsync(string sender)
        {
            var startVotingFunction = new StartVotingFunction();
                startVotingFunction.Sender = sender;
            
             return ContractHandler.SendRequestAsync(startVotingFunction);
        }

        public Task<TransactionReceipt> StartVotingRequestAndWaitForReceiptAsync(string sender, CancellationTokenSource cancellationToken = null)
        {
            var startVotingFunction = new StartVotingFunction();
                startVotingFunction.Sender = sender;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(startVotingFunction, cancellationToken);
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

        public Task<bool> VotingStartedQueryAsync(VotingStartedFunction votingStartedFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<VotingStartedFunction, bool>(votingStartedFunction, blockParameter);
        }

        
        public Task<bool> VotingStartedQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<VotingStartedFunction, bool>(null, blockParameter);
        }
    }
}
