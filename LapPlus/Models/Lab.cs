using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabPlus.Models
{
    public class Lab
    {
        [Key] public int LabId { get; set; }

        [Required, MaxLength(200)] public string Name { get; set; }
        public LabType LabType { get; set; }
        [Required, MaxLength(256)] public string Email { get; set; }
        [Required] public string PasswordHash { get; set; }
        [MaxLength(500)] public string Address { get; set; }
        [Required, MaxLength(100)] public string LicenseNo { get; set; }
        [Required] public string LicenseDocUrl { get; set; }
        public AccountStatus AccountStatus { get; set; } = AccountStatus.Pending;
        [MaxLength(500)] public string RejectionReason { get; set; }

        public int? ApprovedByAdminId { get; set; }
        [ForeignKey(nameof(ApprovedByAdminId))] public virtual Admin ApprovedByAdmin { get; set; }

        public virtual ICollection<LabReport> LabReports { get; set; } = new List<LabReport>();
        public virtual ICollection<Payout> Payouts { get; set; } = new List<Payout>();
        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
