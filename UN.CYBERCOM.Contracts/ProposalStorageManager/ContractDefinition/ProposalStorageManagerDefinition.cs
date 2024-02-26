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

namespace UN.CYBERCOM.Contracts.ProposalStorageManager.ContractDefinition
{


    public partial class ProposalStorageManagerDeployment : ProposalStorageManagerDeploymentBase
    {
        public ProposalStorageManagerDeployment() : base(BYTECODE) { }
        public ProposalStorageManagerDeployment(string byteCode) : base(byteCode) { }
    }

    public class ProposalStorageManagerDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "60806040525f600655348015610013575f80fd5b5060405161070a38038061070a83398101604081905261003291610056565b5f80546001600160a01b0319166001600160a01b0392909216919091179055610083565b5f60208284031215610066575f80fd5b81516001600160a01b038116811461007c575f80fd5b9392505050565b61067a806100905f395ff3fe608060405234801561000f575f80fd5b50600436106100ca575f3560e01c80638e81236611610088578063d6a2b21411610063578063d6a2b214146101df578063da35c6641461020a578063f73c2c6214610213578063fbe53c2f1461021b575f80fd5b80638e8123661461018f5780639b28221f146101a2578063c7f758a8146101b7575f80fd5b8062b9f954146100ce578063013cf08b146100e35780631b6f984b146101285780634445a4911461013b5780634c6d25f01461014e578063787e0fe014610179575b5f80fd5b6100e16100dc36600461054b565b61022e565b005b61010b6100f1366004610575565b60036020525f90815260409020546001600160a01b031681565b6040516001600160a01b0390911681526020015b60405180910390f35b6100e161013636600461058c565b610286565b6100e16101493660046105b4565b6102de565b61010b61015c3660046105b4565b6001600160a01b039081165f908152600160205260409020541690565b61018161035a565b60405190815260200161011f565b6100e161019d3660046105b4565b61039e565b6101aa61041a565b60405161011f91906105d4565b61010b6101c5366004610575565b5f908152600360205260409020546001600160a01b031690565b61010b6101ed3660046105b4565b6001600160a01b039081165f908152600260205260409020541690565b61018160065481565b6101aa61047a565b6100e161022936600461058c565b6104d8565b5f54336001600160a01b039091160361025957604051621607ef60ea1b815260040160405180910390fd5b5f9182526003602052604090912080546001600160a01b0319166001600160a01b03909216919091179055565b5f54336001600160a01b03909116036102b157604051621607ef60ea1b815260040160405180910390fd5b6001600160a01b039182165f90815260016020526040902080546001600160a01b03191691909216179055565b5f54336001600160a01b039091160361030957604051621607ef60ea1b815260040160405180910390fd5b600580546001810182555f919091527f036b6384b5eca791c62761152d0c79bb0604c104a5fb6f4eb0703f3154bb3db00180546001600160a01b0319166001600160a01b0392909216919091179055565b5f8054336001600160a01b039091160361038657604051621607ef60ea1b815260040160405180910390fd5b60065f815461039490610620565b9182905550905090565b5f54336001600160a01b03909116036103c957604051621607ef60ea1b815260040160405180910390fd5b600480546001810182555f919091527f8a35acfbc15ff81a39ae7d344fd709f28e8600b4aa8c65c6b64bfe7fe36bd19b0180546001600160a01b0319166001600160a01b0392909216919091179055565b6060600580548060200260200160405190810160405280929190818152602001828054801561047057602002820191905f5260205f20905b81546001600160a01b03168152600190910190602001808311610452575b5050505050905090565b6060600480548060200260200160405190810160405280929190818152602001828054801561047057602002820191905f5260205f209081546001600160a01b03168152600190910190602001808311610452575050505050905090565b5f54336001600160a01b039091160361050357604051621607ef60ea1b815260040160405180910390fd5b6001600160a01b039182165f90815260026020526040902080546001600160a01b03191691909216179055565b80356001600160a01b0381168114610546575f80fd5b919050565b5f806040838503121561055c575f80fd5b8235915061056c60208401610530565b90509250929050565b5f60208284031215610585575f80fd5b5035919050565b5f806040838503121561059d575f80fd5b6105a683610530565b915061056c60208401610530565b5f602082840312156105c4575f80fd5b6105cd82610530565b9392505050565b602080825282518282018190525f9190848201906040850190845b818110156106145783516001600160a01b0316835292840192918401916001016105ef565b50909695505050505050565b5f6001820161063d57634e487b7160e01b5f52601160045260245ffd5b506001019056fea2646970667358221220e0a3fc40790b9636696293a0823e4588e5d93e7d33053224d7d9df04ceb9172764736f6c63430008170033";
        public ProposalStorageManagerDeploymentBase() : base(BYTECODE) { }
        public ProposalStorageManagerDeploymentBase(string byteCode) : base(byteCode) { }
        [Parameter("address", "_daoAddress", 1)]
        public virtual string DaoAddress { get; set; }
    }

    public partial class AddMembershipProposalFunction : AddMembershipProposalFunctionBase { }

    [Function("addMembershipProposal")]
    public class AddMembershipProposalFunctionBase : FunctionMessage
    {
        [Parameter("address", "key", 1)]
        public virtual string Key { get; set; }
    }

    public partial class AddMembershipRemovalProposalFunction : AddMembershipRemovalProposalFunctionBase { }

    [Function("addMembershipRemovalProposal")]
    public class AddMembershipRemovalProposalFunctionBase : FunctionMessage
    {
        [Parameter("address", "key", 1)]
        public virtual string Key { get; set; }
    }

    public partial class GetMembershipProposalFunction : GetMembershipProposalFunctionBase { }

    [Function("getMembershipProposal", "address")]
    public class GetMembershipProposalFunctionBase : FunctionMessage
    {
        [Parameter("address", "key", 1)]
        public virtual string Key { get; set; }
    }

    public partial class GetMembershipProposalAddressesFunction : GetMembershipProposalAddressesFunctionBase { }

    [Function("getMembershipProposalAddresses", "address[]")]
    public class GetMembershipProposalAddressesFunctionBase : FunctionMessage
    {

    }

    public partial class GetMembershipRemovalProposalFunction : GetMembershipRemovalProposalFunctionBase { }

    [Function("getMembershipRemovalProposal", "address")]
    public class GetMembershipRemovalProposalFunctionBase : FunctionMessage
    {
        [Parameter("address", "key", 1)]
        public virtual string Key { get; set; }
    }

    public partial class GetMembershipRemovalProposalAddressesFunction : GetMembershipRemovalProposalAddressesFunctionBase { }

    [Function("getMembershipRemovalProposalAddresses", "address[]")]
    public class GetMembershipRemovalProposalAddressesFunctionBase : FunctionMessage
    {

    }

    public partial class GetNextProposalIdFunction : GetNextProposalIdFunctionBase { }

    [Function("getNextProposalId", "uint256")]
    public class GetNextProposalIdFunctionBase : FunctionMessage
    {

    }

    public partial class GetProposalFunction : GetProposalFunctionBase { }

    [Function("getProposal", "address")]
    public class GetProposalFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "key", 1)]
        public virtual BigInteger Key { get; set; }
    }

    public partial class ProposalCountFunction : ProposalCountFunctionBase { }

    [Function("proposalCount", "uint256")]
    public class ProposalCountFunctionBase : FunctionMessage
    {

    }

    public partial class ProposalsFunction : ProposalsFunctionBase { }

    [Function("proposals", "address")]
    public class ProposalsFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class SetMembershipProposalFunction : SetMembershipProposalFunctionBase { }

    [Function("setMembershipProposal")]
    public class SetMembershipProposalFunctionBase : FunctionMessage
    {
        [Parameter("address", "key", 1)]
        public virtual string Key { get; set; }
        [Parameter("address", "value", 2)]
        public virtual string Value { get; set; }
    }

    public partial class SetMembershipRemovalProposalFunction : SetMembershipRemovalProposalFunctionBase { }

    [Function("setMembershipRemovalProposal")]
    public class SetMembershipRemovalProposalFunctionBase : FunctionMessage
    {
        [Parameter("address", "key", 1)]
        public virtual string Key { get; set; }
        [Parameter("address", "value", 2)]
        public virtual string Value { get; set; }
    }

    public partial class SetProposalFunction : SetProposalFunctionBase { }

    [Function("setProposal")]
    public class SetProposalFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "key", 1)]
        public virtual BigInteger Key { get; set; }
        [Parameter("address", "value", 2)]
        public virtual string Value { get; set; }
    }

    public partial class AuthorizationErrorError : AuthorizationErrorErrorBase { }
    [Error("AuthorizationError")]
    public class AuthorizationErrorErrorBase : IErrorDTO
    {
    }





    public partial class GetMembershipProposalOutputDTO : GetMembershipProposalOutputDTOBase { }

    [FunctionOutput]
    public class GetMembershipProposalOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class GetMembershipProposalAddressesOutputDTO : GetMembershipProposalAddressesOutputDTOBase { }

    [FunctionOutput]
    public class GetMembershipProposalAddressesOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address[]", "", 1)]
        public virtual List<string> ReturnValue1 { get; set; }
    }

    public partial class GetMembershipRemovalProposalOutputDTO : GetMembershipRemovalProposalOutputDTOBase { }

    [FunctionOutput]
    public class GetMembershipRemovalProposalOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class GetMembershipRemovalProposalAddressesOutputDTO : GetMembershipRemovalProposalAddressesOutputDTOBase { }

    [FunctionOutput]
    public class GetMembershipRemovalProposalAddressesOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address[]", "", 1)]
        public virtual List<string> ReturnValue1 { get; set; }
    }



    public partial class GetProposalOutputDTO : GetProposalOutputDTOBase { }

    [FunctionOutput]
    public class GetProposalOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class ProposalCountOutputDTO : ProposalCountOutputDTOBase { }

    [FunctionOutput]
    public class ProposalCountOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class ProposalsOutputDTO : ProposalsOutputDTOBase { }

    [FunctionOutput]
    public class ProposalsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }






}
