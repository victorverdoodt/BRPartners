using BRPartners.Domain.Core.Entities;
using MediatR;

namespace BRPartners.Application.Queries
{
    public class GetContactsQuery : IRequest<IEnumerable<Contact>> { }
}
