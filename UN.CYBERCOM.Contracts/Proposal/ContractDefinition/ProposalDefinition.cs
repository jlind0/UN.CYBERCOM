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

namespace UN.CYBERCOM.Contracts.Proposal.ContractDefinition
{


    public partial class ProposalDeployment : ProposalDeploymentBase
    {
        public ProposalDeployment() : base(BYTECODE) { }
        public ProposalDeployment(string byteCode) : base(byteCode) { }
    }

    public class ProposalDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "";
        public ProposalDeploymentBase() : base(BYTECODE) { }
        public ProposalDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class DurationFunction : DurationFunctionBase { }

    [Function("duration", "uint256")]
    public class DurationFunctionBase : FunctionMessage
    {

    }

    public partial class GetVotesFunction : GetVotesFunctionBase { }

    [Function("getVotes", typeof(GetVotesOutputDTO))]
    public class GetVotesFunctionBase : FunctionMessage
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

    public partial class GetVotesOutputDTO : GetVotesOutputDTOBase { }

    [FunctionOutput]
    public class GetVotesOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("tuple[]", "", 1)]
        public virtual List<Vote> ReturnValue1 { get; set; }
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
