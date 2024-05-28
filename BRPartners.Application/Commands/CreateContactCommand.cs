using MediatR;

namespace BRPartners.Application.Commands
{
    public class CreateContactCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
