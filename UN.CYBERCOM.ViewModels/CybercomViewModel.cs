using DynamicData;
using DynamicData.Binding;
using Microsoft.Extensions.Configuration;
using Nethereum.Contracts;
using Nethereum.Web3;
using Nethereum.Contracts;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using UN.CYBERCOM.Contracts.CYBERCOM;
using UNCYOLD = UN.CYBERCOM.Contracts.CYBERCOM.OLD;
using UN.CYBERCOM.Contracts.CYBERCOM.ContractDefinition;
using UNCYOLDCD = UN.CYBERCOM.Contracts.CYBERCOM.OLD.ContractDefinition;
using AutoMapper;

namespace UN.CYBERCOM.ViewModels
{
    public class CybercomViewModel : ReactiveObject
    {
        public Interaction<string, bool> Alert { get; } = new Interaction<string, bool>();
        public Interaction<string, string> SignatureRequest { get; } = new Interaction<string, string>();
        public ReactiveCommand<Unit, Unit> Load { get; }
        public ReactiveCommand<Unit, Unit> Deploy { get; }
        public ReactiveCommand<Unit, Unit> MembershipRequest { get; }
        internal Web3 W3 { get; }
        private string? accountNumber;
        public string? AccountNumber
        {
            get => accountNumber;
            set => this.RaiseAndSetIfChanged(ref accountNumber, value);
        }
        private CybercomService? cybercomService;
        public CybercomService? CyberService
        {
            get => cybercomService;
            set => this.RaiseAndSetIfChanged(ref cybercomService, value);
        }
        private bool isDeployed;
        public bool IsDeployed
        {
            get => isDeployed;
            set => this.RaiseAndSetIfChanged(ref isDeployed, value);
        }
        private bool isLoading;
        public bool IsLoading
        {
            get => isLoading;
            set => this.RaiseAndSetIfChanged(ref isLoading, value);
        }
        private string? contractAddress;
        public string? ContractAddress
        {
            get => contractAddress;
            set => this.RaiseAndSetIfChanged(ref contractAddress, value);
        }
        private string? selectedCouncilRole;
        public string? SelectedCouncilRole
        {
            get => selectedCouncilRole;
            set
            { 
                this.RaiseAndSetIfChanged(ref selectedCouncilRole, value);
                this.RaisePropertyChanged(nameof(SelectedCouncil));
            }
        }
        public CouncilViewModel? SelectedCouncil
        {
            get => Councils.SingleOrDefault(c => c.Role == selectedCouncilRole);
        }
        private string? newNationName;
        public string? NewNationName
        {
            get => newNationName;
            set => this.RaiseAndSetIfChanged(ref newNationName, value);
        }
        private string? newMemberAddress;
        public string? NewMemberAddress
        {
            get => newMemberAddress;
            set => this.RaiseAndSetIfChanged(ref newMemberAddress, value);
        }

