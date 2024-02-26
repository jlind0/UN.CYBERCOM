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
using UN.CYBERCOM.Contracts.ProposalStorageManager.ContractDefinition;

namespace UN.CYBERCOM.Contracts.ProposalStorageManager
{
    public partial class ProposalStorageManagerService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, ProposalStorageManagerDeployment proposalStorageManagerDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<ProposalStorageManagerDeployment>().SendRequestAndWaitForReceiptAsync(proposalStorageManagerDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, ProposalStorageManagerDeployment proposalStorageManagerDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<ProposalStorageManagerDeployment>().SendRequestAsync(proposalStorageManagerDeployment);
        }

        public static async Task<ProposalStorageManagerService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, ProposalStorageManagerDeployment proposalStorageManagerDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, proposalStorageManagerDeployment, cancellationTokenSource);
            return new ProposalStorageManagerService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public ProposalStorageManagerService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public ProposalStorageManagerService(Nethereum.Web3.IWeb3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> AddMembershipProposalRequestAsync(AddMembershipProposalFunction addMembershipProposalFunction)
        {
             return ContractHandler.SendRequestAsync(addMembershipProposalFunction);
        }

        public Task<TransactionReceipt> AddMembershipProposalRequestAndWaitForReceiptAsync(AddMembershipProposalFunction addMembershipProposalFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addMembershipProposalFunction, cancellationToken);
        }

        public Task<string> AddMembershipProposalRequestAsync(string key)
        {
            var addMembershipProposalFunction = new AddMembershipProposalFunction();
                addMembershipProposalFunction.Key = key;
            
             return ContractHandler.SendRequestAsync(addMembershipProposalFunction);
        }

        public Task<TransactionReceipt> AddMembershipProposalRequestAndWaitForReceiptAsync(string key, CancellationTokenSource cancellationToken = null)
        {
            var addMembershipProposalFunction = new AddMembershipProposalFunction();
                addMembershipProposalFunction.Key = key;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addMembershipProposalFunction, cancellationToken);
        }

        public Task<string> AddMembershipRemovalProposalRequestAsync(AddMembershipRemovalProposalFunction addMembershipRemovalProposalFunction)
        {
             return ContractHandler.SendRequestAsync(addMembershipRemovalProposalFunction);
        }

        public Task<TransactionReceipt> AddMembershipRemovalProposalRequestAndWaitForReceiptAsync(AddMembershipRemovalProposalFunction addMembershipRemovalProposalFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addMembershipRemovalProposalFunction, cancellationToken);
        }

        public Task<string> AddMembershipRemovalProposalRequestAsync(string key)
        {
            var addMembershipRemovalProposalFunction = new AddMembershipRemovalProposalFunction();
                addMembershipRemovalProposalFunction.Key = key;
            
             return ContractHandler.SendRequestAsync(addMembershipRemovalProposalFunction);
        }

        public Task<TransactionReceipt> AddMembershipRemovalProposalRequestAndWaitForReceiptAsync(string key, CancellationTokenSource cancellationToken = null)
        {
            var addMembershipRemovalProposalFunction = new AddMembershipRemovalProposalFunction();
                addMembershipRemovalProposalFunction.Key = key;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addMembershipRemovalProposalFunction, cancellationToken);
        }

        public Task<string> GetMembershipProposalQueryAsync(GetMembershipProposalFunction getMembershipProposalFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetMembershipProposalFunction, string>(getMembershipProposalFunction, blockParameter);
        }

        
        public Task<string> GetMembershipProposalQueryAsync(string key, BlockParameter blockParameter = null)
        {
            var getMembershipProposalFunction = new GetMembershipProposalFunction();
                getMembershipProposalFunction.Key = key;
            
            return ContractHandler.QueryAsync<GetMembershipProposalFunction, string>(getMembershipProposalFunction, blockParameter);
        }

        public Task<List<string>> GetMembershipProposalAddressesQueryAsync(GetMembershipProposalAddressesFunction getMembershipProposalAddressesFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetMembershipProposalAddressesFunction, List<string>>(getMembershipProposalAddressesFunction, blockParameter);
        }

        
        public Task<List<string>> GetMembershipProposalAddressesQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetMembershipProposalAddressesFunction, List<string>>(null, blockParameter);
        }

        public Task<string> GetMembershipRemovalProposalQueryAsync(GetMembershipRemovalProposalFunction getMembershipRemovalProposalFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetMembershipRemovalProposalFunction, string>(getMembershipRemovalProposalFunction, blockParameter);
        }

        
        public Task<string> GetMembershipRemovalProposalQueryAsync(string key, BlockParameter blockParameter = null)
        {
            var getMembershipRemovalProposalFunction = new GetMembershipRemovalProposalFunction();
                getMembershipRemovalProposalFunction.Key = key;
            
            return ContractHandler.QueryAsync<GetMembershipRemovalProposalFunction, string>(getMembershipRemovalProposalFunction, blockParameter);
        }

        public Task<List<string>> GetMembershipRemovalProposalAddressesQueryAsync(GetMembershipRemovalProposalAddressesFunction getMembershipRemovalProposalAddressesFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetMembershipRemovalProposalAddressesFunction, List<string>>(getMembershipRemovalProposalAddressesFunction, blockParameter);
        }

        
        public Task<List<string>> GetMembershipRemovalProposalAddressesQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetMembershipRemovalProposalAddressesFunction, List<string>>(null, blockParameter);
        }

        public Task<string> GetNextProposalIdRequestAsync(GetNextProposalIdFunction getNextProposalIdFunction)
        {
             return ContractHandler.SendRequestAsync(getNextProposalIdFunction);
        }

        public Task<string> GetNextProposalIdRequestAsync()
        {
             return ContractHandler.SendRequestAsync<GetNextProposalIdFunction>();
        }

        public Task<TransactionReceipt> GetNextProposalIdRequestAndWaitForReceiptAsync(GetNextProposalIdFunction getNextProposalIdFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(getNextProposalIdFunction, cancellationToken);
        }

        public Task<TransactionReceipt> GetNextProposalIdRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<GetNextProposalIdFunction>(null, cancellationToken);
        }

        public Task<string> GetProposalQueryAsync(GetProposalFunction getProposalFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetProposalFunction, string>(getProposalFunction, blockParameter);
        }

        
        public Task<string> GetProposalQueryAsync(BigInteger key, BlockParameter blockParameter = null)
        {
            var getProposalFunction = new GetProposalFunction();
                getProposalFunction.Key = key;
            
            return ContractHandler.QueryAsync<GetProposalFunction, string>(getProposalFunction, blockParameter);
        }

        public Task<BigInteger> ProposalCountQueryAsync(ProposalCountFunction proposalCountFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ProposalCountFunction, BigInteger>(proposalCountFunction, blockParameter);
        }

        
        public Task<BigInteger> ProposalCountQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ProposalCountFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> ProposalsQueryAsync(ProposalsFunction proposalsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ProposalsFunction, string>(proposalsFunction, blockParameter);
        }

        
        public Task<string> ProposalsQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
        {
            var proposalsFunction = new ProposalsFunction();
                proposalsFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryAsync<ProposalsFunction, string>(proposalsFunction, blockParameter);
        }

        public Task<string> SetMembershipProposalRequestAsync(SetMembershipProposalFunction setMembershipProposalFunction)
        {
             return ContractHandler.SendRequestAsync(setMembershipProposalFunction);
        }

        public Task<TransactionReceipt> SetMembershipProposalRequestAndWaitForReceiptAsync(SetMembershipProposalFunction setMembershipProposalFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setMembershipProposalFunction, cancellationToken);
        }

        public Task<string> SetMembershipProposalRequestAsync(string key, string value)
        {
            var setMembershipProposalFunction = new SetMembershipProposalFunction();
                setMembershipProposalFunction.Key = key;
                setMembershipProposalFunction.Value = value;
            
             return ContractHandler.SendRequestAsync(setMembershipProposalFunction);
        }

        public Task<TransactionReceipt> SetMembershipProposalRequestAndWaitForReceiptAsync(string key, string value, CancellationTokenSource cancellationToken = null)
        {
            var setMembershipProposalFunction = new SetMembershipProposalFunction();
                setMembershipProposalFunction.Key = key;
                setMembershipProposalFunction.Value = value;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setMembershipProposalFunction, cancellationToken);
        }

        public Task<string> SetMembershipRemovalProposalRequestAsync(SetMembershipRemovalProposalFunction setMembershipRemovalProposalFunction)
        {
             return ContractHandler.SendRequestAsync(setMembershipRemovalProposalFunction);
        }

        public Task<TransactionReceipt> SetMembershipRemovalProposalRequestAndWaitForReceiptAsync(SetMembershipRemovalProposalFunction setMembershipRemovalProposalFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setMembershipRemovalProposalFunction, cancellationToken);
        }

        public Task<string> SetMembershipRemovalProposalRequestAsync(string key, string value)
        {
            var setMembershipRemovalProposalFunction = new SetMembershipRemovalProposalFunction();
                setMembershipRemovalProposalFunction.Key = key;
                setMembershipRemovalProposalFunction.Value = value;
            
             return ContractHandler.SendRequestAsync(setMembershipRemovalProposalFunction);
        }

        public Task<TransactionReceipt> SetMembershipRemovalProposalRequestAndWaitForReceiptAsync(string key, string value, CancellationTokenSource cancellationToken = null)
        {
            var setMembershipRemovalProposalFunction = new SetMembershipRemovalProposalFunction();
                setMembershipRemovalProposalFunction.Key = key;
                setMembershipRemovalProposalFunction.Value = value;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setMembershipRemovalProposalFunction, cancellationToken);
        }

        public Task<string> SetProposalRequestAsync(SetProposalFunction setProposalFunction)
        {
             return ContractHandler.SendRequestAsync(setProposalFunction);
        }

        public Task<TransactionReceipt> SetProposalRequestAndWaitForReceiptAsync(SetProposalFunction setProposalFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setProposalFunction, cancellationToken);
        }

        public Task<string> SetProposalRequestAsync(BigInteger key, string value)
        {
            var setProposalFunction = new SetProposalFunction();
                setProposalFunction.Key = key;
                setProposalFunction.Value = value;
            
             return ContractHandler.SendRequestAsync(setProposalFunction);
        }

        public Task<TransactionReceipt> SetProposalRequestAndWaitForReceiptAsync(BigInteger key, string value, CancellationTokenSource cancellationToken = null)
        {
            var setProposalFunction = new SetProposalFunction();
                setProposalFunction.Key = key;
                setProposalFunction.Value = value;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setProposalFunction, cancellationToken);
        }
    }
}
