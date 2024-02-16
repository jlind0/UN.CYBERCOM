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
using UN.CYBERCOM.Contracts.DocumentsHolder.ContractDefinition;

namespace UN.CYBERCOM.Contracts.DocumentsHolder
{
    public partial class DocumentsHolderService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, DocumentsHolderDeployment documentsHolderDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<DocumentsHolderDeployment>().SendRequestAndWaitForReceiptAsync(documentsHolderDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, DocumentsHolderDeployment documentsHolderDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<DocumentsHolderDeployment>().SendRequestAsync(documentsHolderDeployment);
        }

        public static async Task<DocumentsHolderService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, DocumentsHolderDeployment documentsHolderDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, documentsHolderDeployment, cancellationTokenSource);
            return new DocumentsHolderService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public DocumentsHolderService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public DocumentsHolderService(Nethereum.Web3.IWeb3 web3, string contractAddress)
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

        public Task<GetDocumentsOutputDTO> GetDocumentsQueryAsync(GetDocumentsFunction getDocumentsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetDocumentsFunction, GetDocumentsOutputDTO>(getDocumentsFunction, blockParameter);
        }

        public Task<GetDocumentsOutputDTO> GetDocumentsQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetDocumentsFunction, GetDocumentsOutputDTO>(null, blockParameter);
        }
    }
}