        protected ulong SubscriptionId { get; }
        private readonly CompositeDisposable disposable = new CompositeDisposable();
        public ObservableCollection<CouncilViewModel> Councils { get; } = new ObservableCollection<CouncilViewModel>();
        public ObservableCollection<Nation> Nations { get; } = new ObservableCollection<Nation>();
        public ObservableCollection<MembershipProposalViewModel> PendingMembershipProposals { get; } = new ObservableCollection<MembershipProposalViewModel>();
        public ObservableCollection<MembershipProposalViewModel> ReadyMembershipProposals { get; } = new ObservableCollection<MembershipProposalViewModel>();
        public ObservableCollection<MembershipProposalViewModel> ApprovedMembershipProposals { get; } = new ObservableCollection<MembershipProposalViewModel>();
        public ObservableCollection<MembershipProposalViewModel> RejectedMembershipProposals { get; } = new ObservableCollection<MembershipProposalViewModel>();
        protected string? OldContractAddress { get; }
        public CybercomViewModel(Web3 w3, IConfiguration config)
        {
            W3 = w3;
            IsDeployed = bool.Parse(config["CYBERCOM:IsDeployed"] ?? throw new InvalidDataException());
            Load = ReactiveCommand.CreateFromTask(DoLoad);
            Deploy = ReactiveCommand.CreateFromTask(DoDeploy);
            MembershipRequest = ReactiveCommand.CreateFromTask(DoMembershipRequest);
            ContractAddress = config["CYBERCOM:Address"];
            OldContractAddress = config["CYBERCOM:OldAddress"];
            SubscriptionId = ulong.Parse(config["VRF:SubscriptionId"] ?? throw new InvalidDataException());
            this.WhenPropertyChanged(p => p.AccountNumber).Subscribe(p =>
            {
                if (!IsDeployed)
                    return;
                if (p.Value != null)
                    CyberService = new CybercomService(W3, ContractAddress ?? throw new InvalidDataException());
                else
                    CyberService = null;
            }).DisposeWith(disposable);
            this.WhenPropertyChanged(p => p.CyberService).Subscribe(async p =>
            {
                if (!IsDeployed)
                    return;
                await DoLoad();
            }).DisposeWith(disposable);
            this.WhenPropertyChanged(p => p.IsDeployed).Subscribe(async p =>
            {
                if (p.Value)
                    await DoLoad();
            });
        }
        protected async Task DoMembershipRequest()
        {
            try
            {
                if (IsDeployed && CyberService != null && 
                    !string.IsNullOrWhiteSpace(SelectedCouncilRole) && 
                    !string.IsNullOrWhiteSpace(SelectedCouncil?.SelectedGroupId) &&
                    !string.IsNullOrWhiteSpace(NewMemberAddress) &&
                    !string.IsNullOrWhiteSpace(NewNationName))
                {
                    IsLoading = true;
                    var tran = new SubmitMembershipProposalFunction()
                    {
                        FromAddress = AccountNumber,
                        Request = new MembershipProposalRequest()
                        {
                            Member = NewMemberAddress,
                            NewNation = new Nation()
                            {
                                Name = NewNationName,
                                Id = NewMemberAddress
                            },
                            GroupId = BigInteger.Parse(SelectedCouncil.SelectedGroupId),
                            Duration = BigInteger.Zero
                        },
                        AmountToSend = 0,
                        Gas = 1500000,
                        
                    };
                    var data = Convert.ToHexString(tran.GetCallData()).ToLower();
                    var signedData = await SignatureRequest.Handle(data).GetAwaiter();
                    var str = await W3.Eth.Transactions.SendRawTransaction.SendRequestAsync(signedData);
                    //await CyberService.SubmitMembershipProposalRequestAndWaitForReceiptAsync(new SubmitMembershipProposalFunction().DecodeInput(signedData));
                }
            }
            catch(Exception ex)
            {
                await Alert.Handle(ex.Message).GetAwaiter();
            }
            finally
            {
                IsLoading = false;
            }
        }
        protected async Task DoDeploy()
        {
            try
            {
                IsLoading = true;
                if (!IsDeployed)
                {
                    GetCouncilsOutputDTO? oldData = null;
                    if (OldContractAddress != null)
                    {
                        UNCYOLD.CybercomService cs = new UNCYOLD.CybercomService(W3, OldContractAddress);
                        oldData = await cs.GetCouncilsQueryAsync();
                    }
                    var rcpt = await CybercomService.DeployContractAndWaitForReceiptAsync(W3, new CybercomDeployment()
                    {
                        SubscriptionId = SubscriptionId
                    });
                    ContractAddress = rcpt.ContractAddress;
                    if (oldData != null) 
                    {
                        CybercomService cs = new CybercomService(W3, ContractAddress);
                        await cs.LoadOldCouncilsRequestAndWaitForReceiptAsync(new LoadOldCouncilsFunction()
                        {
                            OldCouncils = oldData.ReturnValue1
                        });
                    }
                    IsDeployed = true;
                }
            }
            catch (Exception ex)
            {
                await Alert.Handle(ex.Message).GetAwaiter();
            }
            finally
            {
                IsLoading = false;
            }
        }
        protected async Task DoLoad()
        {
            try
            {
                IsLoading = true;
                if (!IsDeployed)
                    return;
                if(CyberService != null)
                {
                    Councils.Clear();
                    var councilDto = await CyberService.GetCouncilsQueryAsync(new GetCouncilsFunction()
                    {
                        FromAddress = AccountNumber
                    });
                    Nations.Clear();
                    var nationDto = await CyberService.GetNationsQueryAsync(new GetNationsFunction()
                    {
                        FromAddress = AccountNumber
                    });
                    Nations.AddRange(nationDto.ReturnValue1);
                    Councils.AddRange(councilDto.ReturnValue1.Select(g => new CouncilViewModel(g)).ToArray());
                    PendingMembershipProposals.Clear();
                    var pendingDto = await CyberService.GetPendingMembershipRequestsQueryAsync(new GetPendingMembershipRequestsFunction()
                    {
                        FromAddress = AccountNumber
                    });
                    var dicCouncils = Councils.ToDictionary(g => g.Role, g => g.Data);
                    PendingMembershipProposals.AddRange(pendingDto.ReturnValue1.Select(g => new MembershipProposalViewModel(this, g, dicCouncils[Convert.ToBase64String(g.Council)])));
                    foreach (var pmp in PendingMembershipProposals)
                        await pmp.Load.Execute().GetAwaiter();
                    var readyDto = await CyberService.GetReadyMembershipRequestsQueryAsync(new GetReadyMembershipRequestsFunction()
                    {
                        FromAddress = AccountNumber
                    });
                    ReadyMembershipProposals.Clear();
                    ReadyMembershipProposals.AddRange(readyDto.ReturnValue1.Select(g => new MembershipProposalViewModel(this, g, dicCouncils[Convert.ToBase64String(g.Council)])));
                    foreach (var pmp in ReadyMembershipProposals)
                        await pmp.Load.Execute().GetAwaiter();
                    var rejectedDto = await CyberService.GetRejectedMembershipRequestsQueryAsync(new GetRejectedMembershipRequestsFunction()
                    {
                        FromAddress = AccountNumber
                    });
                    RejectedMembershipProposals.Clear();
                    RejectedMembershipProposals.AddRange(rejectedDto.ReturnValue1.Select(g => new MembershipProposalViewModel(this, g, dicCouncils[Convert.ToBase64String(g.Council)])));
                    foreach (var pmp in RejectedMembershipProposals)
                        await pmp.Load.Execute().GetAwaiter();
                    var approvedDto = await CyberService.GetApprovedMembershipRequestsQueryAsync(new GetApprovedMembershipRequestsFunction()
                    {
                        FromAddress = AccountNumber
                    });
                    ApprovedMembershipProposals.Clear();
                    ApprovedMembershipProposals.AddRange(approvedDto.ReturnValue1.Select(g => new MembershipProposalViewModel(this, g, dicCouncils[Convert.ToBase64String(g.Council)])));
                    foreach (var pmp in ApprovedMembershipProposals)
                        await pmp.Load.Execute().GetAwaiter();

                }
                else
                {
                    Councils.Clear();
                    Nations.Clear();
                    PendingMembershipProposals.Clear();
                    ReadyMembershipProposals.Clear();
                    RejectedMembershipProposals.Clear();
                }
            }
            catch (Exception ex)
            {
                await Alert.Handle(ex.Message).GetAwaiter();
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
    public class MembershipVoteProposalViewModel : ReactiveObject
    {
        public MembershipProposalViewModel Proposal { get; }
        
        public Vote Data { get; }
        public bool CastedVote { get => Data.VoteCasted; }
        public Nation Nation { get; }
        protected CybercomViewModel Root { get; }
        public MembershipVoteProposalViewModel(CybercomViewModel root, MembershipProposalViewModel proposal, Vote vote)
        {
            Root = root;
            Data = vote;
            Proposal = proposal;
            Nation = root.Nations.Single(n => n.Id == vote.Member);
        }
    }
    public class MembershipProposalViewModel : ReactiveObject
    {
        public ObservableCollection<MembershipVoteProposalViewModel> MembershipVotes { get; } = new ObservableCollection<MembershipVoteProposalViewModel>();
        public enum ApprovalStatus : byte
        {
            Pending,
            Ready,
            Approved,
            Rejected
        }
        protected Interaction<string, string> SignatureRequest { get; }
        protected Interaction<string, bool> Alert { get; }
        protected MembershipProposalResponse Data { get; }
        public Council Council { get; }
        public CouncilGroup CouncilGroup { get; }
        public string Id
        {
            get => Data.Id.ToString();
        }
        public Nation NewNation
        {
            get => Data.NewNation;
        }
        public string ProposalId
        {
            get => Data.Proposal.Id.ToString();
        }
        public DateTime Duration
        {
            get => Data.Proposal.Duration.FromUnixTimestamp();
        }
        public ReactiveCommand<Unit, Unit> Tally { get; }
        public ReactiveCommand<Unit, Unit> CompleteTally { get; }
        public ReactiveCommand<Unit, Unit> Vote { get; }
        public ReactiveCommand<Unit, Unit> Load { get; }
        private bool castVote;
        public bool CastVote
        {
            get => castVote;
            set => this.RaiseAndSetIfChanged(ref castVote, value);
        }
        public bool CanTally
        {
            get => ((ApprovalStatus)Data.Proposal.Status) == ApprovalStatus.Pending && Duration < DateTime.UtcNow && !Data.Proposal.IsProcessing;
        }
        public bool CanVote
        {
            get => !CanTally && !CanCompleteTally;
        }
        public bool CanCompleteTally
        {
            get => ((ApprovalStatus)Data.Proposal.Status) == ApprovalStatus.Ready;
        }
        protected CybercomViewModel Parent { get; }
        public MembershipProposalViewModel(CybercomViewModel vm, MembershipProposalResponse data, Council council)
        {
            Parent = vm;
            SignatureRequest = vm.SignatureRequest;
            Alert = vm.Alert;
            Data = data;
            Council = council;
            CouncilGroup = Council.Groups.Single(g => g.Id == Data.GroupId);
            Tally = ReactiveCommand.CreateFromTask(DoTally);
            CompleteTally = ReactiveCommand.CreateFromTask(DoCompleteTally);
            Vote = ReactiveCommand.CreateFromTask(DoVote);
            Load = ReactiveCommand.CreateFromTask(DoLoad);
        }
        protected async Task DoLoad()
        {
            try
            {
                if (Parent.CyberService != null)
                {
                    var votesDto = await Parent.CyberService.GetProposalVotesQueryAsync(new GetProposalVotesFunction()
                    {
                        ProposalId = Data.Proposal.Id,
                        FromAddress = Parent.AccountNumber
                    });
                    MembershipVotes.Clear();
                    MembershipVotes.AddRange(votesDto.ReturnValue1.Where(v => v.Votes.Count > 0).SelectMany(v => v.Votes).Where(v => v.Votes.Count > 0).SelectMany(v => v.Votes).Select(
                        v => new MembershipVoteProposalViewModel(Parent, this, v)));
                }
            }
            catch (Exception ex)
            {
                await Alert.Handle(ex.Message).GetAwaiter();
            }
        }
        protected async Task DoVote()
        {
            try
            {
                var tran = new PerformVoteFunction()
                {
                    FromAddress = Parent.AccountNumber,
                    ProposalId = Data.Proposal.Id,
                    AmountToSend = 0,
                    Gas = 1500000,
                    VoteCasted = CastVote
                };
                var data = Convert.ToHexString(tran.GetCallData()).ToLower();
                var signedData = await SignatureRequest.Handle(data).GetAwaiter();
                var str = await Parent.W3.Eth.Transactions.SendRawTransaction.SendRequestAsync(signedData);
            }
            catch (Exception ex)
            {
                await Alert.Handle(ex.Message).GetAwaiter();
            }
        }
        protected async Task DoCompleteTally()
        {
            try
            {
                if (CanCompleteTally)
                {
                    var tran = new CompleteVotingFunction()
                    {
                        FromAddress = Parent.AccountNumber,
                        ProposalId = Data.Proposal.Id,
                        AmountToSend = 0,
                        Gas = 1500000
                    };
                    var data = Convert.ToHexString(tran.GetCallData()).ToLower();
                    var signedData = await SignatureRequest.Handle(data).GetAwaiter();
                    var str = await Parent.W3.Eth.Transactions.SendRawTransaction.SendRequestAsync(signedData);
                }
            }
            catch(Exception ex)
            {
                await Alert.Handle(ex.Message).GetAwaiter();
            }
        }
        protected async Task DoTally()
        {
            try
            {
                if(CanTally)
                {
                    var tran = new PrepareTallyFunction()
                    {
                        FromAddress = Parent.AccountNumber,
                        ProposalId = Data.Proposal.Id,
                        AmountToSend = 0,
                        Gas = 1500000
                    };
                    var data = Convert.ToHexString(tran.GetCallData()).ToLower();
                    var signedData = await SignatureRequest.Handle(data).GetAwaiter();
                    var str = await Parent.W3.Eth.Transactions.SendRawTransaction.SendRequestAsync(signedData);
                }
            }
            catch(Exception ex)
            {
                await Alert.Handle(ex.Message).GetAwaiter();
            }
        }
    }
    public static class DateTimeExtensions
    {
        public static DateTime FromUnixTimestamp(this BigInteger timestamp) 
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return dateTime.AddSeconds(((long)timestamp));
        }
    }
    public class CouncilViewModel : ReactiveObject
    {
        public Council Data { get; }
        public string Name
        {
            get => Data.Name;
        }
        public byte[] RoleBytes
        {
            get => Data.Role;
        }
        public string Role
        {
            get => Convert.ToBase64String(Data.Role);
        }
        public ObservableCollection<CouncilGroupViewModel> Groups { get; }
        private string? selectedGrouplId;
        public string? SelectedGroupId
        {
            get => selectedGrouplId;
            set => this.RaiseAndSetIfChanged(ref selectedGrouplId, value);
        }
        public CouncilViewModel(Council data) 
        {
            Data = data;
            Groups = new ObservableCollection<CouncilGroupViewModel>(data.Groups.Select(g => new CouncilGroupViewModel(g)).ToArray());
        }
    }
    public class CouncilGroupViewModel : ReactiveObject
    {
        protected CouncilGroup Data { get; }
        public string Name { get => Data.Name; }
        public string Id { get => Data.Id.ToString(); }
        public CouncilGroupViewModel(CouncilGroup data)
        {
            Data = data;
        }
    }
}
