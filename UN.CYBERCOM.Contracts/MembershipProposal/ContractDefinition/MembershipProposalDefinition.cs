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

namespace UN.CYBERCOM.Contracts.MembershipProposal.ContractDefinition
{


    public partial class MembershipProposalDeployment : MembershipProposalDeploymentBase
    {
        public MembershipProposalDeployment() : base(BYTECODE) { }
        public MembershipProposalDeployment(string byteCode) : base(byteCode) { }
    }

    public class MembershipProposalDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "60806040526005805460ff191690553480156200001a575f80fd5b5060405162000e0338038062000e038339810160408190526200003d91620001bb565b600980546001600160a01b03808b166001600160a01b031992831617909255600a8054838b16908316179055600b8054928916929091169190911790555f8590556001805489918991899189918991899183919060ff191681836002811115620000ab57620000ab62000313565b0217905550603c811015620000be5750603c5b620000ca428262000327565b60025550506003805460ff1916905550504260045550508151600c80546001600160a01b0319166001600160a01b039092169190911781556020830151839190600d90620001199082620003d7565b505050600e5550620004a395505050505050565b80516001600160a01b038116811462000144575f80fd5b919050565b634e487b7160e01b5f52604160045260245ffd5b604080519081016001600160401b038111828210171562000182576200018262000149565b60405290565b604051601f8201601f191681016001600160401b0381118282101715620001b357620001b362000149565b604052919050565b5f805f805f805f80610100898b031215620001d4575f80fd5b620001df896200012d565b97506020620001f0818b016200012d565b97506200020060408b016200012d565b965060608a0151955060808a0151600381106200021b575f80fd5b60a08b015160c08c015191965094506001600160401b03808211156200023f575f80fd5b908b01906040828e03121562000253575f80fd5b6200025d6200015d565b62000268836200012d565b815283830151828111156200027b575f80fd5b8084019350508d601f84011262000290575f80fd5b825182811115620002a557620002a562000149565b620002b9601f8201601f1916860162000188565b92508083528e85828601011115620002cf575f80fd5b5f5b81811015620002ee578481018601518482018701528501620002d1565b505f908301850152928301525060e09990990151979a96995094979396929591945050565b634e487b7160e01b5f52602160045260245ffd5b808201808211156200034757634e487b7160e01b5f52601160045260245ffd5b92915050565b600181811c908216806200036257607f821691505b6020821081036200038157634e487b7160e01b5f52602260045260245ffd5b50919050565b601f821115620003d257805f5260205f20601f840160051c81016020851015620003ae5750805b601f840160051c820191505b81811015620003cf575f8155600101620003ba565b50505b505050565b81516001600160401b03811115620003f357620003f362000149565b6200040b816200040484546200034d565b8462000387565b602080601f83116001811462000441575f8415620004295750858301515b5f19600386901b1c1916600185901b1785556200049b565b5f85815260208120601f198616915b82811015620004715788860151825594840194600190910190840162000450565b50858210156200048f57878501515f19600388901b60f8161c191681555b505060018460011b0185555b505050505050565b61095280620004b15f395ff3fe608060405234801561000f575f80fd5b50600436106100e5575f3560e01c8063633dfc7011610088578063b46a357f11610063578063b46a357f146101b7578063b80777ea146101cc578063ccbac9f5146101d5578063d6bfea28146101de575f80fd5b8063633dfc7014610193578063a0f44c92146101a6578063af640d0f146101af575f80fd5b80630f792235116100c35780630f792235146101355780630fb5a6b414610148578063200d2ed21461015f578063351d9f9614610179575f80fd5b806302484895146100e95780630b3af7f91461010b5780630dc9601514610120575b5f80fd5b6005546100f69060ff1681565b60405190151581526020015b60405180910390f35b61011e6101193660046106d8565b6101f1565b005b610128610295565b60405161010291906106fd565b61011e61014336600461077f565b6103c5565b61015160025481565b604051908152602001610102565b60035461016c9060ff1681565b60405161010291906107ac565b6001546101869060ff1681565b60405161010291906107c6565b61011e6101a13660046107da565b610432565b610151600e5481565b6101515f5481565b6101bf6105b4565b604051610102919061081a565b61015160045481565b61015160065481565b61011e6101ec366004610881565b610679565b6009546001600160a01b03163314806102145750600a546001600160a01b031633145b61026f5760405162461bcd60e51b815260206004820152602160248201527f4d7573742062652063616c6c65642066726f6d2044414f206f7220566f74696e6044820152606760f81b60648201526084015b60405180910390fd5b6003805482919060ff19166001838381111561028d5761028d610798565b021790555050565b60605f8060078054905067ffffffffffffffff8111156102b7576102b7610898565b60405190808252806020026020018201604052801561030757816020015b604080516080810182525f8082526020808301829052928201819052606082015282525f199092019101816102d55790505b5090505b6007548210156103bf5760085f6007848154811061032b5761032b6108ac565b5f918252602080832091909101546001600160a01b039081168452838201949094526040928301909120825160808101845281549485168152600160a01b90940460ff1615159184019190915260018101549183019190915260020154606082015281518290849081106103a1576103a16108ac565b602002602001018190525081806103b7906108c0565b92505061030b565b92915050565b600a546001600160a01b0316331461041f5760405162461bcd60e51b815260206004820152601a60248201527f4d7573742062652063616c6c65642066726f6d20566f74696e670000000000006044820152606401610266565b6005805460ff1916911515919091179055565b6009546001600160a01b0316331461048c5760405162461bcd60e51b815260206004820152601760248201527f4d7573742062652063616c6c65642066726f6d2044414f0000000000000000006044820152606401610266565b4260025410156104d25760405162461bcd60e51b8152602060048201526011602482015270159bdd1a5b99c81a185cc818db1bdcd959607a1b6044820152606401610266565b5f80546001600160a01b038316825260086020526040909120600201541461053f57600780546001810182555f919091527fa66cc928b5edb82af9bd49922954155ab7b0942694bea4ce44661d9a8736c6880180546001600160a01b0319166001600160a01b0383161790555b604080516080810182526001600160a01b039283168082529315156020808301918252428385019081525f80546060860190815297815260089092529390209151825491511515600160a01b026001600160a81b03199092169416939093179290921782555160018201559051600290910155565b604080518082019091525f81526060602082015260408051808201909152600c80546001600160a01b03168252600d80546020840191906105f4906108e4565b80601f0160208091040260200160405190810160405280929190818152602001828054610620906108e4565b801561066b5780601f106106425761010080835404028352916020019161066b565b820191905f5260205f20905b81548152906001019060200180831161064e57829003601f168201915b505050505081525050905090565b600a546001600160a01b031633146106d35760405162461bcd60e51b815260206004820152601a60248201527f4d7573742062652063616c6c65642066726f6d20566f74696e670000000000006044820152606401610266565b600655565b5f602082840312156106e8575f80fd5b8135600481106106f6575f80fd5b9392505050565b602080825282518282018190525f919060409081850190868401855b8281101561075e57815180516001600160a01b031685528681015115158786015285810151868601526060908101519085015260809093019290850190600101610719565b5091979650505050505050565b8035801515811461077a575f80fd5b919050565b5f6020828403121561078f575f80fd5b6106f68261076b565b634e487b7160e01b5f52602160045260245ffd5b60208101600483106107c0576107c0610798565b91905290565b60208101600383106107c0576107c0610798565b5f80604083850312156107eb575f80fd5b6107f48361076b565b915060208301356001600160a01b038116811461080f575f80fd5b809150509250929050565b5f602080835260018060a01b038451166020840152602084015160408085015280518060608601525f5b8181101561086057828101840151868201608001528301610844565b505f608082870101526080601f19601f830116860101935050505092915050565b5f60208284031215610891575f80fd5b5035919050565b634e487b7160e01b5f52604160045260245ffd5b634e487b7160e01b5f52603260045260245ffd5b5f600182016108dd57634e487b7160e01b5f52601160045260245ffd5b5060010190565b600181811c908216806108f857607f821691505b60208210810361091657634e487b7160e01b5f52602260045260245ffd5b5091905056fea2646970667358221220fdf4540a7174c42b71f505f656dc32d342abaf733d13d8fa980284c12dc5087364736f6c63430008170033";
        public MembershipProposalDeploymentBase() : base(BYTECODE) { }
        public MembershipProposalDeploymentBase(string byteCode) : base(byteCode) { }
        [Parameter("address", "_daoAddress", 1)]
        public virtual string DaoAddress { get; set; }
        [Parameter("address", "_votingAddress", 2)]
        public virtual string VotingAddress { get; set; }
        [Parameter("address", "_councilManager", 3)]
        public virtual string CouncilManager { get; set; }
        [Parameter("uint256", "_id", 4)]
        public virtual BigInteger Id { get; set; }
        [Parameter("uint8", "_proposalType", 5)]
        public virtual byte ProposalType { get; set; }
        [Parameter("uint256", "_duration", 6)]
        public virtual BigInteger Duration { get; set; }
        [Parameter("tuple", "_nation", 7)]
        public virtual Nation Nation { get; set; }
        [Parameter("uint256", "_groupId", 8)]
        public virtual BigInteger GroupId { get; set; }
    }

    public partial class DurationFunction : DurationFunctionBase { }

    [Function("duration", "uint256")]
    public class DurationFunctionBase : FunctionMessage
    {

    }

    public partial class GetNationFunction : GetNationFunctionBase { }

    [Function("getNation", typeof(GetNationOutputDTO))]
    public class GetNationFunctionBase : FunctionMessage
    {

    }

    public partial class GetVotesFunction : GetVotesFunctionBase { }

    [Function("getVotes", typeof(GetVotesOutputDTO))]
    public class GetVotesFunctionBase : FunctionMessage
    {

    }

    public partial class GroupIdFunction : GroupIdFunctionBase { }

    [Function("groupId", "uint256")]
    public class GroupIdFunctionBase : FunctionMessage
    {

    }

    public partial class IdFunction : IdFunctionBase { }

    [Function("id", "uint256")]
    public class IdFunctionBase : FunctionMessage
    {

    }

    public partial class IsProcessingFunction : IsProcessingFunctionBase { }

    [Function("isProcessing", "bool")]
    public class IsProcessingFunctionBase : FunctionMessage
    {

    }

    public partial class ProposalTypeFunction : ProposalTypeFunctionBase { }

    [Function("proposalType", "uint8")]
    public class ProposalTypeFunctionBase : FunctionMessage
    {

    }

    public partial class RandomNumberFunction : RandomNumberFunctionBase { }

    [Function("randomNumber", "uint256")]
    public class RandomNumberFunctionBase : FunctionMessage
    {

    }

    public partial class SetProcessingFunction : SetProcessingFunctionBase { }

    [Function("setProcessing")]
    public class SetProcessingFunctionBase : FunctionMessage
    {
        [Parameter("bool", "processing", 1)]
        public virtual bool Processing { get; set; }
    }

    public partial class SetRandomNumberFunction : SetRandomNumberFunctionBase { }

    [Function("setRandomNumber")]
    public class SetRandomNumberFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "random", 1)]
        public virtual BigInteger Random { get; set; }
    }

    public partial class StatusFunction : StatusFunctionBase { }

    [Function("status", "uint8")]
    public class StatusFunctionBase : FunctionMessage
    {

    }

    public partial class TimestampFunction : TimestampFunctionBase { }

    [Function("timestamp", "uint256")]
    public class TimestampFunctionBase : FunctionMessage
    {

    }

    public partial class UpdateStatusFunction : UpdateStatusFunctionBase { }

    [Function("updateStatus")]
    public class UpdateStatusFunctionBase : FunctionMessage
    {
        [Parameter("uint8", "_status", 1)]
        public virtual byte Status { get; set; }
    }

    public partial class VoteFunction : VoteFunctionBase { }

    [Function("vote")]
    public class VoteFunctionBase : FunctionMessage
    {
        [Parameter("bool", "voteCasted", 1)]
        public virtual bool VoteCasted { get; set; }
        [Parameter("address", "member", 2)]
        public virtual string Member { get; set; }
    }

    public partial class DurationOutputDTO : DurationOutputDTOBase { }

    [FunctionOutput]
    public class DurationOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class GetNationOutputDTO : GetNationOutputDTOBase { }

    [FunctionOutput]
    public class GetNationOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("tuple", "", 1)]
        public virtual Nation ReturnValue1 { get; set; }
    }

    public partial class GetVotesOutputDTO : GetVotesOutputDTOBase { }

    [FunctionOutput]
    public class GetVotesOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("tuple[]", "", 1)]
        public virtual List<Vote> ReturnValue1 { get; set; }
    }

    public partial class GroupIdOutputDTO : GroupIdOutputDTOBase { }

    [FunctionOutput]
    public class GroupIdOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class IdOutputDTO : IdOutputDTOBase { }

    [FunctionOutput]
    public class IdOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class IsProcessingOutputDTO : IsProcessingOutputDTOBase { }

    [FunctionOutput]
    public class IsProcessingOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }

    public partial class ProposalTypeOutputDTO : ProposalTypeOutputDTOBase { }

    [FunctionOutput]
    public class ProposalTypeOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint8", "", 1)]
        public virtual byte ReturnValue1 { get; set; }
    }

    public partial class RandomNumberOutputDTO : RandomNumberOutputDTOBase { }

    [FunctionOutput]
    public class RandomNumberOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }





    public partial class StatusOutputDTO : StatusOutputDTOBase { }

    [FunctionOutput]
    public class StatusOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint8", "", 1)]
        public virtual byte ReturnValue1 { get; set; }
    }

    public partial class TimestampOutputDTO : TimestampOutputDTOBase { }

    [FunctionOutput]
    public class TimestampOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }




}
