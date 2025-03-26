using Microsoft.EntityFrameworkCore;

namespace Clinic_WebApp.Models
{
    public class ClinicDbContext : DbContext
    {
        public ClinicDbContext(DbContextOptions<ClinicDbContext> options) : base(options) { }

        public DbSet<BookAppoitment> BookAppoitments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BookAppoitment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.BookAppoitments)
                .HasForeignKey(a => a.DoctorID);
        }
    }
}
