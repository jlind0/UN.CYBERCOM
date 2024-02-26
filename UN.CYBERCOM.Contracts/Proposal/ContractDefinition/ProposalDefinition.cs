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

    public partial class DurationFunction : DurationFunctionBase { }

    [Function("duration", "uint256")]
    public class DurationFunctionBase : FunctionMessage
    {

    }

    public partial class GetDocumentsFunction : GetDocumentsFunctionBase { }

    [Function("getDocuments", typeof(GetDocumentsOutputDTO))]
    public class GetDocumentsFunctionBase : FunctionMessage
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

    public partial class OwnerFunction : OwnerFunctionBase { }

    [Function("owner", "address")]
    public class OwnerFunctionBase : FunctionMessage
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

    public partial class StartVotingFunction : StartVotingFunctionBase { }

    [Function("startVoting")]
    public class StartVotingFunctionBase : FunctionMessage
    {
        [Parameter("address", "sender", 1)]
        public virtual string Sender { get; set; }
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

    public partial class VotingStartedFunction : VotingStartedFunctionBase { }

    [Function("votingStarted", "bool")]
    public class VotingStartedFunctionBase : FunctionMessage
    {

    }

    public partial class StatusUpdatedEventDTO : StatusUpdatedEventDTOBase { }

    [Event("StatusUpdated")]
    public class StatusUpdatedEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "proposalId", 1, true )]
        public virtual BigInteger ProposalId { get; set; }
        [Parameter("uint8", "newStatus", 2, false )]
        public virtual byte NewStatus { get; set; }
    }

    public partial class VoteCastedEventDTO : VoteCastedEventDTOBase { }

    [Event("VoteCasted")]
    public class VoteCastedEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "proposalId", 1, true )]
        public virtual BigInteger ProposalId { get; set; }
        [Parameter("address", "member", 2, false )]
        public virtual string Member { get; set; }
        [Parameter("bool", "vote", 3, false )]
        public virtual bool Vote { get; set; }
    }

    public partial class VotingCompletedEventDTO : VotingCompletedEventDTOBase { }

    [Event("VotingCompleted")]
    public class VotingCompletedEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "proposalId", 1, true )]
        public virtual BigInteger ProposalId { get; set; }
    }

    public partial class VotingStartedEventDTO : VotingStartedEventDTOBase { }

    [Event("VotingStarted")]
    public class VotingStartedEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "proposalId", 1, true )]
        public virtual BigInteger ProposalId { get; set; }
    }

    public partial class AuthorizationErrorError : AuthorizationErrorErrorBase { }
    [Error("AuthorizationError")]
    public class AuthorizationErrorErrorBase : IErrorDTO
    {
    }

    public partial class VotingClosedError : VotingClosedErrorBase { }
    [Error("VotingClosed")]
    public class VotingClosedErrorBase : IErrorDTO
    {
    }

    public partial class VotingNotStartedError : VotingNotStartedErrorBase { }
    [Error("VotingNotStarted")]
    public class VotingNotStartedErrorBase : IErrorDTO
    {
    }



    public partial class DurationOutputDTO : DurationOutputDTOBase { }

    [FunctionOutput]
    public class DurationOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class GetDocumentsOutputDTO : GetDocumentsOutputDTOBase { }

    [FunctionOutput]
    public class GetDocumentsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("tuple[]", "", 1)]
        public virtual List<Doc> ReturnValue1 { get; set; }
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

    public partial class OwnerOutputDTO : OwnerOutputDTOBase { }

    [FunctionOutput]
    public class OwnerOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
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





    public partial class VotingStartedOutputDTO : VotingStartedOutputDTOBase { }

    [FunctionOutput]
    public class VotingStartedOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }
}
