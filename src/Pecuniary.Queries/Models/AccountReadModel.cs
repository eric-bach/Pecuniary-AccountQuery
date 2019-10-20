using System;
using Newtonsoft.Json;

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

    /// <summary>
    /// This has to match AccountViewModel in Pecuniary.ViewModels
    /// </summary>
    public class AccountViewModel
    {
        public string Name { get; set; }

        public string AccountTypeCode { get; set; }
    }

    public class ViewModel
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public string EventName { get; set; }
    }
}