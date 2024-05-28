using BRPartners.Domain.Core.Entities;
using BRPartners.Domain.Interfaces;

namespace BRPartners.Infrastructure.Data.Persistence.Context.Repositories
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        public ContactRepository(AppDbContext context) : base(context)
        {
        }
    }
}
