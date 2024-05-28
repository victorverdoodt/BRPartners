using BRPartners.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BRPartners.Infrastructure.Data.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
