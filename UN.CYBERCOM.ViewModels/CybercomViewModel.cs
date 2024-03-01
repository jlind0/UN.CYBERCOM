using DynamicData;
using DynamicData.Binding;
using Microsoft.Extensions.Configuration;
using Nethereum.Contracts;
using Nethereum.Web3;
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
using UN.CYBERCOM.Contracts.CybercomDAO;
using UN.CYBERCOM.Contracts.CybercomDAO.ContractDefinition;
using UN.CYBERCOM.Contracts.Document;
using Nethereum.Hex.HexConvertors.Extensions;
using UN.CYBERCOM.Contracts.Proposal;
using System.Security.Cryptography;
using UN.CYBERCOM.Contracts.CouncilManager;
using UN.CYBERCOM.Contracts.CouncilManager.ContractDefinition;
using UN.CYBERCOM.Contracts.ProposalStorageManager;
using UN.CYBERCOM.Contracts.Voting;
using UN.CYBERCOM.Contracts.MembershipRemovalManager;
using System.Windows.Input;
using Org.BouncyCastle.Asn1.Crmf;

namespace UN.CYBERCOM.ViewModels
{
    public struct TransactionData
    {
        public string ContractAddress { get; set; }
        public string TXData { get; set; }
    }
    public class AddMembershipViewModel : ReactiveObject
    {
        private bool isOpen;
        public bool IsOpen
        {
            get => isOpen;
            set => this.RaiseAndSetIfChanged(ref isOpen, value);
        }
        public ObservableCollection<CouncilViewModel> Councils
        {
            get => CybercomVM.Councils;
        }
        protected CybercomViewModel CybercomVM { get; }
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
            get => CybercomVM.Councils.SingleOrDefault(c => c.Role == selectedCouncilRole);
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
        public ReactiveCommand<Unit, Unit> MembershipRequest { get; }
        public AddMembershipViewModel(CybercomViewModel cybercomVM)
        {
            CybercomVM = cybercomVM;
            MembershipRequest = ReactiveCommand.CreateFromTask(DoMembershipRequest);
            Open = ReactiveCommand.Create(DoOpen);
        }
        public ICommand Open { get; }
        protected void DoOpen()
        {
            SelectedCouncilRole = null;
            NewNationName = null;
            NewMemberAddress = null;
            IsOpen = true;
        }
        protected async Task DoMembershipRequest()
        {
            try
            {
                if (CybercomVM.IsDeployed && CybercomVM.CyberService != null &&
                    !string.IsNullOrWhiteSpace(SelectedCouncilRole) &&
                    !string.IsNullOrWhiteSpace(SelectedCouncil?.SelectedGroupId) &&
                    !string.IsNullOrWhiteSpace(NewMemberAddress) &&
                    !string.IsNullOrWhiteSpace(NewNationName))
                {
                    CybercomVM.IsLoading = true;
                    var tran = new SubmitMembershipProposalFunction()
                    {
                        FromAddress = CybercomVM.AccountNumber,
                        Request = new MembershipProposalRequest()
                        {
                            Member = NewMemberAddress,
                            NewNation = new Contracts.CybercomDAO.ContractDefinition.Nation()
                            {
                                Name = NewNationName,
                                Id = NewMemberAddress
                            },
                            GroupId = BigInteger.Parse(SelectedCouncil.SelectedGroupId),
                            Duration = BigInteger.Zero,
                            Owner = CybercomViewModel.ZERO_ADDRESS
                        },
                        AmountToSend = 0,
                        Gas = 15000000,

                    };
                    var data = Convert.ToHexString(tran.GetCallData()).ToLower();
                    var signedData = await CybercomVM.SignatureRequest.Handle(new TransactionData()
                    {
                        ContractAddress = CybercomVM.ContractAddress ?? throw new InvalidDataException(),
                        TXData = data

                    }).GetAwaiter();
                    var str = await CybercomVM.W3.Eth.Transactions.SendRawTransaction.SendRequestAsync(signedData);
                    IsOpen = false;
                }
            }
            catch (Exception ex)
            {
                await CybercomVM.Alert.Handle(ex.Message).GetAwaiter();
            }
            finally
            {
                CybercomVM.IsLoading = false;
            }
        }
    }
    public class CybercomViewModel : ReactiveObject, IDisposable
    {
        public Interaction<string, bool> Alert { get; } = new Interaction<string, bool>();
        public Interaction<TransactionData, string> SignatureRequest { get; } = new Interaction<TransactionData, string>();
        public Interaction<string, string> SignData { get; } = new Interaction<string, string>();
        public ReactiveCommand<Unit, Unit> Load { get; }
        public ReactiveCommand<Unit, Unit> Deploy { get; }
        public AddMembershipViewModel AddMembershipVM { get; }
        internal Web3 W3 { get; }
        private bool disposedValue;
        private string? accountNumber;
        public string? AccountNumber
        {
            get => accountNumber;
            set => this.RaiseAndSetIfChanged(ref accountNumber, value);
        }
        private CybercomDAOService? cybercomService;
        public CybercomDAOService? CyberService
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
        private string? votingAddress;
        public string? VotingAddress
        {
            get => votingAddress;
            set => this.RaiseAndSetIfChanged(ref votingAddress, value);
        }
        private string? membershipAddress;
        public string? MembershipAddress
        {
            get => membershipAddress;
            set => this.RaiseAndSetIfChanged(ref membershipAddress, value);
        }
        private string? membershipRemovalAddress;
        public string? MembershipRemovalAddress
        {
            get => membershipRemovalAddress;
            set => this.RaiseAndSetIfChanged(ref membershipRemovalAddress, value);
        }
        private string? councilManagerAddress;
        public string? CouncilManagerAddress
        {
            get => councilManagerAddress;
            set => this.RaiseAndSetIfChanged(ref councilManagerAddress, value);
        }
        private string? proposalStorageAddress;
        public string? ProposalStorageAddress
        {
            get => proposalStorageAddress;
            set => this.RaiseAndSetIfChanged(ref proposalStorageAddress, value);
        }
        

