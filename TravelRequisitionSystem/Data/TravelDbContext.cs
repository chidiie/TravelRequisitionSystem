using Microsoft.EntityFrameworkCore;

namespace TravelRequisitionSystem.Data
{
    public class TravelDbContext : DbContext
    {
        public TravelDbContext(DbContextOptions<TravelDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TravelRequest>()
                .Property(u => u.TripType)
                .HasConversion<string>()
                .HasMaxLength(50);

            modelBuilder.Entity<TravelRequest>()
                .Property(u => u.TravelClass)
                .HasConversion<string>()
                .HasMaxLength(50);

            modelBuilder.Entity<TravelRequest>()
                .Property(u => u.RequisitionStatus)
                .HasConversion<string>()
                .HasMaxLength(50);
        }

        public DbSet<TravelRequest> TravelRequests { get; set; }
    }
}
