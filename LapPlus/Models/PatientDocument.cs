using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabPlus.Models
{
    public class PatientDocument
    {
        [Key] public int DocumentId { get; set; }

        [Required] public string FileUrl { get; set; }
        public DocType DocType { get; set; }
        public string AiAssessment { get; set; }
        public string BasicGuidance { get; set; }
        public bool ConsultRecommendation { get; set; }
        public bool IsShared { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        public int PatientId { get; set; }                 // Patient uploads
        [ForeignKey(nameof(PatientId))] public virtual Patient Patient { get; set; }

        public virtual ICollection<DocumentShare> SharedInAppointments { get; set; } = new List<DocumentShare>();
    }

    // M:N junction: Patient Document "shared in" Appointment
    public class DocumentShare
    {
        public int DocumentId { get; set; }
        public virtual PatientDocument Document { get; set; }

        public int AppointmentId { get; set; }
        public virtual Appointment Appointment { get; set; }

        public DateTime SharedAt { get; set; } = DateTime.UtcNow;
    }
}
