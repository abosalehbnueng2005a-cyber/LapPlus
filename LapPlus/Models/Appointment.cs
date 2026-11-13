using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabPlus.Models
{
    public class Appointment
    {
        [Key] public int AppointmentId { get; set; }

        public AppointmentType AppointmentType { get; set; }
        public DateTime ScheduledAt { get; set; }
        public SessionStatus SessionStatus { get; set; } = SessionStatus.Scheduled;
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
        [MaxLength(100)] public string PackageName { get; set; }
        [MaxLength(500)] public string RejectReason { get; set; }
        public string MedicalNotes { get; set; }
        public string RecommendedTests { get; set; }
        [Range(1, 5)] public int? Rating { get; set; }
        public string Feedback { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int PatientId { get; set; }                 // Patient books
        [ForeignKey(nameof(PatientId))] public virtual Patient Patient { get; set; }

        public int DoctorId { get; set; }                  // Doctor conducts
        [ForeignKey(nameof(DoctorId))] public virtual Doctor Doctor { get; set; }

        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public virtual ICollection<DocumentShare> SharedDocuments { get; set; } = new List<DocumentShare>();
    }
}
