using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabPlus.Models
{
    public class Notification
    {
        [Key] public int NotificationId { get; set; }

        [MaxLength(50)] public string NotifType { get; set; }
        [Required] public string Content { get; set; }
        public NotificationChannel Channel { get; set; }
        public NotificationStatus Status { get; set; } = NotificationStatus.Pending;
        public bool IsRead { get; set; }
        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        // Recipient: one of these four is set (all actors "receive" notifications)
        public int? PatientId { get; set; }
        [ForeignKey(nameof(PatientId))] public virtual Patient Patient { get; set; }

        public int? DoctorId { get; set; }
        [ForeignKey(nameof(DoctorId))] public virtual Doctor Doctor { get; set; }

        public int? LabId { get; set; }
        [ForeignKey(nameof(LabId))] public virtual Lab Lab { get; set; }

        public int? RecipientAdminId { get; set; }
        [ForeignKey(nameof(RecipientAdminId))] public virtual Admin RecipientAdmin { get; set; }

        // Admin sends announcements
        public int? SentByAdminId { get; set; }
        [ForeignKey(nameof(SentByAdminId))] public virtual Admin SentByAdmin { get; set; }
    }
}
