using System;
using MediatR;
using Pecuniary.Account.Data.Models;

namespace Pecuniary.Account.Query.Queries
{
    public class GetAccountQuery : IRequest<AccountReadModel>
    {
        public Guid Id { get; set; }
    }
}
