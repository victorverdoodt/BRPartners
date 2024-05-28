using BRPartners.Domain.Core.Entities;
using BRPartners.Domain.Interfaces;
using MediatR;

namespace BRPartners.Application.Commands.Handlers
{
    public class SyncMongoCommandHandler : IRequestHandler<SyncMongoCommand, Unit>
    {
        private readonly IRepository<Contact> _mongoRepository;

        public SyncMongoCommandHandler(IRepository<Contact> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<Unit> Handle(SyncMongoCommand request, CancellationToken cancellationToken)
        {
            var contact = new Contact
            {
                Id = request.Id,
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone
            };

            switch (request.Operation)
            {
                case "Add":
                    await _mongoRepository.AddAsync(contact);
                    break;
                case "Update":
                    await _mongoRepository.UpdateAsync(contact);
                    break;
                case "Delete":
                    await _mongoRepository.DeleteAsync(request.Id);
                    break;
            }

            return Unit.Value;
        }
    }
}
