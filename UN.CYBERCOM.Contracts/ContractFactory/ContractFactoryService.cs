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
using UN.CYBERCOM.Contracts.ContractFactory.ContractDefinition;

namespace UN.CYBERCOM.Contracts.ContractFactory
{
    public partial class ContractFactoryService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, ContractFactoryDeployment contractFactoryDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<ContractFactoryDeployment>().SendRequestAndWaitForReceiptAsync(contractFactoryDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, ContractFactoryDeployment contractFactoryDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<ContractFactoryDeployment>().SendRequestAsync(contractFactoryDeployment);
        }

        public static async Task<ContractFactoryService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, ContractFactoryDeployment contractFactoryDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, contractFactoryDeployment, cancellationTokenSource);
            return new ContractFactoryService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public ContractFactoryService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public ContractFactoryService(Nethereum.Web3.IWeb3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<ContractsOutputDTO> ContractsQueryAsync(ContractsFunction contractsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<ContractsFunction, ContractsOutputDTO>(contractsFunction, blockParameter);
        }

        public Task<ContractsOutputDTO> ContractsQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<ContractsFunction, ContractsOutputDTO>(null, blockParameter);
        }

        public Task<string> Init2RequestAsync(Init2Function init2Function)
        {
             return ContractHandler.SendRequestAsync(init2Function);
        }

        public Task<TransactionReceipt> Init2RequestAndWaitForReceiptAsync(Init2Function init2Function, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(init2Function, cancellationToken);
        }

        public Task<string> Init2RequestAsync(uint subscriptionId)
        {
            var init2Function = new Init2Function();
                init2Function.SubscriptionId = subscriptionId;
            
             return ContractHandler.SendRequestAsync(init2Function);
        }

        public Task<TransactionReceipt> Init2RequestAndWaitForReceiptAsync(uint subscriptionId, CancellationTokenSource cancellationToken = null)
        {
            var init2Function = new Init2Function();
                init2Function.SubscriptionId = subscriptionId;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(init2Function, cancellationToken);
        }

        public Task<string> InitalizeRequestAsync(InitalizeFunction initalizeFunction)
        {
             return ContractHandler.SendRequestAsync(initalizeFunction);
        }

        public Task<TransactionReceipt> InitalizeRequestAndWaitForReceiptAsync(InitalizeFunction initalizeFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(initalizeFunction, cancellationToken);
        }

        public Task<string> InitalizeRequestAsync(string daoAddress, uint subscriptionId)
        {
            var initalizeFunction = new InitalizeFunction();
                initalizeFunction.DaoAddress = daoAddress;
                initalizeFunction.SubscriptionId = subscriptionId;
            
             return ContractHandler.SendRequestAsync(initalizeFunction);
        }

        public Task<TransactionReceipt> InitalizeRequestAndWaitForReceiptAsync(string daoAddress, uint subscriptionId, CancellationTokenSource cancellationToken = null)
        {
            var initalizeFunction = new InitalizeFunction();
                initalizeFunction.DaoAddress = daoAddress;
                initalizeFunction.SubscriptionId = subscriptionId;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(initalizeFunction, cancellationToken);
        }

        public Task<bool> IsInitalizedQueryAsync(IsInitalizedFunction isInitalizedFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<IsInitalizedFunction, bool>(isInitalizedFunction, blockParameter);
        }

        
        public Task<bool> IsInitalizedQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<IsInitalizedFunction, bool>(null, blockParameter);
        }
    }
}
