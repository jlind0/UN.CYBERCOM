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
using UN.CYBERCOM.Contracts.CYBERCOM.ContractDefinition;

namespace UN.CYBERCOM.Contracts.CYBERCOM
{
    public partial class CybercomService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, CybercomDeployment cybercomDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<CybercomDeployment>().SendRequestAndWaitForReceiptAsync(cybercomDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, CybercomDeployment cybercomDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<CybercomDeployment>().SendRequestAsync(cybercomDeployment);
        }

        public static async Task<CybercomService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, CybercomDeployment cybercomDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, cybercomDeployment, cancellationTokenSource);
            return new CybercomService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public CybercomService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public CybercomService(Nethereum.Web3.IWeb3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<byte[]> BrokerRoleQueryAsync(BrokerRoleFunction brokerRoleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<BrokerRoleFunction, byte[]>(brokerRoleFunction, blockParameter);
        }

        
        public Task<byte[]> BrokerRoleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<BrokerRoleFunction, byte[]>(null, blockParameter);
        }

        public Task<byte[]> CentralRoleQueryAsync(CentralRoleFunction centralRoleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CentralRoleFunction, byte[]>(centralRoleFunction, blockParameter);
        }

        
        public Task<byte[]> CentralRoleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CentralRoleFunction, byte[]>(null, blockParameter);
        }

        public Task<byte[]> DefaultAdminRoleQueryAsync(DefaultAdminRoleFunction defaultAdminRoleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DefaultAdminRoleFunction, byte[]>(defaultAdminRoleFunction, blockParameter);
        }

        
        public Task<byte[]> DefaultAdminRoleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DefaultAdminRoleFunction, byte[]>(null, blockParameter);
        }

        public Task<byte[]> EmergingRoleQueryAsync(EmergingRoleFunction emergingRoleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<EmergingRoleFunction, byte[]>(emergingRoleFunction, blockParameter);
        }

        
        public Task<byte[]> EmergingRoleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<EmergingRoleFunction, byte[]>(null, blockParameter);
        }

        public Task<byte[]> GeneralRoleQueryAsync(GeneralRoleFunction generalRoleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GeneralRoleFunction, byte[]>(generalRoleFunction, blockParameter);
        }

        
        public Task<byte[]> GeneralRoleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GeneralRoleFunction, byte[]>(null, blockParameter);
        }

        public Task<byte[]> PowerRoleQueryAsync(PowerRoleFunction powerRoleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PowerRoleFunction, byte[]>(powerRoleFunction, blockParameter);
        }

        
        public Task<byte[]> PowerRoleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PowerRoleFunction, byte[]>(null, blockParameter);
        }

        public Task<BigInteger> CalculateAverageQueryAsync(CalculateAverageFunction calculateAverageFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CalculateAverageFunction, BigInteger>(calculateAverageFunction, blockParameter);
        }

        
        public Task<BigInteger> CalculateAverageQueryAsync(List<BigInteger> numbers, BlockParameter blockParameter = null)
        {
            var calculateAverageFunction = new CalculateAverageFunction();
                calculateAverageFunction.Numbers = numbers;
            
            return ContractHandler.QueryAsync<CalculateAverageFunction, BigInteger>(calculateAverageFunction, blockParameter);
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

        public Task<GetPendingMembershipRequestsOutputDTO> GetPendingMembershipRequestsQueryAsync(GetPendingMembershipRequestsFunction getPendingMembershipRequestsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetPendingMembershipRequestsFunction, GetPendingMembershipRequestsOutputDTO>(getPendingMembershipRequestsFunction, blockParameter);
        }

        public Task<GetPendingMembershipRequestsOutputDTO> GetPendingMembershipRequestsQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetPendingMembershipRequestsFunction, GetPendingMembershipRequestsOutputDTO>(null, blockParameter);
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

        public Task<BigInteger> MultiplyQueryAsync(MultiplyFunction multiplyFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<MultiplyFunction, BigInteger>(multiplyFunction, blockParameter);
        }

        
        public Task<BigInteger> MultiplyQueryAsync(BigInteger a, BigInteger b, BlockParameter blockParameter = null)
        {
            var multiplyFunction = new MultiplyFunction();
                multiplyFunction.A = a;
                multiplyFunction.B = b;
            
            return ContractHandler.QueryAsync<MultiplyFunction, BigInteger>(multiplyFunction, blockParameter);
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
    }
}
