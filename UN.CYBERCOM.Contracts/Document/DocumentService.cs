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
using UN.CYBERCOM.Contracts.Document.ContractDefinition;

namespace UN.CYBERCOM.Contracts.Document
{
    public partial class DocumentService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, DocumentDeployment documentDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<DocumentDeployment>().SendRequestAndWaitForReceiptAsync(documentDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, DocumentDeployment documentDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<DocumentDeployment>().SendRequestAsync(documentDeployment);
        }

        public static async Task<DocumentService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, DocumentDeployment documentDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, documentDeployment, cancellationTokenSource);
            return new DocumentService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public DocumentService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public DocumentService(Nethereum.Web3.IWeb3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<byte[]> DochashQueryAsync(DochashFunction dochashFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DochashFunction, byte[]>(dochashFunction, blockParameter);
        }

        
        public Task<byte[]> DochashQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DochashFunction, byte[]>(null, blockParameter);
        }

        public Task<string> OwningContractQueryAsync(OwningContractFunction owningContractFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OwningContractFunction, string>(owningContractFunction, blockParameter);
        }

        
        public Task<string> OwningContractQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OwningContractFunction, string>(null, blockParameter);
        }

        public Task<byte[]> SignatureQueryAsync(SignatureFunction signatureFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SignatureFunction, byte[]>(signatureFunction, blockParameter);
        }

        
        public Task<byte[]> SignatureQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SignatureFunction, byte[]>(null, blockParameter);
        }

        public Task<string> SignerQueryAsync(SignerFunction signerFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SignerFunction, string>(signerFunction, blockParameter);
        }

        
        public Task<string> SignerQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SignerFunction, string>(null, blockParameter);
        }

        public Task<BigInteger> TimestampQueryAsync(TimestampFunction timestampFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TimestampFunction, BigInteger>(timestampFunction, blockParameter);
        }

        
        public Task<BigInteger> TimestampQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TimestampFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> TitleQueryAsync(TitleFunction titleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TitleFunction, string>(titleFunction, blockParameter);
        }

        
        public Task<string> TitleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TitleFunction, string>(null, blockParameter);
        }

        public Task<string> UrlQueryAsync(UrlFunction urlFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<UrlFunction, string>(urlFunction, blockParameter);
        }

        
        public Task<string> UrlQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<UrlFunction, string>(null, blockParameter);
        }
    }
}
