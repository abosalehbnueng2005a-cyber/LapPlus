namespace LabPlus.Models
{
    public enum Gender { Male, Female, Other }
    public enum AccountStatus { Pending, Active, Suspended, Rejected }
    public enum AdminRole { SuperAdmin, Moderator }
    public enum LabType { Laboratory, Radiology }
    public enum AppointmentType { Consultation, FollowUp }
    public enum SessionStatus { Scheduled, InProgress, Completed, Cancelled, Rejected }
    public enum PaymentStatus { Pending, Paid, Failed, Refunded }
    public enum RefundDecision { None, Approved, Rejected }
    public enum PayoutStatus { Pending, Paid, Failed }
    public enum DocType { LabReport, Xray, SkinImage, Prescription, Other }
    public enum ReportStatus { Uploaded, Processing, Analyzed, Failed }
    public enum ReviewStatus { NotRequired, Pending, Reviewed }
    public enum ChatInputType { Text, Image, Voice, File }
    public enum NotificationChannel { InApp, Email, Sms }
    public enum NotificationStatus { Pending, Sent, Failed }
}
