using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabPlus.Models
{
    public class Payment
    {
        [Key] public int PaymentId { get; set; }

        [Column(TypeName = "decimal(18,2)")] public decimal Amount { get; set; }
        [Column(TypeName = "decimal(18,2)")] public decimal PlatformShare { get; set; }
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        [MaxLength(200)] public string GatewayRef { get; set; }
        public RefundDecision RefundDecision { get; set; } = RefundDecision.None;
        [MaxLength(500)] public string RefundReason { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int PatientId { get; set; }                 // Patient pays
        [ForeignKey(nameof(PatientId))] public virtual Patient Patient { get; set; }

        public int? AppointmentId { get; set; }            // payment for an appointment
        [ForeignKey(nameof(AppointmentId))] public virtual Appointment Appointment { get; set; }

        public int? RefundDecidedByAdminId { get; set; }   // Admin decides refunds
        [ForeignKey(nameof(RefundDecidedByAdminId))] public virtual Admin RefundDecidedByAdmin { get; set; }
    }

    public class Payout
    {
        [Key] public int PayoutId { get; set; }

        [Column(TypeName = "decimal(18,2)")] public decimal Amount { get; set; }
        public PayoutStatus Status { get; set; } = PayoutStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Exactly one of these is set (doctor OR lab) - enforced by a check constraint in the DbContext
        public int? DoctorId { get; set; }
        [ForeignKey(nameof(DoctorId))] public virtual Doctor Doctor { get; set; }

        public int? LabId { get; set; }
        [ForeignKey(nameof(LabId))] public virtual Lab Lab { get; set; }
    }
}
