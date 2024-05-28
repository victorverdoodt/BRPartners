using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BRPartners.Application.Commands
{
    public class CreateContactCommand : IRequest<Guid>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
