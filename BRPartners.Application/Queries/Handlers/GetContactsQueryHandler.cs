using BRPartners.Domain.Core.Entities;
using BRPartners.Domain.Interfaces;
using MediatR;

namespace BRPartners.Application.Queries.Handlers
{
    public class GetContactsQueryHandler : IRequestHandler<GetContactsQuery, IEnumerable<Contact>>
    {
        private readonly IRepository<Contact> _repository;

        public GetContactsQueryHandler(IRepository<Contact> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Contact>> Handle(GetContactsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
