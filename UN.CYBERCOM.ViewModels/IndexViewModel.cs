using DynamicData.Binding;
using Nethereum.Web3;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UN.CYBERCOM.ViewModels
{
    public class IndexViewModel : ReactiveObject
    {
        protected Web3 Web3 { get; }
        private string? accountNumber;
        public string? AccountNumber
        {
            get => this.AccountNumber;
            set => this.RaiseAndSetIfChanged(ref accountNumber, value);
        }
        public IndexViewModel(Web3 web3)
        {
            Web3 = web3;
        }
    }
}