        protected ulong SubscriptionId { get; }
        private readonly CompositeDisposable disposable = new CompositeDisposable();
        public ObservableCollection<CouncilViewModel> Councils { get; } = new ObservableCollection<CouncilViewModel>();
        public ObservableCollection<NationViewModel> Nations { get; } = new ObservableCollection<NationViewModel>();
        public ObservableCollection<MembershipProposalViewModel> EnteredMembershipProposals { get; } = new ObservableCollection<MembershipProposalViewModel>();
        public ObservableCollection<MembershipProposalViewModel> PendingMembershipProposals { get; } = new ObservableCollection<MembershipProposalViewModel>();
        public ObservableCollection<MembershipProposalViewModel> ReadyMembershipProposals { get; } = new ObservableCollection<MembershipProposalViewModel>();
        public ObservableCollection<MembershipProposalViewModel> ApprovedMembershipProposals { get; } = new ObservableCollection<MembershipProposalViewModel>();
        public ObservableCollection<MembershipProposalViewModel> RejectedMembershipProposals { get; } = new ObservableCollection<MembershipProposalViewModel>();
        public ObservableCollection<MembershipRemovalProposalViewModel> EnteredMembershipRemovalProposals { get; } = new ObservableCollection<MembershipRemovalProposalViewModel>();
        public ObservableCollection<MembershipRemovalProposalViewModel> PendingMembershipRemovalProposals { get; } = new ObservableCollection<MembershipRemovalProposalViewModel>();
        public ObservableCollection<MembershipRemovalProposalViewModel> ReadyMembershipRemovalProposals { get; } = new ObservableCollection<MembershipRemovalProposalViewModel>();
        public ObservableCollection<MembershipRemovalProposalViewModel> ApprovedMembershipRemovalProposals { get; } = new ObservableCollection<MembershipRemovalProposalViewModel>();
        public ObservableCollection<MembershipRemovalProposalViewModel> RejectedMembershipRemovalProposals { get; } = new ObservableCollection<MembershipRemovalProposalViewModel>();
        protected string? OldContractAddress { get; }
        public CybercomViewModel(Web3 w3, IConfiguration config)
        {
            W3 = w3;
            AddMembershipVM = new AddMembershipViewModel(this);
            IsDeployed = bool.Parse(config["CYBERCOM:IsDeployed"] ?? throw new InvalidDataException());
            Load = ReactiveCommand.CreateFromTask(DoLoad);
            Deploy = ReactiveCommand.CreateFromTask(DoDeploy);
            
            ContractAddress = config["CYBERCOM:Address"];
            OldContractAddress = config["CYBERCOM:OldAddress"];
            SubscriptionId = ulong.Parse(config["VRF:SubscriptionId"] ?? throw new InvalidDataException());
            this.WhenPropertyChanged(p => p.AccountNumber).Subscribe(p =>
            {
                if (!IsDeployed)
                    return;
                if (p.Value != null)
                {
                    CyberService = new CybercomDAOService(W3, ContractAddress ?? throw new InvalidDataException());
                    
                }
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
            }).DisposeWith(disposable);
        }
        public const string ZERO_ADDRESS = "0x0000000000000000000000000000000000000000";
        
