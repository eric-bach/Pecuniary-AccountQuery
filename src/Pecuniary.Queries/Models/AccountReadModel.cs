using System;
using Newtonsoft.Json;
using Pecuniary.ViewModels;

namespace Pecuniary.Queries.Models
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

    public class ViewModel
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public string EventName { get; set; }
    }
}