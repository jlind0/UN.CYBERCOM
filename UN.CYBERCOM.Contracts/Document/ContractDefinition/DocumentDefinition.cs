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

namespace UN.CYBERCOM.Contracts.Document.ContractDefinition
{


    public partial class DocumentDeployment : DocumentDeploymentBase
    {
        public DocumentDeployment() : base(BYTECODE) { }
        public DocumentDeployment(string byteCode) : base(byteCode) { }
    }

    public class DocumentDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "608060405234801561000f575f80fd5b506040516107d83803806107d883398101604081905261002e9161033e565b5f6100398282610483565b5060016100468382610483565b50600283905560036100588582610483565b50600480546001600160a01b038088166001600160a01b0319928316179092556005805492891692909116919091179055426006556002546040515f916100cd916020017f19457468657265756d205369676e6564204d6573736167653a0a3332000000008152601c810191909152603c0190565b60408051601f19818403018152919052805160209091012090505f80806100f48489610156565b919450925090505f82600381111561010e5761010e610542565b14158061012957506004546001600160a01b03848116911614155b1561014757604051638baa579f60e01b815260040160405180910390fd5b50505050505050505050610556565b5f805f835160410361018d576020840151604085015160608601515f1a61017f8882858561019f565b955095509550505050610198565b505081515f91506002905b9250925092565b5f80806fa2a8918ca85bafe22016d0b997e4df60600160ff1b038411156101ce57505f91506003905082610253565b604080515f808252602082018084528a905260ff891692820192909252606081018790526080810186905260019060a0016020604051602081039080840390855afa15801561021f573d5f803e3d5ffd5b5050604051601f1901519150506001600160a01b03811661024a57505f925060019150829050610253565b92505f91508190505b9450945094915050565b80516001600160a01b0381168114610273575f80fd5b919050565b634e487b7160e01b5f52604160045260245ffd5b5f6001600160401b03808411156102a5576102a5610278565b604051601f8501601f19908116603f011681019082821181831017156102cd576102cd610278565b816040528093508581528686860111156102e5575f80fd5b5f92505b858310156103075782850151602084830101526020830192506102e9565b5f602087830101525050509392505050565b5f82601f830112610328575f80fd5b6103378383516020850161028c565b9392505050565b5f805f805f8060c08789031215610353575f80fd5b61035c8761025d565b955061036a6020880161025d565b60408801519095506001600160401b0380821115610386575f80fd5b818901915089601f830112610399575f80fd5b6103a88a83516020850161028c565b95506060890151945060808901519150808211156103c4575f80fd5b6103d08a838b01610319565b935060a08901519150808211156103e5575f80fd5b506103f289828a01610319565b9150509295509295509295565b600181811c9082168061041357607f821691505b60208210810361043157634e487b7160e01b5f52602260045260245ffd5b50919050565b601f82111561047e57805f5260205f20601f840160051c8101602085101561045c5750805b601f840160051c820191505b8181101561047b575f8155600101610468565b50505b505050565b81516001600160401b0381111561049c5761049c610278565b6104b0816104aa84546103ff565b84610437565b602080601f8311600181146104e3575f84156104cc5750858301515b5f19600386901b1c1916600185901b17855561053a565b5f85815260208120601f198616915b82811015610511578886015182559484019460019091019084016104f2565b508582101561052e57878501515f19600388901b60f8161c191681555b505060018460011b0185555b505050505050565b634e487b7160e01b5f52602160045260245ffd5b610275806105635f395ff3fe608060405234801561000f575f80fd5b506004361061007a575f3560e01c806351ff48471161005857806351ff4847146100da5780635600f04f146100e2578063b80777ea146100ea578063ca973727146100f3575f80fd5b806310c83e531461007e578063238ac9331461009a5780634a79d50c146100c5575b5f80fd5b61008760025481565b6040519081526020015b60405180910390f35b6004546100ad906001600160a01b031681565b6040516001600160a01b039091168152602001610091565b6100cd610106565b60405161009191906101ee565b6100cd610191565b6100cd61019e565b61008760065481565b6005546100ad906001600160a01b031681565b5f805461011290610207565b80601f016020809104026020016040519081016040528092919081815260200182805461013e90610207565b80156101895780601f1061016057610100808354040283529160200191610189565b820191905f5260205f20905b81548152906001019060200180831161016c57829003601f168201915b505050505081565b6003805461011290610207565b6001805461011290610207565b5f81518084525f5b818110156101cf576020818501810151868301820152016101b3565b505f602082860101526020601f19601f83011685010191505092915050565b602081525f61020060208301846101ab565b9392505050565b600181811c9082168061021b57607f821691505b60208210810361023957634e487b7160e01b5f52602260045260245ffd5b5091905056fea26469706673582212200b2832f3a36311a56e16e9fa36375d9ca262b127833a79483743c3ece3cf05fa64736f6c63430008170033";
        public DocumentDeploymentBase() : base(BYTECODE) { }
        public DocumentDeploymentBase(string byteCode) : base(byteCode) { }
        [Parameter("address", "_owningContract", 1)]
        public virtual string OwningContract { get; set; }
        [Parameter("address", "_signer", 2)]
        public virtual string Signer { get; set; }
        [Parameter("bytes", "_signature", 3)]
        public virtual byte[] Signature { get; set; }
        [Parameter("bytes32", "_dochash", 4)]
        public virtual byte[] Dochash { get; set; }
        [Parameter("string", "_url", 5)]
        public virtual string Url { get; set; }
        [Parameter("string", "_title", 6)]
        public virtual string Title { get; set; }
    }

    public partial class DochashFunction : DochashFunctionBase { }

    [Function("dochash", "bytes32")]
    public class DochashFunctionBase : FunctionMessage
    {

    }

    public partial class OwningContractFunction : OwningContractFunctionBase { }

    [Function("owningContract", "address")]
    public class OwningContractFunctionBase : FunctionMessage
    {

    }

    public partial class SignatureFunction : SignatureFunctionBase { }

    [Function("signature", "bytes")]
    public class SignatureFunctionBase : FunctionMessage
    {

    }

    public partial class SignerFunction : SignerFunctionBase { }

    [Function("signer", "address")]
    public class SignerFunctionBase : FunctionMessage
    {

    }

    public partial class TimestampFunction : TimestampFunctionBase { }

    [Function("timestamp", "uint256")]
    public class TimestampFunctionBase : FunctionMessage
    {

    }

    public partial class TitleFunction : TitleFunctionBase { }

    [Function("title", "string")]
    public class TitleFunctionBase : FunctionMessage
    {

    }

    public partial class UrlFunction : UrlFunctionBase { }

    [Function("url", "string")]
    public class UrlFunctionBase : FunctionMessage
    {

    }

    public partial class AuthorizationErrorError : AuthorizationErrorErrorBase { }
    [Error("AuthorizationError")]
    public class AuthorizationErrorErrorBase : IErrorDTO
    {
    }

    public partial class InvalidSignatureError : InvalidSignatureErrorBase { }
    [Error("InvalidSignature")]
    public class InvalidSignatureErrorBase : IErrorDTO
    {
    }

    public partial class DochashOutputDTO : DochashOutputDTOBase { }

    [FunctionOutput]
    public class DochashOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bytes32", "", 1)]
        public virtual byte[] ReturnValue1 { get; set; }
    }

    public partial class OwningContractOutputDTO : OwningContractOutputDTOBase { }

    [FunctionOutput]
    public class OwningContractOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class SignatureOutputDTO : SignatureOutputDTOBase { }

    [FunctionOutput]
    public class SignatureOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bytes", "", 1)]
        public virtual byte[] ReturnValue1 { get; set; }
    }

    public partial class SignerOutputDTO : SignerOutputDTOBase { }

    [FunctionOutput]
    public class SignerOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class TimestampOutputDTO : TimestampOutputDTOBase { }

    [FunctionOutput]
    public class TimestampOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class TitleOutputDTO : TitleOutputDTOBase { }

    [FunctionOutput]
    public class TitleOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class UrlOutputDTO : UrlOutputDTOBase { }

    [FunctionOutput]
    public class UrlOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }
}
