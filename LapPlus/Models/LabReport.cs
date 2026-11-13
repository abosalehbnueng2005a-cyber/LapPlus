using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabPlus.Models
{
    public class LabReport
    {
        [Key] public int ReportId { get; set; }

        [MaxLength(150)] public string TestType { get; set; }
        public DateTime ReportDate { get; set; }
        public string ExtractedValues { get; set; }        // JSON from OCR
        public string AnalysisResult { get; set; }
        [Column(TypeName = "decimal(5,2)")] public decimal? OcrConfidence { get; set; }
        public bool HasCriticalValue { get; set; }
        public ReportStatus Status { get; set; } = ReportStatus.Uploaded;
        public bool IsReportIncorrect { get; set; }
        [MaxLength(500)] public string ReportReason { get; set; }
        public ReviewStatus ReviewStatus { get; set; } = ReviewStatus.NotRequired;

        public int PatientId { get; set; }                 // Patient uploads
        [ForeignKey(nameof(PatientId))] public virtual Patient Patient { get; set; }

        public int? LabId { get; set; }                    // Lab has results
        [ForeignKey(nameof(LabId))] public virtual Lab Lab { get; set; }

        public int? ReviewedByAdminId { get; set; }        // Admin reviews
        [ForeignKey(nameof(ReviewedByAdminId))] public virtual Admin ReviewedByAdmin { get; set; }
    }
}
