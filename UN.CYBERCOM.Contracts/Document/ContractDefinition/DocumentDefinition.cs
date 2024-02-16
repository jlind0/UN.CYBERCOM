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
        public static string BYTECODE = "608060405234801561000f575f80fd5b506040516107ff3803806107ff83398101604081905261002e91610365565b5f61003982826104aa565b50600161004683826104aa565b506002839055600361005885826104aa565b50600480546001600160a01b038088166001600160a01b0319928316179092556005805492891692909116919091179055426006556002546040515f916100cd916020017f19457468657265756d205369676e6564204d6573736167653a0a3332000000008152601c810191909152603c0190565b60408051601f19818403018152919052805160209091012090505f80806100f4848961017d565b919450925090505f82600381111561010e5761010e610569565b14158061012957506004546001600160a01b03848116911614155b1561016e5760405162461bcd60e51b8152602060048201526011602482015270496e76616c6964207369676e617475726560781b604482015260640160405180910390fd5b5050505050505050505061057d565b5f805f83516041036101b4576020840151604085015160608601515f1a6101a6888285856101c6565b9550955095505050506101bf565b505081515f91506002905b9250925092565b5f80806fa2a8918ca85bafe22016d0b997e4df60600160ff1b038411156101f557505f9150600390508261027a565b604080515f808252602082018084528a905260ff891692820192909252606081018790526080810186905260019060a0016020604051602081039080840390855afa158015610246573d5f803e3d5ffd5b5050604051601f1901519150506001600160a01b03811661027157505f92506001915082905061027a565b92505f91508190505b9450945094915050565b80516001600160a01b038116811461029a575f80fd5b919050565b634e487b7160e01b5f52604160045260245ffd5b5f6001600160401b03808411156102cc576102cc61029f565b604051601f8501601f19908116603f011681019082821181831017156102f4576102f461029f565b8160405280935085815286868601111561030c575f80fd5b5f92505b8583101561032e578285015160208483010152602083019250610310565b5f602087830101525050509392505050565b5f82601f83011261034f575f80fd5b61035e838351602085016102b3565b9392505050565b5f805f805f8060c0878903121561037a575f80fd5b61038387610284565b955061039160208801610284565b60408801519095506001600160401b03808211156103ad575f80fd5b818901915089601f8301126103c0575f80fd5b6103cf8a8351602085016102b3565b95506060890151945060808901519150808211156103eb575f80fd5b6103f78a838b01610340565b935060a089015191508082111561040c575f80fd5b5061041989828a01610340565b9150509295509295509295565b600181811c9082168061043a57607f821691505b60208210810361045857634e487b7160e01b5f52602260045260245ffd5b50919050565b601f8211156104a557805f5260205f20601f840160051c810160208510156104835750805b601f840160051c820191505b818110156104a2575f815560010161048f565b50505b505050565b81516001600160401b038111156104c3576104c361029f565b6104d7816104d18454610426565b8461045e565b602080601f83116001811461050a575f84156104f35750858301515b5f19600386901b1c1916600185901b178555610561565b5f85815260208120601f198616915b8281101561053857888601518255948401946001909101908401610519565b508582101561055557878501515f19600388901b60f8161c191681555b505060018460011b0185555b505050505050565b634e487b7160e01b5f52602160045260245ffd5b6102758061058a5f395ff3fe608060405234801561000f575f80fd5b506004361061007a575f3560e01c806351ff48471161005857806351ff4847146100da5780635600f04f146100e2578063b80777ea146100ea578063ca973727146100f3575f80fd5b806310c83e531461007e578063238ac9331461009a5780634a79d50c146100c5575b5f80fd5b61008760025481565b6040519081526020015b60405180910390f35b6004546100ad906001600160a01b031681565b6040516001600160a01b039091168152602001610091565b6100cd610106565b60405161009191906101ee565b6100cd610191565b6100cd61019e565b61008760065481565b6005546100ad906001600160a01b031681565b5f805461011290610207565b80601f016020809104026020016040519081016040528092919081815260200182805461013e90610207565b80156101895780601f1061016057610100808354040283529160200191610189565b820191905f5260205f20905b81548152906001019060200180831161016c57829003601f168201915b505050505081565b6003805461011290610207565b6001805461011290610207565b5f81518084525f5b818110156101cf576020818501810151868301820152016101b3565b505f602082860101526020601f19601f83011685010191505092915050565b602081525f61020060208301846101ab565b9392505050565b600181811c9082168061021b57607f821691505b60208210810361023957634e487b7160e01b5f52602260045260245ffd5b5091905056fea26469706673582212209e59bd1384402e8bf43c9958338c32753916f9addea53f7f5aad24b62934ae5864736f6c63430008170033";
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
