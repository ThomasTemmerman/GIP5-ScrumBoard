using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GIP5_ScrumBoard.Models
{
    public partial class ScrumBoardContext : IdentityDbContext
    {
        public ScrumBoardContext()
        {

        }

        public ScrumBoardContext(DbContextOptions<ScrumBoardContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Ticket> Ticket { get; set; }
        public virtual DbSet<Milestone> Milestone { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