        protected async Task DoDeploy()
        {
            try
            {
                IsLoading = true;
                if (!IsDeployed)
                {
                    var rcpt = await CybercomDAOService.DeployContractAndWaitForReceiptAsync(W3, new CybercomDAODeployment()
                    {
                        SubscriptionId = SubscriptionId
                    });
                    ContractAddress = rcpt.ContractAddress;
                    var rcpt2 = await CouncilManagerService.DeployContractAndWaitForReceiptAsync(W3, new CouncilManagerDeployment()
                    {
                        DaoAddress = ContractAddress
                    });
                    CouncilManagerAddress = rcpt2.ContractAddress;
                    var rcpt3 = await ProposalStorageManagerService.DeployContractAndWaitForReceiptAsync(W3, new Contracts.ProposalStorageManager.ContractDefinition.ProposalStorageManagerDeployment()
                    {
                        DaoAddress = ContractAddress
                    });
                    ProposalStorageAddress = rcpt3.ContractAddress;
                    var rcpt4 = await VotingService.DeployContractAndWaitForReceiptAsync(W3, new Contracts.Voting.ContractDefinition.VotingDeployment()
                    {
                        SubscriptionId = SubscriptionId,
                        DaoAddress = ContractAddress,
                        CouncilManagerAddress = CouncilManagerAddress
                    });
                    VotingAddress = rcpt4.ContractAddress;
                    var rcpt5 = await MembershipRemovalManagerService.DeployContractAndWaitForReceiptAsync(W3, new Contracts.MembershipRemovalManager.ContractDefinition.MembershipRemovalManagerDeployment()
                    {
                        VotingAddress = VotingAddress,
                        CouncilManagementAddress = CouncilManagerAddress,
                        ProposalStorageAddress = ProposalStorageAddress,
                        DaoAddress = ContractAddress
                    });
                    MembershipRemovalAddress = rcpt5.ContractAddress;
                    var rcpt6 = await Contracts.MembershipManager.MembershipManagerService.DeployContractAndWaitForReceiptAsync(W3, new Contracts.MembershipManager.ContractDefinition.MembershipManagerDeployment()
                    {
                        VotingAddress = VotingAddress,
                        CouncilManagementAddress = CouncilManagerAddress,
                        ProposalStorageAddress = ProposalStorageAddress,
                        DaoAddress = ContractAddress
                    });
                    MembershipAddress = rcpt6.ContractAddress;
                    var service = new CybercomDAOService(W3, ContractAddress);
                    await service.InitializeRequestAndWaitForReceiptAsync(new ContractAddresses()
                    {
                        DaoAddress = ContractAddress,
                        VotingAddress = VotingAddress,
                        CouncilManagementAddress = CouncilManagerAddress,
                        ProposalStorageAddress = ProposalStorageAddress,
                        MembershipManagerAddress = MembershipAddress,
                        MembershipRemovalAddress = MembershipRemovalAddress
                    });
                    await service.CloseInitializationRequestAndWaitForReceiptAsync();
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
                if (IsLoading)
                    return;
                IsLoading = true;
                if (!IsDeployed)
                    return;
                if(CyberService != null)
                {
                    var contracts = await CyberService.ContractsQueryAsync();
                    VotingAddress = contracts.VotingAddress;
                    CouncilManagerAddress = contracts.CouncilManagementAddress;
                    MembershipAddress = contracts.MembershipManagerAddress;
                    MembershipRemovalAddress = contracts.MembershipRemovalAddress;
                    Councils.Clear();
                    var councilDto = await new CouncilManagerService(W3, CouncilManagerAddress).GetCouncilsQueryAsync(new Contracts.CouncilManager.ContractDefinition.GetCouncilsFunction()
                    {
                        FromAddress = AccountNumber
                    });
                    Nations.Clear();
                    Nations.AddRange(councilDto.ReturnValue1.SelectMany(c => c.Groups.SelectMany(g => g.Members.Select(m => new NationViewModel(m, c, g, this)))));
                    Councils.AddRange(councilDto.ReturnValue1.Select(g => new CouncilViewModel(g)).ToArray());
                    var dicCouncils = Councils.ToDictionary(g => g.Role, g => g.Data);
                    EnteredMembershipProposals.Clear();
                    var memberManager = new UN.CYBERCOM.Contracts.MembershipManager.MembershipManagerService(W3, MembershipAddress);

                    var enteredDto = await memberManager.GetMembershipRequestsQueryAsync(new Contracts.MembershipManager.ContractDefinition.GetMembershipRequestsFunction()
                    {
                        Status = (byte)ProposalViewModel.ApprovalStatus.Entered,
                        FromAddress = AccountNumber
                    });
                    EnteredMembershipProposals.AddRange(enteredDto.ReturnValue1.Select(g => new MembershipProposalViewModel(this, g, dicCouncils[Convert.ToBase64String(g.Council)])));
                    foreach (var pmp in EnteredMembershipProposals)
                        await pmp.Load.Execute().GetAwaiter();
                    PendingMembershipProposals.Clear();
                    var pendingDto = await memberManager.GetMembershipRequestsQueryAsync(new Contracts.MembershipManager.ContractDefinition.GetMembershipRequestsFunction()
                    {
                        Status = (byte)ProposalViewModel.ApprovalStatus.Pending,
                        FromAddress = AccountNumber
                    });

                    PendingMembershipProposals.AddRange(pendingDto.ReturnValue1.Select(g => new MembershipProposalViewModel(this, g, dicCouncils[Convert.ToBase64String(g.Council)])));
                    foreach (var pmp in PendingMembershipProposals)
                        await pmp.Load.Execute().GetAwaiter();
                    var readyDto = await memberManager.GetMembershipRequestsQueryAsync(new Contracts.MembershipManager.ContractDefinition.GetMembershipRequestsFunction()
                    {
                        Status = (byte)ProposalViewModel.ApprovalStatus.Ready,
                        FromAddress = AccountNumber
                    });
                    ReadyMembershipProposals.Clear();
                    ReadyMembershipProposals.AddRange(readyDto.ReturnValue1.Select(g => new MembershipProposalViewModel(this, g, dicCouncils[Convert.ToBase64String(g.Council)])));
                    foreach (var pmp in ReadyMembershipProposals)
                        await pmp.Load.Execute().GetAwaiter();
                    var rejectedDto = await memberManager.GetMembershipRequestsQueryAsync(new Contracts.MembershipManager.ContractDefinition.GetMembershipRequestsFunction()
                    {
                        Status = (byte)ProposalViewModel.ApprovalStatus.Rejected,
                        FromAddress = AccountNumber
                    });
                    RejectedMembershipProposals.Clear();
                    RejectedMembershipProposals.AddRange(rejectedDto.ReturnValue1.Select(g => new MembershipProposalViewModel(this, g, dicCouncils[Convert.ToBase64String(g.Council)])));
                    foreach (var pmp in RejectedMembershipProposals)
                        await pmp.Load.Execute().GetAwaiter();
                    var approvedDto = await memberManager.GetMembershipRequestsQueryAsync(new Contracts.MembershipManager.ContractDefinition.GetMembershipRequestsFunction()
                    {
                        Status = (byte)ProposalViewModel.ApprovalStatus.Approved,
                        FromAddress = AccountNumber
                    });
                    ApprovedMembershipProposals.Clear();
                    ApprovedMembershipProposals.AddRange(approvedDto.ReturnValue1.Select(g => new MembershipProposalViewModel(this, g, dicCouncils[Convert.ToBase64String(g.Council)])));
                    foreach (var pmp in ApprovedMembershipProposals)
                        await pmp.Load.Execute().GetAwaiter();
                    EnteredMembershipRemovalProposals.Clear();
                    var memberRemovalManager = new MembershipRemovalManagerService(W3, MembershipRemovalAddress);
                    var memberRemovalEnterDto = await memberRemovalManager.GetMembershipRemovalRequestsQueryAsync(new Contracts.MembershipRemovalManager.ContractDefinition.GetMembershipRemovalRequestsFunction()
                    {
                        Status = (byte)ProposalViewModel.ApprovalStatus.Entered,
                        FromAddress = AccountNumber
                    });
                    EnteredMembershipRemovalProposals.Clear();
                    EnteredMembershipRemovalProposals.AddRange(memberRemovalEnterDto.ReturnValue1.Select(g => new MembershipRemovalProposalViewModel(this, g)));
                    foreach (var pmp in EnteredMembershipRemovalProposals)
                        await pmp.Load.Execute().GetAwaiter();
                    var memberRemovalPendingDto = await memberRemovalManager.GetMembershipRemovalRequestsQueryAsync(new Contracts.MembershipRemovalManager.ContractDefinition.GetMembershipRemovalRequestsFunction()
                    {
                        Status = (byte)ProposalViewModel.ApprovalStatus.Pending,
                        FromAddress = AccountNumber
                    });
                    PendingMembershipRemovalProposals.Clear();
                    PendingMembershipRemovalProposals.AddRange(memberRemovalPendingDto.ReturnValue1.Select(g => new MembershipRemovalProposalViewModel(this, g)));
                    foreach (var pmp in PendingMembershipRemovalProposals)
                        await pmp.Load.Execute().GetAwaiter();
                    var memberRemovalReadyDto = await memberRemovalManager.GetMembershipRemovalRequestsQueryAsync(new Contracts.MembershipRemovalManager.ContractDefinition.GetMembershipRemovalRequestsFunction()
                    {
                        Status = (byte)ProposalViewModel.ApprovalStatus.Ready,
                        FromAddress = AccountNumber
                    });
                    ReadyMembershipRemovalProposals.Clear();
                    ReadyMembershipRemovalProposals.AddRange(memberRemovalReadyDto.ReturnValue1.Select(g => new MembershipRemovalProposalViewModel(this, g)));
                    foreach (var pmp in ReadyMembershipRemovalProposals)
                        await pmp.Load.Execute().GetAwaiter();
                    var memberRemovalRejectedDto = await memberRemovalManager.GetMembershipRemovalRequestsQueryAsync(new Contracts.MembershipRemovalManager.ContractDefinition.GetMembershipRemovalRequestsFunction()
                    {
                        Status = (byte)ProposalViewModel.ApprovalStatus.Ready,
                        FromAddress = AccountNumber
                    });
                    RejectedMembershipRemovalProposals.Clear();
                    RejectedMembershipRemovalProposals.AddRange(memberRemovalRejectedDto.ReturnValue1.Select(g => new MembershipRemovalProposalViewModel(this, g)));
                    foreach (var pmp in RejectedMembershipRemovalProposals)
                        await pmp.Load.Execute().GetAwaiter();
                    var memberRemovalAcceptedDto = await memberRemovalManager.GetMembershipRemovalRequestsQueryAsync(new Contracts.MembershipRemovalManager.ContractDefinition.GetMembershipRemovalRequestsFunction()
                    {
                        Status = (byte)ProposalViewModel.ApprovalStatus.Approved,
                        FromAddress = AccountNumber
                    });
                    ApprovedMembershipRemovalProposals.Clear();
                    ApprovedMembershipRemovalProposals.AddRange(memberRemovalAcceptedDto.ReturnValue1.Select(g => new MembershipRemovalProposalViewModel(this, g)));
                    foreach (var pmp in ApprovedMembershipRemovalProposals)
                        await pmp.Load.Execute().GetAwaiter();
                }
                else
                {
                    Councils.Clear();
                    Nations.Clear();
                    EnteredMembershipProposals.Clear();
                    EnteredMembershipRemovalProposals.Clear();
                    PendingMembershipProposals.Clear();
                    PendingMembershipRemovalProposals.Clear();
                    ReadyMembershipProposals.Clear();
                    ReadyMembershipRemovalProposals.Clear();
                    RejectedMembershipProposals.Clear();
                    RejectedMembershipRemovalProposals.Clear();
                    ApprovedMembershipRemovalProposals.Clear();
                    ApprovedMembershipProposals.Clear();
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

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }
                disposable.Dispose();
                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~CybercomViewModel()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
    public class VoteViewModel : ReactiveObject
    {
        public ProposalViewModel Proposal { get; }
        
        public UN.CYBERCOM.Contracts.Proposal.ContractDefinition.Vote Data { get; }
        public bool CastedVote { get => Data.VoteCasted; }
        public Contracts.CouncilManager.ContractDefinition.Nation Nation { get; }
        protected CybercomViewModel Root { get; }
        public VoteViewModel(CybercomViewModel root, ProposalViewModel proposal, UN.CYBERCOM.Contracts.Proposal.ContractDefinition.Vote vote)
        {
            Root = root;
            Data = vote;
            Proposal = proposal;
            Nation = root.Nations.Single(n => n.Id == vote.Member).Data;
        }
    }
    public class AddDocumentViewModel : DocumentViewModel
    {
        public ReactiveCommand<Unit, Unit> HashUrl { get; }
        public ReactiveCommand<Unit, Unit> Add { get; }
        public ReactiveCommand<Unit, Unit> SignHash { get; }
        public bool CanHashUrl
        {
            get => Url != null;
        }
        private bool isOpen = false;
        public bool IsOpen
        {
            get => isOpen;
            set => this.RaiseAndSetIfChanged(ref isOpen, value);
        }
        public ReactiveCommand<Unit, Unit> Open { get; }
        public AddDocumentViewModel(CybercomViewModel root, ProposalViewModel proposalVM) : base(root, proposalVM) 
        {
            this.WhenPropertyChanged(p => p.Url).Subscribe(p => {
                Signature = null;
                DocumentHash = null;
                this.RaisePropertyChanged(nameof(CanHashUrl));
                this.RaisePropertyChanged(nameof(CanSignHash));
                this.RaisePropertyChanged(nameof(CanAdd));
                }).DisposeWith(disposable);
            this.WhenPropertyChanged(p => p.DocumentHash).Subscribe(p =>
            {
                Signature = null;
                this.RaisePropertyChanged(nameof(CanSignHash));
                this.RaisePropertyChanged(nameof(CanAdd));
            }).DisposeWith(disposable);
            this.WhenPropertyChanged(p => p.Signature).Subscribe(p =>
            {
                this.RaisePropertyChanged(nameof(CanSignHash));
                this.RaisePropertyChanged(nameof(CanAdd));
            }).DisposeWith(disposable);
            HashUrl = ReactiveCommand.CreateFromTask(DoHashUrl);
            Add = ReactiveCommand.CreateFromTask(DoAdd);
            SignHash = ReactiveCommand.CreateFromTask(DoSignHash);
            Open = ReactiveCommand.Create(DoOpen);
        }
        protected void DoOpen()
        {
            IsOpen = true;
        }
        protected async Task DoSignHash()
        {
            if (!CanSignHash)
            {
                await Root.Alert.Handle("Document Hash not ready to sign.").GetAwaiter();
                return;
            }
            try
            {
                Signature = await Root.SignData.Handle(DocumentHash ?? throw new InvalidDataException()).GetAwaiter();
            }
            catch (Exception ex)
            {
                await Root.Alert.Handle(ex.Message).GetAwaiter();
            }
        }
        public bool CanSignHash
        {
            get => !string.IsNullOrWhiteSpace(DocumentHash);
        }
        public bool CanAdd
        {
            get => !string.IsNullOrWhiteSpace(Signature) && !string.IsNullOrWhiteSpace(DocumentHash) && !string.IsNullOrWhiteSpace(Title) && !string.IsNullOrWhiteSpace(Url);
        }
        protected async Task DoAdd()
        {
            if(!ProposalVM.OwnsProposal)
            {
                await Root.Alert.Handle("Only the Proposal owner can add a document").GetAwaiter();
                return;
            }
            if (!CanAdd)
            {
                await Root.Alert.Handle("Please complete form");
                return;
            }
            try
            {
                var tx = new Contracts.Proposal.ContractDefinition.AddDocumentFunction()
                {
                    Signature = Signature.HexToByteArray(),
                    DocHash = DocumentHash.HexToByteArray(),
                    Title = Title ?? throw new InvalidDataException(),
                    Signer = Root.AccountNumber ?? throw new InvalidDataException(),
                    FromAddress = Root.AccountNumber,
                    Url = Url ?? throw new InvalidDataException(),
                    AmountToSend = 0,
                    Gas = 15000000
                };
                var data = Convert.ToHexString(tx.GetCallData()).ToLower();
                var signedData = await Root.SignatureRequest.Handle(new TransactionData()
                {
                    ContractAddress = ProposalVM.ProposalAddress ?? throw new InvalidDataException(),
                    TXData = data

                }).GetAwaiter();
                var str = await Root.W3.Eth.Transactions.SendRawTransaction.SendRequestAsync(signedData);
            }
            catch (Exception ex) 
            {
                await Root.Alert.Handle(ex.Message).GetAwaiter();
            }
        }
        protected async Task DoHashUrl()
        {
            if (!CanHashUrl)
            {
                await Root.Alert.Handle("Url not ready for hash").GetAwaiter();
                return;
            }
            try
            {
                DocumentHash = await ComputeHash();
            }
            catch(Exception ex)
            {
                await Root.Alert.Handle(ex.Message).GetAwaiter();
            }
        }
    }
    public class DocumentViewModel : ReactiveObject, IDisposable
    {
        protected async Task<string> ComputeHash()
        {
            using (var client = new HttpClient())
            {
                var data = await client.GetByteArrayAsync(Url);
                if (data != null)
                {
                    using (var sha256 = SHA256.Create())
                    {
                        byte[] hashBytes = sha256.ComputeHash(data);
                        return hashBytes.ToHex(true);
                    }
                }
                else
                    throw new InvalidDataException();
            }
        }
        private string? url;
        public string? Url
        {
            get => url;
            set => this.RaiseAndSetIfChanged(ref url, value);
        }
        private string? docHash;
        public string? DocumentHash
        {
            get => docHash;
            set => this.RaiseAndSetIfChanged(ref docHash, value);
        }
        private string? signature;
        public string? Signature
        {
            get => signature;
            set => this.RaiseAndSetIfChanged(ref signature, value);
        }
        private string? address;
        private bool disposedValue;

        public string? Address
        {
            get => address;
            set => this.RaiseAndSetIfChanged(ref address, value);
        }
        private string? title;
        public string? Title
        {
            get => title;
            set => this.RaiseAndSetIfChanged(ref title, value);
        }
        private string? signer;
        public string? Signer
        {
            get => signer;
            set => this.RaiseAndSetIfChanged(ref signer, value);
        }
        private bool? documentHashIsValid;
        public bool? DocumentHashIsValid
        {
            get => documentHashIsValid;
            set => this.RaiseAndSetIfChanged(ref documentHashIsValid, value);
        }
        protected CybercomViewModel Root { get; }
        protected ProposalViewModel ProposalVM { get; }
        protected readonly CompositeDisposable disposable = new CompositeDisposable();
        public ReactiveCommand<Unit, Unit> VerifyDocumentHash { get; }
        public DocumentViewModel(CybercomViewModel root, ProposalViewModel proposalVM, string? _address = null)
        {
            Root = root;
            ProposalVM = proposalVM;
            this.WhenPropertyChanged(p => p.Address).Subscribe(async p =>
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(p.Value))
                    {
                        DocumentService ds = new DocumentService(Root.W3, p.Value);
                        Title = await ds.TitleQueryAsync(new Contracts.Document.ContractDefinition.TitleFunction()
                        {
                            FromAddress = Root.AccountNumber
                        });
                        Signature = (await ds.SignatureQueryAsync(new Contracts.Document.ContractDefinition.SignatureFunction()
                        {
                            FromAddress = Root.AccountNumber
                        })).ToHex(true);
                        DocumentHash = (await ds.DochashQueryAsync(new Contracts.Document.ContractDefinition.DochashFunction()
                        {
                            FromAddress = Root.AccountNumber
                        })).ToHex(true);
                        Url = await ds.UrlQueryAsync(new Contracts.Document.ContractDefinition.UrlFunction()
                        {
                            FromAddress = Root.AccountNumber
                        });
                        Signer = await ds.SignerQueryAsync(new Contracts.Document.ContractDefinition.SignerFunction()
                        {
                            FromAddress = Root.AccountNumber
                        });
                    }
                    else
                    {
                        Title = null;
                        Signature = null;
                        DocumentHash = null;
                        Url = null;
                        Signer = null;
                    }
                }
                catch(Exception ex)
                {
                    await Root.Alert.Handle(ex.Message).GetAwaiter();
                }
            }).DisposeWith(disposable);
            Address = _address;
            VerifyDocumentHash = ReactiveCommand.CreateFromTask(DoVerifyDocumentHash);
        }
        protected async Task DoVerifyDocumentHash()
        {
            if(string.IsNullOrWhiteSpace(DocumentHash))
            {
                await Root.Alert.Handle("Hash ios not set").GetAwaiter();
                return;
            }
            try
            {
                DocumentHashIsValid = null;
                var hash = await ComputeHash();
                DocumentHashIsValid = DocumentHash == hash;
            }
            catch (Exception ex)
            {
                await Root.Alert.Handle(ex.Message).GetAwaiter();
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }
                disposable.Dispose();
                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~DocumentViewModel()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
    public class NationViewModel : ReactiveObject
    {
        internal Contracts.CouncilManager.ContractDefinition.Nation Data { get; }
        protected CybercomViewModel Root { get; }
        public string Id
        {
            get => Data.Id;
        }
        public string Name
        {
            get => Data.Name;
        }
        public Contracts.CouncilManager.ContractDefinition.Council Council { get; }
        public Contracts.CouncilManager.ContractDefinition.CouncilGroup CouncilGroup { get; }
        public ReactiveCommand<Unit, Unit> Remove { get; }
        public NationViewModel(Contracts.CouncilManager.ContractDefinition.Nation data, Contracts.CouncilManager.ContractDefinition.Council council, Contracts.CouncilManager.ContractDefinition.CouncilGroup councilGroup, CybercomViewModel root)
        {
            Council = council;
            CouncilGroup = councilGroup;
            Data = data;
            Root = root;
            Remove = ReactiveCommand.CreateFromTask(DoRemove);
        }
        protected async Task DoRemove()
        {
            try
            {
                var tx = new SubmitMembershipRemovalProposalFunction()
                {
                    FromAddress = Root.AccountNumber,
                    AmountToSend = 0,
                    Gas = 15000000,
                    Request = new MembershipRemovalRequest()
                    {
                        Duration = BigInteger.Zero,
                        NationToRemove = Id,
                        Owner = CybercomViewModel.ZERO_ADDRESS
                    }
                    
                };
                var data = Convert.ToHexString(tx.GetCallData()).ToLower();
                var signedData = await Root.SignatureRequest.Handle(new TransactionData()
                {
                    ContractAddress = Root.ContractAddress ?? throw new InvalidDataException(),
                    TXData = data

                }).GetAwaiter();
                var str = await Root.W3.Eth.Transactions.SendRawTransaction.SendRequestAsync(signedData);
            }
            catch(Exception ex)
            {
                await Root.Alert.Handle(ex.Message).GetAwaiter();
            }
        }
    }
    public abstract class ProposalViewModel : ReactiveObject
    {
        public ObservableCollection<VoteViewModel> Votes { get; } = new ObservableCollection<VoteViewModel>();
        public ObservableCollection<DocumentViewModel> Documents { get; } = new ObservableCollection<DocumentViewModel>();
        public AddDocumentViewModel AddDocumentVM { get; }
        public enum ApprovalStatus : byte
        {
            Entered,
            Pending,
            Ready,
            Approved,
            Rejected
        }
        protected Interaction<TransactionData, string> SignatureRequest { get; }
        protected Interaction<string, bool> Alert { get; }
        public abstract string Id { get; }
        public abstract DateTime Duration { get; }
        public ReactiveCommand<Unit, Unit> StartVoting { get; }
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
            get => Status == ApprovalStatus.Pending && Duration < DateTime.UtcNow && !IsProcessing;
        }
        public bool CanVote
        {
            get => !CanTally && !CanCompleteTally && !IsCompleted;
        }
        public bool CanCompleteTally
        {
            get => Status == ApprovalStatus.Ready;
        }
        public bool IsCompleted
        {
            get => Status == ApprovalStatus.Rejected || Status == ApprovalStatus.Approved;
        }
        protected CybercomViewModel Parent { get; }
        public bool OwnsProposal
        {
            get => Parent.AccountNumber == Owner;
        }
        public bool CanStartVoting
        {
            get => OwnsProposal && Status == ApprovalStatus.Entered;
        }
        public abstract ApprovalStatus Status { get; }
        public abstract bool IsProcessing { get; }
        public abstract string Owner { get; }
        public abstract string ProposalAddress { get; }
        public ProposalViewModel(CybercomViewModel vm)
        {
            AddDocumentVM = new AddDocumentViewModel(vm, this);
            Parent = vm;
            SignatureRequest = vm.SignatureRequest;
            Alert = vm.Alert;
            Tally = ReactiveCommand.CreateFromTask(DoTally);
            CompleteTally = ReactiveCommand.CreateFromTask(DoCompleteTally);
            Vote = ReactiveCommand.CreateFromTask(DoVote);
            Load = ReactiveCommand.CreateFromTask(DoLoad);
            StartVoting = ReactiveCommand.CreateFromTask(DoStartVoting);
        }
        protected async Task DoStartVoting()
        {
            try
            {
                if (CanStartVoting)
                {
                    StartVotingFunction svf = new StartVotingFunction()
                    {
                        ProposalId = BigInteger.Parse(Id),
                        FromAddress = Parent.AccountNumber,
                        AmountToSend = 0,
                        Gas = 15000000
                    };
                    var data = Convert.ToHexString(svf.GetCallData()).ToLower();
                    var signedData = await SignatureRequest.Handle(new TransactionData()
                    {
                        ContractAddress = Parent.ContractAddress ?? throw new InvalidDataException(),
                        TXData = data

                    }).GetAwaiter();
                    var str = await Parent.W3.Eth.Transactions.SendRawTransaction.SendRequestAsync(signedData);
                }
            }
            catch (Exception ex)
            {
                await Alert.Handle(ex.Message).GetAwaiter();
            }
        }
        protected virtual async Task DoLoad()
        {
            try
            {
                if (Parent.CyberService != null)
                {
                    var ms = new ProposalService(Parent.W3, ProposalAddress);
                    var votesDto = await ms.GetVotesQueryAsync(new Contracts.Proposal.ContractDefinition.GetVotesFunction()
                    {
                        FromAddress = Parent.AccountNumber,

                    });
                    Votes.Clear();
                    Votes.AddRange(votesDto.ReturnValue1.Select(
                        v => new VoteViewModel(Parent, this, v)));
                    foreach (var doc in Documents)
                        doc.Dispose();
                    Documents.Clear();
                    
                    var docs = await ms.GetDocumentsQueryAsync(new Contracts.Proposal.ContractDefinition.GetDocumentsFunction()
                    {
                        FromAddress = Parent.AccountNumber
                    });
                    Documents.AddRange(docs.ReturnValue1.Select(g => new DocumentViewModel(Parent, this, g.DocAddress)));
                }
            }
            catch (Exception ex)
            {
                await Alert.Handle(ex.Message).GetAwaiter();
            }
        }
        protected virtual async Task DoVote()
        {
            try
            {
                var tran = new PerformVoteFunction()
                {
                    FromAddress = Parent.AccountNumber,
                    ProposalId = BigInteger.Parse(Id),
                    AmountToSend = 0,
                    Gas = 15000000,
                    VoteCast = CastVote
                };
                var data = Convert.ToHexString(tran.GetCallData()).ToLower();
                var signedData = await SignatureRequest.Handle(new TransactionData()
                {
                    ContractAddress = Parent.ContractAddress ?? throw new InvalidDataException(),
                    TXData = data

                }).GetAwaiter();
                var str = await Parent.W3.Eth.Transactions.SendRawTransaction.SendRequestAsync(signedData);
            }
            catch (Exception ex)
            {
                await Alert.Handle(ex.Message).GetAwaiter();
            }
        }
        protected virtual async Task DoCompleteTally()
        {
            try
            {
                if (CanCompleteTally)
                {
                    var tran = new CompleteVotingFunction()
                    {
                        FromAddress = Parent.AccountNumber,
                        ProposalId = BigInteger.Parse(Id),
                        AmountToSend = 0,
                        Gas = 15000000
                    };
                    var data = Convert.ToHexString(tran.GetCallData()).ToLower();
                    var signedData = await SignatureRequest.Handle(new TransactionData()
                    {
                        ContractAddress = Parent.ContractAddress ?? throw new InvalidDataException(),
                        TXData = data

                    }).GetAwaiter();
                    var str = await Parent.W3.Eth.Transactions.SendRawTransaction.SendRequestAsync(signedData);
                }
            }
            catch (Exception ex)
            {
                await Alert.Handle(ex.Message).GetAwaiter();
            }
        }
        protected virtual async Task DoTally()
        {
            try
            {
                if (CanTally)
                {
                    var tran = new PrepareTallyFunction()
                    {
                        FromAddress = Parent.AccountNumber,
                        ProposalId = BigInteger.Parse(Id),
                        AmountToSend = 0,
                        Gas = 15000000
                    };
                    var data = Convert.ToHexString(tran.GetCallData()).ToLower();
                    var signedData = await SignatureRequest.Handle(new TransactionData()
                    {
                        ContractAddress = Parent.ContractAddress ?? throw new InvalidDataException(),
                        TXData = data

                    }).GetAwaiter();
                    var str = await Parent.W3.Eth.Transactions.SendRawTransaction.SendRequestAsync(signedData);
                }
            }
            catch (Exception ex)
            {
                await Alert.Handle(ex.Message).GetAwaiter();
            }
        }
    }
    public class MembershipRemovalProposalViewModel : ProposalViewModel
    {


        protected Contracts.MembershipRemovalManager.ContractDefinition.MembershipRemovalResponse Data { get; }

        public Contracts.MembershipRemovalManager.ContractDefinition.Nation NationToRemove
        {
            get => Data.NationToRemove;
        }

        public override string Id => Data.Id.ToString();

        public override DateTime Duration => Data.Duration.FromUnixTimestamp();

        public override ApprovalStatus Status => (ApprovalStatus)Data.Status;

        public override bool IsProcessing => Data.IsProcessing;

        public override string Owner => Data.Owner.ToLower();

        public override string ProposalAddress => Data.ProposalAddress;

        public MembershipRemovalProposalViewModel(CybercomViewModel vm, Contracts.MembershipRemovalManager.ContractDefinition.MembershipRemovalResponse data) : base(vm)
        {
            Data = data;

        }

    }
    public class MembershipProposalViewModel : ProposalViewModel
    {
        
        
        protected Contracts.MembershipManager.ContractDefinition.MembershipProposalResponse Data { get; }
        public Contracts.CouncilManager.ContractDefinition.Council Council { get; }
        public Contracts.CouncilManager.ContractDefinition.CouncilGroup CouncilGroup { get; }
        
        public Contracts.MembershipManager.ContractDefinition.Nation NewNation
        {
            get => Data.NewNation;
        }

        public override string Id => Data.Id.ToString();

        public override DateTime Duration => Data.Duration.FromUnixTimestamp();

        public override ApprovalStatus Status => (ApprovalStatus)Data.Status;

        public override bool IsProcessing => Data.IsProcessing;

        public override string Owner => Data.Owner.ToLower();

        public override string ProposalAddress => Data.ProposalAddress;

        public MembershipProposalViewModel(CybercomViewModel vm, Contracts.MembershipManager.ContractDefinition.MembershipProposalResponse data, Contracts.CouncilManager.ContractDefinition.Council council) :base(vm)
        {
            Data = data;
            Council = council;
            CouncilGroup = Council.Groups.Single(g => g.Id == Data.GroupId);
            
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
        public Contracts.CouncilManager.ContractDefinition.Council Data { get; }
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
        public CouncilViewModel(Contracts.CouncilManager.ContractDefinition.Council data) 
        {
            Data = data;
            Groups = new ObservableCollection<CouncilGroupViewModel>(data.Groups.Select(g => new CouncilGroupViewModel(g)).ToArray());
        }
    }
    public class CouncilGroupViewModel : ReactiveObject
    {
        protected Contracts.CouncilManager.ContractDefinition.CouncilGroup Data { get; }
        public string Name { get => Data.Name; }
        public string Id { get => Data.Id.ToString(); }
        public CouncilGroupViewModel(Contracts.CouncilManager.ContractDefinition.CouncilGroup data)
        {
            Data = data;
        }
    }
}
