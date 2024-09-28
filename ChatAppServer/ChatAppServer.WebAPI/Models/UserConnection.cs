using System.ComponentModel.DataAnnotations;

namespace ChatAppServer.WebAPI.Models
{
    public class UserConnection
    {
        [Key]
        public string Id { get; set; }
        public required string UserId { get; set; }
        public required string ConnectionId { get; set; }

        public DateTime EntryDate { get; set; } = DateTime.Now;

    }

}
