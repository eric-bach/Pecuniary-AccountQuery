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
    }
}