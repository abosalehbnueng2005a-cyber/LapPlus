using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabPlus.Models
{
    public class Patient
    {
        [Key] public int PatientId { get; set; }

        [Required, MaxLength(200)] public string FullName { get; set; }
        [Required, MaxLength(100)] public string FName { get; set; }
        [Required, MaxLength(100)] public string LName { get; set; }
        [Required, MaxLength(256)] public string Email { get; set; }
        [Required] public string PasswordHash { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public AccountStatus AccountStatus { get; set; } = AccountStatus.Active;
        [MaxLength(100)] public string PackageStatus { get; set; }
        public DateTime? NextTestDueDate { get; set; }
        public DateTime? FollowUpDate { get; set; }

        // Derived attribute (dashed ellipse) - not stored
        [NotMapped]
        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - DateOfBirth.Year;
                if (DateOfBirth.Date > today.AddYears(-age)) age--;
                return age;
            }
        }

        public virtual ICollection<PatientPhone> Phones { get; set; } = new List<PatientPhone>();
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public virtual ICollection<LabReport> LabReports { get; set; } = new List<LabReport>();
        public virtual ICollection<PatientDocument> Documents { get; set; } = new List<PatientDocument>();
        public virtual ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();
        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }

    public class PatientPhone
    {
        [Key] public int PatientPhoneId { get; set; }
        [Required, MaxLength(30)] public string Phone { get; set; }
        public int PatientId { get; set; }
        [ForeignKey(nameof(PatientId))] public virtual Patient Patient { get; set; }
    }
}
