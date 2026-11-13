using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace LabPlus.Models
{
    public class Doctor
    {
        [Key] public int DoctorId { get; set; }

        [Required, MaxLength(200)] public string FullName { get; set; }
        [Required, MaxLength(100)] public string FName { get; set; }
        [Required, MaxLength(100)] public string LName { get; set; }
        [Required, MaxLength(256)] public string Email { get; set; }
        [Required] public string PasswordHash { get; set; }
        [Required, MaxLength(150)] public string Specialty { get; set; }
        [Required, MaxLength(100)] public string LicenseNo { get; set; }
        [Required] public string LicenseDocUrl { get; set; }
        public int ExperienceYears { get; set; }
        [Column(TypeName = "decimal(18,2)")] public decimal ConsultationFee { get; set; }
        public AccountStatus AccountStatus { get; set; } = AccountStatus.Pending;

        // Admin approves doctor accounts
        public int? ApprovedByAdminId { get; set; }
        [ForeignKey(nameof(ApprovedByAdminId))] public virtual Admin ApprovedByAdmin { get; set; }

        // Derived attribute - computed from rated appointments
        [NotMapped]
        public double AvgRating =>
            Appointments == null ? 0 :
            Appointments.Where(a => a.Rating.HasValue).Select(a => (double)a.Rating.Value).DefaultIfEmpty(0).Average();

        public virtual ICollection<DoctorPhone> Phones { get; set; } = new List<DoctorPhone>();
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public virtual ICollection<Payout> Payouts { get; set; } = new List<Payout>();
        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }

    public class DoctorPhone
    {
        [Key] public int DoctorPhoneId { get; set; }
        [Required, MaxLength(30)] public string Phone { get; set; }
        public int DoctorId { get; set; }
        [ForeignKey(nameof(DoctorId))] public virtual Doctor Doctor { get; set; }
    }
}
