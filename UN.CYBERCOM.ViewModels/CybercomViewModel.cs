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
using UN.CYBERCOM.Contracts.CYBERCOM.ContractDefinition;

namespace UN.CYBERCOM.ViewModels
{
    public class CybercomViewModel : ReactiveObject
    {
        public Interaction<string, bool> Alert { get; } = new Interaction<string, bool>();
        public Interaction<string, string> SignMembershipRequest { get; } = new Interaction<string, string>();
        public ReactiveCommand<Unit, Unit> Load { get; }
        public ReactiveCommand<Unit, Unit> Deploy { get; }
        public ReactiveCommand<Unit, Unit> MembershipRequest { get; }
        protected Web3 W3 { get; }
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
        public CybercomViewModel(Web3 w3, IConfiguration config)
        {
            W3 = w3;
            IsDeployed = bool.Parse(config["CYBERCOM:IsDeployed"] ?? throw new InvalidDataException());
            Load = ReactiveCommand.CreateFromTask(DoLoad);
            Deploy = ReactiveCommand.CreateFromTask(DoDeploy);
            MembershipRequest = ReactiveCommand.CreateFromTask(DoMembershipRequest);
            ContractAddress = config["CYBERCOM:Address"];
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
                            Council = SelectedCouncil.RoleBytes,
                            NewNation = new Nation()
                            {
                                Name = NewNationName,
                                Id = NewMemberAddress
                            },
                            GroupId = BigInteger.Parse(SelectedCouncil.SelectedGroupId),
                            Duration = BigInteger.Zero
                        },
                        AmountToSend = 0,
                        Gas = 5000000,
                        
                    };
                    var es = tran.GetEncodingService();
                    var data = Convert.ToHexString(tran.GetCallData()).ToLower();
                    var signedData = await SignMembershipRequest.Handle(data).GetAwaiter();
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
                    var rcpt = await CybercomService.DeployContractAndWaitForReceiptAsync(W3, new CybercomDeployment()
                    {
                        SubscriptionId = SubscriptionId
                    });
                    ContractAddress = rcpt.ContractAddress;
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
                }
                else
                {
                    Councils.Clear();
                    Nations.Clear();
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
    public class CouncilViewModel : ReactiveObject
    {
        protected Council Data { get; }
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
