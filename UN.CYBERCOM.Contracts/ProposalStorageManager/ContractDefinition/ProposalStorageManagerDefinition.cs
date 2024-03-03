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
        public static string BYTECODE = "60806040525f600755348015610013575f80fd5b5060405161082438038061082483398101604081905261003291610056565b5f80546001600160a01b0319166001600160a01b0392909216919091179055610083565b5f60208284031215610066575f80fd5b81516001600160a01b038116811461007c575f80fd5b9392505050565b610794806100905f395ff3fe608060405234801561000f575f80fd5b50600436106100ef575f3560e01c80638e81236611610093578063d6a2b21411610063578063d6a2b2141461021f578063da35c6641461024a578063f73c2c6214610253578063fbe53c2f1461025b575f80fd5b80638e812366146101c75780639b28221f146101da578063c3a47fd8146101ef578063c7f758a8146101f7575f80fd5b80634445a491116100ce5780634445a491146101605780634c6d25f014610173578063787e0fe01461019e5780637c53abbe146101b4575f80fd5b8062b9f954146100f3578063013cf08b146101085780631b6f984b1461014d575b5f80fd5b610106610101366004610665565b61026e565b005b61013061011636600461068f565b60036020525f90815260409020546001600160a01b031681565b6040516001600160a01b0390911681526020015b60405180910390f35b61010661015b3660046106a6565b6102c6565b61010661016e3660046106ce565b61031e565b6101306101813660046106ce565b6001600160a01b039081165f908152600160205260409020541690565b6101a661039a565b604051908152602001610144565b6101066101c23660046106ce565b6103de565b6101066101d53660046106ce565b61045a565b6101e26104d6565b60405161014491906106ee565b6101e2610536565b61013061020536600461068f565b5f908152600360205260409020546001600160a01b031690565b61013061022d3660046106ce565b6001600160a01b039081165f908152600260205260409020541690565b6101a660075481565b6101e2610594565b6101066102693660046106a6565b6105f2565b5f54336001600160a01b039091160361029957604051621607ef60ea1b815260040160405180910390fd5b5f9182526003602052604090912080546001600160a01b0319166001600160a01b03909216919091179055565b5f54336001600160a01b03909116036102f157604051621607ef60ea1b815260040160405180910390fd5b6001600160a01b039182165f90815260016020526040902080546001600160a01b03191691909216179055565b5f54336001600160a01b039091160361034957604051621607ef60ea1b815260040160405180910390fd5b600580546001810182555f919091527f036b6384b5eca791c62761152d0c79bb0604c104a5fb6f4eb0703f3154bb3db00180546001600160a01b0319166001600160a01b0392909216919091179055565b5f8054336001600160a01b03909116036103c657604051621607ef60ea1b815260040160405180910390fd5b60075f81546103d49061073a565b9182905550905090565b5f54336001600160a01b039091160361040957604051621607ef60ea1b815260040160405180910390fd5b600680546001810182555f919091527ff652222313e28459528d920b65115c16c04f3efc82aaedc97be59f3f377c0d3f0180546001600160a01b0319166001600160a01b0392909216919091179055565b5f54336001600160a01b039091160361048557604051621607ef60ea1b815260040160405180910390fd5b600480546001810182555f919091527f8a35acfbc15ff81a39ae7d344fd709f28e8600b4aa8c65c6b64bfe7fe36bd19b0180546001600160a01b0319166001600160a01b0392909216919091179055565b6060600580548060200260200160405190810160405280929190818152602001828054801561052c57602002820191905f5260205f20905b81546001600160a01b0316815260019091019060200180831161050e575b5050505050905090565b6060600680548060200260200160405190810160405280929190818152602001828054801561052c57602002820191905f5260205f209081546001600160a01b0316815260019091019060200180831161050e575050505050905090565b6060600480548060200260200160405190810160405280929190818152602001828054801561052c57602002820191905f5260205f209081546001600160a01b0316815260019091019060200180831161050e575050505050905090565b5f54336001600160a01b039091160361061d57604051621607ef60ea1b815260040160405180910390fd5b6001600160a01b039182165f90815260026020526040902080546001600160a01b03191691909216179055565b80356001600160a01b0381168114610660575f80fd5b919050565b5f8060408385031215610676575f80fd5b823591506106866020840161064a565b90509250929050565b5f6020828403121561069f575f80fd5b5035919050565b5f80604083850312156106b7575f80fd5b6106c08361064a565b91506106866020840161064a565b5f602082840312156106de575f80fd5b6106e78261064a565b9392505050565b602080825282518282018190525f9190848201906040850190845b8181101561072e5783516001600160a01b031683529284019291840191600101610709565b50909695505050505050565b5f6001820161075757634e487b7160e01b5f52601160045260245ffd5b506001019056fea26469706673582212205c739d7bc715cc7191b709c6ada2501c9f34f3f2e17a25885bc0f742df46bc0964736f6c63430008170033";
        public ProposalStorageManagerDeploymentBase() : base(BYTECODE) { }
        public ProposalStorageManagerDeploymentBase(string byteCode) : base(byteCode) { }
        [Parameter("address", "_daoAddress", 1)]
        public virtual string DaoAddress { get; set; }
    }

    public partial class AddChangeParametersProposalFunction : AddChangeParametersProposalFunctionBase { }

    [Function("addChangeParametersProposal")]
    public class AddChangeParametersProposalFunctionBase : FunctionMessage
    {
        [Parameter("address", "key", 1)]
        public virtual string Key { get; set; }
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

    public partial class GetChangeParametersProposalAddressesFunction : GetChangeParametersProposalAddressesFunctionBase { }

    [Function("getChangeParametersProposalAddresses", "address[]")]
    public class GetChangeParametersProposalAddressesFunctionBase : FunctionMessage
    {

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







    public partial class GetChangeParametersProposalAddressesOutputDTO : GetChangeParametersProposalAddressesOutputDTOBase { }

    [FunctionOutput]
    public class GetChangeParametersProposalAddressesOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address[]", "", 1)]
        public virtual List<string> ReturnValue1 { get; set; }
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
