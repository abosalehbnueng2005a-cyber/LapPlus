using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabPlus.Models
{
    public class ChatMessage
    {
        [Key] public int MessageId { get; set; }

        [Required] public string UserQuestion { get; set; }
        public string BotAnswer { get; set; }
        public ChatInputType InputType { get; set; }
        public bool DisclaimerShown { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int PatientId { get; set; }                 // Patient chats with chatbot
        [ForeignKey(nameof(PatientId))] public virtual Patient Patient { get; set; }
    }
}
