using Microsoft.EntityFrameworkCore;
using PukesMVC.Models.Entities;

namespace PukesMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<State> States { get; set; }
        public DbSet<Puke> Pukes { get; set; }


    }
}
