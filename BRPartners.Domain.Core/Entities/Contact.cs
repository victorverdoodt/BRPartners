using BRPartners.Domain.Core.Models;

namespace BRPartners.Domain.Core.Entities
{
    public class Contact : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
