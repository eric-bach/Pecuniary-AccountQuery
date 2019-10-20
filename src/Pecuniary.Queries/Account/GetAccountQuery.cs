using System;
using MediatR;
using Pecuniary.Queries.Models;

namespace Pecuniary.Queries.Account
{
    public class GetAccountQuery : IRequest<AccountReadModel>
    {
        public Guid Id { get; set; }
    }
}
