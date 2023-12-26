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
using UN.CYBERCOM.Contracts.DominionDAO.ContractDefinition;

namespace UN.CYBERCOM.Contracts.DominionDAO
{
    public partial class DominionDAOService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, DominionDAODeployment dominionDAODeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<DominionDAODeployment>().SendRequestAndWaitForReceiptAsync(dominionDAODeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, DominionDAODeployment dominionDAODeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<DominionDAODeployment>().SendRequestAsync(dominionDAODeployment);
        }

        public static async Task<DominionDAOService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, DominionDAODeployment dominionDAODeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, dominionDAODeployment, cancellationTokenSource);
            return new DominionDAOService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public DominionDAOService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public DominionDAOService(Nethereum.Web3.IWeb3 web3, string contractAddress)
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

        public Task<string> ContributeRequestAsync(ContributeFunction contributeFunction)
        {
             return ContractHandler.SendRequestAsync(contributeFunction);
        }

        public Task<string> ContributeRequestAsync()
        {
             return ContractHandler.SendRequestAsync<ContributeFunction>();
        }

        public Task<TransactionReceipt> ContributeRequestAndWaitForReceiptAsync(ContributeFunction contributeFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(contributeFunction, cancellationToken);
        }

        public Task<TransactionReceipt> ContributeRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<ContributeFunction>(null, cancellationToken);
        }

        public Task<string> CreateProposalRequestAsync(CreateProposalFunction createProposalFunction)
        {
             return ContractHandler.SendRequestAsync(createProposalFunction);
        }

        public Task<TransactionReceipt> CreateProposalRequestAndWaitForReceiptAsync(CreateProposalFunction createProposalFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(createProposalFunction, cancellationToken);
        }

        public Task<string> CreateProposalRequestAsync(string title, string description, string beneficiary, BigInteger amount)
        {
            var createProposalFunction = new CreateProposalFunction();
                createProposalFunction.Title = title;
                createProposalFunction.Description = description;
                createProposalFunction.Beneficiary = beneficiary;
                createProposalFunction.Amount = amount;
            
             return ContractHandler.SendRequestAsync(createProposalFunction);
        }

        public Task<TransactionReceipt> CreateProposalRequestAndWaitForReceiptAsync(string title, string description, string beneficiary, BigInteger amount, CancellationTokenSource cancellationToken = null)
        {
            var createProposalFunction = new CreateProposalFunction();
                createProposalFunction.Title = title;
                createProposalFunction.Description = description;
                createProposalFunction.Beneficiary = beneficiary;
                createProposalFunction.Amount = amount;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(createProposalFunction, cancellationToken);
        }

        public Task<BigInteger> DaoBalanceQueryAsync(DaoBalanceFunction daoBalanceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DaoBalanceFunction, BigInteger>(daoBalanceFunction, blockParameter);
        }

        
        public Task<BigInteger> DaoBalanceQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DaoBalanceFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> GetBalanceQueryAsync(GetBalanceFunction getBalanceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetBalanceFunction, BigInteger>(getBalanceFunction, blockParameter);
        }

        
        public Task<BigInteger> GetBalanceQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetBalanceFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> GetContributorBalanceQueryAsync(GetContributorBalanceFunction getContributorBalanceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetContributorBalanceFunction, BigInteger>(getContributorBalanceFunction, blockParameter);
        }

        
        public Task<BigInteger> GetContributorBalanceQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetContributorBalanceFunction, BigInteger>(null, blockParameter);
        }

        public Task<GetProposalOutputDTO> GetProposalQueryAsync(GetProposalFunction getProposalFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetProposalFunction, GetProposalOutputDTO>(getProposalFunction, blockParameter);
        }

        public Task<GetProposalOutputDTO> GetProposalQueryAsync(BigInteger proposalId, BlockParameter blockParameter = null)
        {
            var getProposalFunction = new GetProposalFunction();
                getProposalFunction.ProposalId = proposalId;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetProposalFunction, GetProposalOutputDTO>(getProposalFunction, blockParameter);
        }

        public Task<GetProposalsOutputDTO> GetProposalsQueryAsync(GetProposalsFunction getProposalsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetProposalsFunction, GetProposalsOutputDTO>(getProposalsFunction, blockParameter);
        }

        public Task<GetProposalsOutputDTO> GetProposalsQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetProposalsFunction, GetProposalsOutputDTO>(null, blockParameter);
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

        public Task<BigInteger> GetStakeholderBalanceQueryAsync(GetStakeholderBalanceFunction getStakeholderBalanceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetStakeholderBalanceFunction, BigInteger>(getStakeholderBalanceFunction, blockParameter);
        }

        
        public Task<BigInteger> GetStakeholderBalanceQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetStakeholderBalanceFunction, BigInteger>(null, blockParameter);
        }

        public Task<List<BigInteger>> GetStakeholderVotesQueryAsync(GetStakeholderVotesFunction getStakeholderVotesFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetStakeholderVotesFunction, List<BigInteger>>(getStakeholderVotesFunction, blockParameter);
        }

        
        public Task<List<BigInteger>> GetStakeholderVotesQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetStakeholderVotesFunction, List<BigInteger>>(null, blockParameter);
        }

        public Task<GetVotesOfOutputDTO> GetVotesOfQueryAsync(GetVotesOfFunction getVotesOfFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetVotesOfFunction, GetVotesOfOutputDTO>(getVotesOfFunction, blockParameter);
        }

        public Task<GetVotesOfOutputDTO> GetVotesOfQueryAsync(BigInteger proposalId, BlockParameter blockParameter = null)
        {
            var getVotesOfFunction = new GetVotesOfFunction();
                getVotesOfFunction.ProposalId = proposalId;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetVotesOfFunction, GetVotesOfOutputDTO>(getVotesOfFunction, blockParameter);
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

        public Task<bool> IsContributorQueryAsync(IsContributorFunction isContributorFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<IsContributorFunction, bool>(isContributorFunction, blockParameter);
        }

        
        public Task<bool> IsContributorQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<IsContributorFunction, bool>(null, blockParameter);
        }

        public Task<bool> IsStakeholderQueryAsync(IsStakeholderFunction isStakeholderFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<IsStakeholderFunction, bool>(isStakeholderFunction, blockParameter);
        }

        
        public Task<bool> IsStakeholderQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<IsStakeholderFunction, bool>(null, blockParameter);
        }

        public Task<string> PayBeneficiaryRequestAsync(PayBeneficiaryFunction payBeneficiaryFunction)
        {
             return ContractHandler.SendRequestAsync(payBeneficiaryFunction);
        }

        public Task<TransactionReceipt> PayBeneficiaryRequestAndWaitForReceiptAsync(PayBeneficiaryFunction payBeneficiaryFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(payBeneficiaryFunction, cancellationToken);
        }

        public Task<string> PayBeneficiaryRequestAsync(BigInteger proposalId)
        {
            var payBeneficiaryFunction = new PayBeneficiaryFunction();
                payBeneficiaryFunction.ProposalId = proposalId;
            
             return ContractHandler.SendRequestAsync(payBeneficiaryFunction);
        }

        public Task<TransactionReceipt> PayBeneficiaryRequestAndWaitForReceiptAsync(BigInteger proposalId, CancellationTokenSource cancellationToken = null)
        {
            var payBeneficiaryFunction = new PayBeneficiaryFunction();
                payBeneficiaryFunction.ProposalId = proposalId;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(payBeneficiaryFunction, cancellationToken);
        }

        public Task<string> PerformVoteRequestAsync(PerformVoteFunction performVoteFunction)
        {
             return ContractHandler.SendRequestAsync(performVoteFunction);
        }

        public Task<TransactionReceipt> PerformVoteRequestAndWaitForReceiptAsync(PerformVoteFunction performVoteFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(performVoteFunction, cancellationToken);
        }

        public Task<string> PerformVoteRequestAsync(BigInteger proposalId, bool choosen)
        {
            var performVoteFunction = new PerformVoteFunction();
                performVoteFunction.ProposalId = proposalId;
                performVoteFunction.Choosen = choosen;
            
             return ContractHandler.SendRequestAsync(performVoteFunction);
        }

        public Task<TransactionReceipt> PerformVoteRequestAndWaitForReceiptAsync(BigInteger proposalId, bool choosen, CancellationTokenSource cancellationToken = null)
        {
            var performVoteFunction = new PerformVoteFunction();
                performVoteFunction.ProposalId = proposalId;
                performVoteFunction.Choosen = choosen;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(performVoteFunction, cancellationToken);
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
    }
}
