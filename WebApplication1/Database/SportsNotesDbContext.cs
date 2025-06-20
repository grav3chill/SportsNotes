using Microsoft.EntityFrameworkCore;

using SportsNotes.Entities;
namespace SportsNotes.Database
{
    public class SportsNotesDbContext : DbContext
    {
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ProgressRecord> ProgressRecords { get; set; }

        public SportsNotesDbContext(DbContextOptions<SportsNotesDbContext> options) : base (options)
        {
            
        }
    }
}
