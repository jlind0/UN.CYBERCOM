using DynamicData.Binding;
using Microsoft.Extensions.Configuration;
using Nethereum.Hex.HexTypes;
using Nethereum.Util;
using Nethereum.Web3;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reactive;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using UN.CYBERCOM.Contracts.DominionDAO;

namespace UN.CYBERCOM.ViewModels
{
    public class IndexViewModel : ReactiveObject, IDisposable
    {
        private BigInteger contributionAmount = new BigInteger(1);
        public BigInteger ContributionAmount
        {
            get => contributionAmount;
            set => this.RaiseAndSetIfChanged(ref contributionAmount, value);
        }
        public ReactiveCommand<Unit, Unit> Contribute { get; }
        public ReactiveCommand<Unit, Unit> Load { get; }
        public ReactiveCommand<Unit, Unit> Deploy { get; }
        protected Web3 W3 { get; }
        private string? accountNumber;
        public string? AccountNumber
        {
            get => accountNumber;
            set => this.RaiseAndSetIfChanged(ref accountNumber, value);
        }
        private DominionDAOService? daoService;
        private bool disposedValue;
        private readonly CompositeDisposable disposable = new CompositeDisposable();
        public DominionDAOService? DaoService
        {
            get => daoService;
            set => this.RaiseAndSetIfChanged(ref daoService, value);
        }
        private bool isStakeHolder;
        public bool IsStakeHolder
        {
            get => isStakeHolder;
            set => this.RaiseAndSetIfChanged(ref isStakeHolder, value);
        }
        private bool isContributor;
        public bool IsContributor
        {
            get => isContributor;
            set => this.RaiseAndSetIfChanged(ref isContributor, value);
        }
        private long totalBalance;
        public long TotalBalance
        {
            get => totalBalance;
            set => this.RaiseAndSetIfChanged(ref totalBalance, value);
        }
        private BigInteger myBalance;
        public BigInteger MyBalance
        {
            get => myBalance;
            set => this.RaiseAndSetIfChanged(ref myBalance, value);
        }
        private bool isDeployed;
        public bool IsDeployed
        {
            get => isDeployed;
            set => this.RaiseAndSetIfChanged(ref isDeployed, value);
        }
        public IndexViewModel(Web3 web3, IConfiguration config)
        {
            W3 = web3;
            IsDeployed = bool.Parse(config["DAO:IsDeployed"] ?? throw new InvalidDataException());
            Load = ReactiveCommand.CreateFromTask(DoLoad);
            Contribute = ReactiveCommand.CreateFromTask(DoContribute);
            Deploy = ReactiveCommand.CreateFromTask(DoDeploy);
            ContractAddress = config["DAO:Address"];
            this.WhenPropertyChanged(p => p.AccountNumber).Subscribe(p =>
            {
                if (!IsDeployed)
                    return;
                if (p.Value != null)
                    DaoService = new DominionDAOService(web3, ContractAddress ?? throw new InvalidDataException());
                else
                    DaoService = null;
            }).DisposeWith(disposable);
            this.WhenPropertyChanged(p => p.DaoService).Subscribe(async p =>
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
        private string? contractAddress;
        public string? ContractAddress
        {
            get => contractAddress;
            set => this.RaiseAndSetIfChanged(ref contractAddress, value);
        }
        protected async Task DoDeploy()
        {
            try
            {
                if (!IsDeployed)
                {
                    var reciept = await DominionDAOService.DeployContractAndWaitForReceiptAsync(W3, new Contracts.DominionDAO.ContractDefinition.DominionDAODeployment()
                    {
                        
                    });
                    ContractAddress = reciept.ContractAddress;
                    IsDeployed = true;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected async Task DoLoad()
        {
            if (!IsDeployed)
                return;
            if(DaoService != null)
            {
                
                IsStakeHolder = await DaoService.IsStakeholderQueryAsync(new Contracts.DominionDAO.ContractDefinition.IsStakeholderFunction()
                {
                    FromAddress = AccountNumber
                });
                IsContributor = await DaoService.IsContributorQueryAsync(new Contracts.DominionDAO.ContractDefinition.IsContributorFunction()
                {
                    FromAddress = AccountNumber
                });
                TotalBalance = (long)await DaoService.DaoBalanceQueryAsync(new Contracts.DominionDAO.ContractDefinition.DaoBalanceFunction()
                {
                    FromAddress = AccountNumber
                });
                MyBalance = await DaoService.GetBalanceQueryAsync(new Contracts.DominionDAO.ContractDefinition.GetBalanceFunction()
                {
                    FromAddress = AccountNumber
                });
            }
            else
            {
                IsStakeHolder = false;
                IsContributor = false;
                TotalBalance = 0;
                MyBalance = 0;
            }
        }
        protected async Task DoContribute()
        {
            try
            {
                if (!IsDeployed)
                    return;
                if (DaoService != null && ContributionAmount > 0)
                {
                    await DaoService.ContributeRequestAsync(new Contracts.DominionDAO.ContractDefinition.ContributeFunction()
                    {
                        AmountToSend = ContributionAmount,
                        FromAddress = AccountNumber
                    });
                    await DoLoad();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    
                }
                disposable.Dispose();
                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        ~IndexViewModel()
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
}
