using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;

namespace UN.CYBERCOM.Contracts.DocumentsHolder.ContractDefinition
{


    public partial class DocumentsHolderDeployment : DocumentsHolderDeploymentBase
    {
        public DocumentsHolderDeployment() : base(BYTECODE) { }
        public DocumentsHolderDeployment(string byteCode) : base(byteCode) { }
    }

    public class DocumentsHolderDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "";
        public DocumentsHolderDeploymentBase() : base(BYTECODE) { }
        public DocumentsHolderDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class AddDocumentFunction : AddDocumentFunctionBase { }

    [Function("addDocument")]
    public class AddDocumentFunctionBase : FunctionMessage
    {
        [Parameter("address", "signer", 1)]
        public virtual string Signer { get; set; }
        [Parameter("string", "title", 2)]
        public virtual string Title { get; set; }
        [Parameter("string", "url", 3)]
        public virtual string Url { get; set; }
        [Parameter("bytes32", "docHash", 4)]
        public virtual byte[] DocHash { get; set; }
        [Parameter("bytes", "signature", 5)]
        public virtual byte[] Signature { get; set; }
    }

    public partial class GetDocumentsFunction : GetDocumentsFunctionBase { }

    [Function("getDocuments", typeof(GetDocumentsOutputDTO))]
    public class GetDocumentsFunctionBase : FunctionMessage
    {

    }



    public partial class GetDocumentsOutputDTO : GetDocumentsOutputDTOBase { }

    [FunctionOutput]
    public class GetDocumentsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("tuple[]", "", 1)]
        public virtual List<Doc> ReturnValue1 { get; set; }
    }
}
