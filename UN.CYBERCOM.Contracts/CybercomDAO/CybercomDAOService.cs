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
using UN.CYBERCOM.Contracts.CybercomDAO.ContractDefinition;

namespace UN.CYBERCOM.Contracts.CybercomDAO
{
    public partial class CybercomDAOService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, CybercomDAODeployment cybercomDAODeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<CybercomDAODeployment>().SendRequestAndWaitForReceiptAsync(cybercomDAODeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, CybercomDAODeployment cybercomDAODeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<CybercomDAODeployment>().SendRequestAsync(cybercomDAODeployment);
        }

        public static async Task<CybercomDAOService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, CybercomDAODeployment cybercomDAODeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, cybercomDAODeployment, cancellationTokenSource);
            return new CybercomDAOService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public CybercomDAOService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public CybercomDAOService(Nethereum.Web3.IWeb3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<byte[]> DefaultAdminRoleQueryAsync(DefaultAdminRoleFunction defaultAdminRoleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DefaultAdminRoleFunction, byte[]>(defaultAdminRoleFunction, blockParameter);
        }

        
        public Task<byte[]> DefaultAdminRoleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DefaultAdminRoleFunction, byte[]>(null, blockParameter);
        }

        public Task<string> CompleteVotingRequestAsync(CompleteVotingFunction completeVotingFunction)
        {
             return ContractHandler.SendRequestAsync(completeVotingFunction);
        }

        public Task<TransactionReceipt> CompleteVotingRequestAndWaitForReceiptAsync(CompleteVotingFunction completeVotingFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(completeVotingFunction, cancellationToken);
        }

        public Task<string> CompleteVotingRequestAsync(BigInteger proposalId)
        {
            var completeVotingFunction = new CompleteVotingFunction();
                completeVotingFunction.ProposalId = proposalId;
            
             return ContractHandler.SendRequestAsync(completeVotingFunction);
        }

        public Task<TransactionReceipt> CompleteVotingRequestAndWaitForReceiptAsync(BigInteger proposalId, CancellationTokenSource cancellationToken = null)
        {
            var completeVotingFunction = new CompleteVotingFunction();
                completeVotingFunction.ProposalId = proposalId;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(completeVotingFunction, cancellationToken);
        }

        public Task<string> CouncilManagementAddressQueryAsync(CouncilManagementAddressFunction councilManagementAddressFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CouncilManagementAddressFunction, string>(councilManagementAddressFunction, blockParameter);
        }

        
        public Task<string> CouncilManagementAddressQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CouncilManagementAddressFunction, string>(null, blockParameter);
        }

        public Task<GetApprovedMembershipRequestsOutputDTO> GetApprovedMembershipRequestsQueryAsync(GetApprovedMembershipRequestsFunction getApprovedMembershipRequestsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetApprovedMembershipRequestsFunction, GetApprovedMembershipRequestsOutputDTO>(getApprovedMembershipRequestsFunction, blockParameter);
        }

        public Task<GetApprovedMembershipRequestsOutputDTO> GetApprovedMembershipRequestsQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetApprovedMembershipRequestsFunction, GetApprovedMembershipRequestsOutputDTO>(null, blockParameter);
        }

        public Task<GetCouncilOutputDTO> GetCouncilQueryAsync(GetCouncilFunction getCouncilFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetCouncilFunction, GetCouncilOutputDTO>(getCouncilFunction, blockParameter);
        }

        public Task<GetCouncilOutputDTO> GetCouncilQueryAsync(byte[] role, BlockParameter blockParameter = null)
        {
            var getCouncilFunction = new GetCouncilFunction();
                getCouncilFunction.Role = role;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetCouncilFunction, GetCouncilOutputDTO>(getCouncilFunction, blockParameter);
        }

        public Task<GetCouncilsOutputDTO> GetCouncilsQueryAsync(GetCouncilsFunction getCouncilsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetCouncilsFunction, GetCouncilsOutputDTO>(getCouncilsFunction, blockParameter);
        }

        public Task<GetCouncilsOutputDTO> GetCouncilsQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetCouncilsFunction, GetCouncilsOutputDTO>(null, blockParameter);
        }

        public Task<GetEnteredMembershipRequestsOutputDTO> GetEnteredMembershipRequestsQueryAsync(GetEnteredMembershipRequestsFunction getEnteredMembershipRequestsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetEnteredMembershipRequestsFunction, GetEnteredMembershipRequestsOutputDTO>(getEnteredMembershipRequestsFunction, blockParameter);
        }

        public Task<GetEnteredMembershipRequestsOutputDTO> GetEnteredMembershipRequestsQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetEnteredMembershipRequestsFunction, GetEnteredMembershipRequestsOutputDTO>(null, blockParameter);
        }

        public Task<GetPendingMembershipRequestsOutputDTO> GetPendingMembershipRequestsQueryAsync(GetPendingMembershipRequestsFunction getPendingMembershipRequestsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetPendingMembershipRequestsFunction, GetPendingMembershipRequestsOutputDTO>(getPendingMembershipRequestsFunction, blockParameter);
        }

        public Task<GetPendingMembershipRequestsOutputDTO> GetPendingMembershipRequestsQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetPendingMembershipRequestsFunction, GetPendingMembershipRequestsOutputDTO>(null, blockParameter);
        }

        public Task<GetProposalVotesOutputDTO> GetProposalVotesQueryAsync(GetProposalVotesFunction getProposalVotesFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetProposalVotesFunction, GetProposalVotesOutputDTO>(getProposalVotesFunction, blockParameter);
        }

        public Task<GetProposalVotesOutputDTO> GetProposalVotesQueryAsync(BigInteger proposalId, BlockParameter blockParameter = null)
        {
            var getProposalVotesFunction = new GetProposalVotesFunction();
                getProposalVotesFunction.ProposalId = proposalId;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetProposalVotesFunction, GetProposalVotesOutputDTO>(getProposalVotesFunction, blockParameter);
        }

        public Task<GetReadyMembershipRequestsOutputDTO> GetReadyMembershipRequestsQueryAsync(GetReadyMembershipRequestsFunction getReadyMembershipRequestsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetReadyMembershipRequestsFunction, GetReadyMembershipRequestsOutputDTO>(getReadyMembershipRequestsFunction, blockParameter);
        }

        public Task<GetReadyMembershipRequestsOutputDTO> GetReadyMembershipRequestsQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetReadyMembershipRequestsFunction, GetReadyMembershipRequestsOutputDTO>(null, blockParameter);
        }

        public Task<GetRejectedMembershipRequestsOutputDTO> GetRejectedMembershipRequestsQueryAsync(GetRejectedMembershipRequestsFunction getRejectedMembershipRequestsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetRejectedMembershipRequestsFunction, GetRejectedMembershipRequestsOutputDTO>(getRejectedMembershipRequestsFunction, blockParameter);
        }

        public Task<GetRejectedMembershipRequestsOutputDTO> GetRejectedMembershipRequestsQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetRejectedMembershipRequestsFunction, GetRejectedMembershipRequestsOutputDTO>(null, blockParameter);
        }

        public Task<byte[]> GetRoleAdminQueryAsync(GetRoleAdminFunction getRoleAdminFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetRoleAdminFunction, byte[]>(getRoleAdminFunction, blockParameter);
        }

        
        public Task<byte[]> GetRoleAdminQueryAsync(byte[] role, BlockParameter blockParameter = null)
        {
            var getRoleAdminFunction = new GetRoleAdminFunction();
                getRoleAdminFunction.Role = role;
            
            return ContractHandler.QueryAsync<GetRoleAdminFunction, byte[]>(getRoleAdminFunction, blockParameter);
        }

        public Task<string> GrantRoleRequestAsync(GrantRoleFunction grantRoleFunction)
        {
             return ContractHandler.SendRequestAsync(grantRoleFunction);
        }

        public Task<TransactionReceipt> GrantRoleRequestAndWaitForReceiptAsync(GrantRoleFunction grantRoleFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(grantRoleFunction, cancellationToken);
        }

        public Task<string> GrantRoleRequestAsync(byte[] role, string account)
        {
            var grantRoleFunction = new GrantRoleFunction();
                grantRoleFunction.Role = role;
                grantRoleFunction.Account = account;
            
             return ContractHandler.SendRequestAsync(grantRoleFunction);
        }

        public Task<TransactionReceipt> GrantRoleRequestAndWaitForReceiptAsync(byte[] role, string account, CancellationTokenSource cancellationToken = null)
        {
            var grantRoleFunction = new GrantRoleFunction();
                grantRoleFunction.Role = role;
                grantRoleFunction.Account = account;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(grantRoleFunction, cancellationToken);
        }

        public Task<bool> HasRoleQueryAsync(HasRoleFunction hasRoleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<HasRoleFunction, bool>(hasRoleFunction, blockParameter);
        }

        
        public Task<bool> HasRoleQueryAsync(byte[] role, string account, BlockParameter blockParameter = null)
        {
            var hasRoleFunction = new HasRoleFunction();
                hasRoleFunction.Role = role;
                hasRoleFunction.Account = account;
            
            return ContractHandler.QueryAsync<HasRoleFunction, bool>(hasRoleFunction, blockParameter);
        }

        public Task<string> PerformVoteRequestAsync(PerformVoteFunction performVoteFunction)
        {
             return ContractHandler.SendRequestAsync(performVoteFunction);
        }

        public Task<TransactionReceipt> PerformVoteRequestAndWaitForReceiptAsync(PerformVoteFunction performVoteFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(performVoteFunction, cancellationToken);
        }

        public Task<string> PerformVoteRequestAsync(BigInteger proposalId, bool voteCast)
        {
            var performVoteFunction = new PerformVoteFunction();
                performVoteFunction.ProposalId = proposalId;
                performVoteFunction.VoteCast = voteCast;
            
             return ContractHandler.SendRequestAsync(performVoteFunction);
        }

        public Task<TransactionReceipt> PerformVoteRequestAndWaitForReceiptAsync(BigInteger proposalId, bool voteCast, CancellationTokenSource cancellationToken = null)
        {
            var performVoteFunction = new PerformVoteFunction();
                performVoteFunction.ProposalId = proposalId;
                performVoteFunction.VoteCast = voteCast;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(performVoteFunction, cancellationToken);
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

        public Task<string> RenounceRoleRequestAsync(RenounceRoleFunction renounceRoleFunction)
        {
             return ContractHandler.SendRequestAsync(renounceRoleFunction);
        }

        public Task<TransactionReceipt> RenounceRoleRequestAndWaitForReceiptAsync(RenounceRoleFunction renounceRoleFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(renounceRoleFunction, cancellationToken);
        }

        public Task<string> RenounceRoleRequestAsync(byte[] role, string callerConfirmation)
        {
            var renounceRoleFunction = new RenounceRoleFunction();
                renounceRoleFunction.Role = role;
                renounceRoleFunction.CallerConfirmation = callerConfirmation;
            
             return ContractHandler.SendRequestAsync(renounceRoleFunction);
        }

        public Task<TransactionReceipt> RenounceRoleRequestAndWaitForReceiptAsync(byte[] role, string callerConfirmation, CancellationTokenSource cancellationToken = null)
        {
            var renounceRoleFunction = new RenounceRoleFunction();
                renounceRoleFunction.Role = role;
                renounceRoleFunction.CallerConfirmation = callerConfirmation;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(renounceRoleFunction, cancellationToken);
        }

        public Task<string> RevokeRoleRequestAsync(RevokeRoleFunction revokeRoleFunction)
        {
             return ContractHandler.SendRequestAsync(revokeRoleFunction);
        }

        public Task<TransactionReceipt> RevokeRoleRequestAndWaitForReceiptAsync(RevokeRoleFunction revokeRoleFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(revokeRoleFunction, cancellationToken);
        }

        public Task<string> RevokeRoleRequestAsync(byte[] role, string account)
        {
            var revokeRoleFunction = new RevokeRoleFunction();
                revokeRoleFunction.Role = role;
                revokeRoleFunction.Account = account;
            
             return ContractHandler.SendRequestAsync(revokeRoleFunction);
        }

        public Task<TransactionReceipt> RevokeRoleRequestAndWaitForReceiptAsync(byte[] role, string account, CancellationTokenSource cancellationToken = null)
        {
            var revokeRoleFunction = new RevokeRoleFunction();
                revokeRoleFunction.Role = role;
                revokeRoleFunction.Account = account;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(revokeRoleFunction, cancellationToken);
        }

        public Task<string> StartVotingRequestAsync(StartVotingFunction startVotingFunction)
        {
             return ContractHandler.SendRequestAsync(startVotingFunction);
        }

        public Task<TransactionReceipt> StartVotingRequestAndWaitForReceiptAsync(StartVotingFunction startVotingFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(startVotingFunction, cancellationToken);
        }

        public Task<string> StartVotingRequestAsync(BigInteger proposalId)
        {
            var startVotingFunction = new StartVotingFunction();
                startVotingFunction.ProposalId = proposalId;
            
             return ContractHandler.SendRequestAsync(startVotingFunction);
        }

        public Task<TransactionReceipt> StartVotingRequestAndWaitForReceiptAsync(BigInteger proposalId, CancellationTokenSource cancellationToken = null)
        {
            var startVotingFunction = new StartVotingFunction();
                startVotingFunction.ProposalId = proposalId;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(startVotingFunction, cancellationToken);
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

        public Task<bool> SupportsInterfaceQueryAsync(SupportsInterfaceFunction supportsInterfaceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SupportsInterfaceFunction, bool>(supportsInterfaceFunction, blockParameter);
        }

        
        public Task<bool> SupportsInterfaceQueryAsync(byte[] interfaceId, BlockParameter blockParameter = null)
        {
            var supportsInterfaceFunction = new SupportsInterfaceFunction();
                supportsInterfaceFunction.InterfaceId = interfaceId;
            
            return ContractHandler.QueryAsync<SupportsInterfaceFunction, bool>(supportsInterfaceFunction, blockParameter);
        }

        public Task<string> VotingAddressQueryAsync(VotingAddressFunction votingAddressFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<VotingAddressFunction, string>(votingAddressFunction, blockParameter);
        }

        
        public Task<string> VotingAddressQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<VotingAddressFunction, string>(null, blockParameter);
        }
    }
}
