using System.Collections.Generic;
using System.Transactions;
using Newtonsoft.Json;
using Pecuniary.Account.Data.ViewModels;

namespace Pecuniary.Account.Data.Models
{
    public class AccountReadModel : BaseReadModel
    {
        [JsonProperty("_source")]
        public AccountSource Source { get; set; }
    }

    public class AccountSource : ViewModel
    {
        public AccountViewModel Account { get; set; }
        public List<TransactionViewModel> Transactions { get; set; }
    }

    public class TransactionViewModel
    {
        // TODO This should be an object if ES can update an object instead of string
        public string Transaction { get; set; }
    }
}