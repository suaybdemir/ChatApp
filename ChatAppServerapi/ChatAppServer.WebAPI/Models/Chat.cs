using System.ComponentModel.DataAnnotations;

namespace ChatAppServer.WebAPI.Models
{
    public sealed class Chat
    {
        [Key]
        public string Id { get; set; }

        public string UserId { get; set; }
        public string ToUserId { get; set; }

        public string Message { get; set; } = string.Empty;

        public DateTime Date {  get; set; }
    }
}
