using System;

namespace Pecuniary.Account.Data.ViewModels
{
    public class ViewModel
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public string EventName { get; set; }
    }
}