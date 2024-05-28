using MediatR;

namespace BRPartners.Application.Commands
{
    public class SyncMongoCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Operation { get; set; }  // Add, Update, Delete
    }
}
