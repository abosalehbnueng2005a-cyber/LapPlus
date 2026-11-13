using System.Linq;
using Microsoft.EntityFrameworkCore;
using LabPlus.Models;

namespace LabPlus.Data
{
    public class LabPlusDbContext : DbContext
    {
        public LabPlusDbContext(DbContextOptions<LabPlusDbContext> options) : base(options) { }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientPhone> PatientPhones { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorPhone> DoctorPhones { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<AdminPhone> AdminPhones { get; set; }
        public DbSet<Lab> Labs { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Payout> Payouts { get; set; }
        public DbSet<LabReport> LabReports { get; set; }
        public DbSet<PatientDocument> PatientDocuments { get; set; }
        public DbSet<DocumentShare> DocumentShares { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder b)
        {
            base.OnModelCreating(b);

            // Composite PK for the M:N junction
            b.Entity<DocumentShare>().HasKey(x => new { x.DocumentId, x.AppointmentId });
            b.Entity<DocumentShare>().HasOne(x => x.Document).WithMany(d => d.SharedInAppointments).HasForeignKey(x => x.DocumentId);
            b.Entity<DocumentShare>().HasOne(x => x.Appointment).WithMany(a => a.SharedDocuments).HasForeignKey(x => x.AppointmentId);

            // Admin has two FKs into Notification - disambiguate
            b.Entity<Notification>().HasOne(n => n.SentByAdmin).WithMany(a => a.SentNotifications).HasForeignKey(n => n.SentByAdminId);
            b.Entity<Notification>().HasOne(n => n.RecipientAdmin).WithMany().HasForeignKey(n => n.RecipientAdminId);

            // Unique emails / license numbers
            b.Entity<Patient>().HasIndex(x => x.Email).IsUnique();
            b.Entity<Doctor>().HasIndex(x => x.Email).IsUnique();
            b.Entity<Doctor>().HasIndex(x => x.LicenseNo).IsUnique();
            b.Entity<Admin>().HasIndex(x => x.Email).IsUnique();
            b.Entity<Lab>().HasIndex(x => x.Email).IsUnique();
            b.Entity<Lab>().HasIndex(x => x.LicenseNo).IsUnique();

            // Payout belongs to a doctor OR a lab (PostgreSQL check constraint)
            b.Entity<Payout>().ToTable(t => t.HasCheckConstraint("CK_Payout_Recipient",
                "(\"DoctorId\" IS NOT NULL AND \"LabId\" IS NULL) OR (\"DoctorId\" IS NULL AND \"LabId\" IS NOT NULL)"));

            // Store enums as readable strings
            foreach (var prop in b.Model.GetEntityTypes().SelectMany(e => e.GetProperties())
                         .Where(p => p.ClrType.IsEnum))
                prop.SetProviderClrType(typeof(string));

            // No accidental cascade-delete chains
            foreach (var fk in b.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                fk.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}
