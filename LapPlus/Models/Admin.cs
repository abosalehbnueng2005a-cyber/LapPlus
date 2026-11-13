using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabPlus.Models
{
    public class Admin
    {
        [Key] public int AdminId { get; set; }

        [Required, MaxLength(200)] public string FullName { get; set; }
        [Required, MaxLength(100)] public string FName { get; set; }
        [Required, MaxLength(100)] public string LName { get; set; }
        [Required, MaxLength(256)] public string Email { get; set; }
        [Required] public string PasswordHash { get; set; }
        public AdminRole Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<AdminPhone> Phones { get; set; } = new List<AdminPhone>();
        public virtual ICollection<Doctor> ApprovedDoctors { get; set; } = new List<Doctor>();
        public virtual ICollection<Lab> ApprovedLabs { get; set; } = new List<Lab>();
        public virtual ICollection<LabReport> ReviewedReports { get; set; } = new List<LabReport>();
        public virtual ICollection<Payment> DecidedRefunds { get; set; } = new List<Payment>();
        public virtual ICollection<Notification> SentNotifications { get; set; } = new List<Notification>();
    }

    public class AdminPhone
    {
        [Key] public int AdminPhoneId { get; set; }
        [Required, MaxLength(30)] public string Phone { get; set; }
        public int AdminId { get; set; }
        [ForeignKey(nameof(AdminId))] public virtual Admin Admin { get; set; }
    }
}
