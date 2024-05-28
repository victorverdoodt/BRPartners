using BRPartners.Domain.Core.Entities;
using BRPartners.Domain.Interfaces;
using MediatR;

namespace BRPartners.Application.Commands.Handlers
{
    public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, Guid>
    {
        private readonly IContactRepository _efRepository;
        private readonly IRabbitMQPublisher _rabbitMQPublisher;

        public CreateContactCommandHandler(IContactRepository efRepository, IRabbitMQPublisher rabbitMQPublisher)
        {
            _efRepository = efRepository;
            _rabbitMQPublisher = rabbitMQPublisher;
        }

        public async Task<Guid> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            var contact = new Contact
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone
            };


            await _efRepository.AddAsync(contact);

            var syncCommand = new SyncMongoCommand
            {
                Id = contact.Id,
                Name = contact.Name,
                Email = contact.Email,
                Phone = contact.Phone,
                Operation = "Add"
            };

            _rabbitMQPublisher.Publish(syncCommand);

            return contact.Id;
        }
    }
}
