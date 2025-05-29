using Microsoft.EntityFrameworkCore;
using MMAPredictor.DataAccess.Entities;

namespace MMAPredictor.DataAccess
{
    public class MMAPredictorDbContext :DbContext 
    {
        public MMAPredictorDbContext(DbContextOptions<MMAPredictorDbContext> options) : base(options)
        {
            
        }

        public DbSet<Fighter> Fighters { get; set; }
        DbSet<Fight> Fights { get; set; }
        DbSet<FightResult> FightResults { get; set; }
        DbSet<FightRound> FightRounds { get; set; }
        DbSet<MmaEvent> MmaEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fighter>()
                .HasIndex(f => f.Name)
                .IsUnique();
        }
    }
}
