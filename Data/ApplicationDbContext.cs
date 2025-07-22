using Microsoft.EntityFrameworkCore;
using EventManagementApi.Models;

namespace EventManagementApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Event> Events { get; set; }   
    }
}
