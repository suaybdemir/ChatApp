using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ChatAppServer.WebAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Key]
        public long IdNumber { get; set; }
        public string Name { get; set; } = "";
        public string? MiddleName { get; set; }
        public string? FamilyName { get; set; }
        [StringLength(800, ErrorMessage = "255 den fazla karakter girilemez.")]
        [Column(TypeName = "varchar(255)")]
        public string Address { get; set; } = "";
        public bool Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.Now;

        public string? AvatarPath { get; set; } = string.Empty;
        public bool Status { get; set; }

        public bool isActive { get; set; } = true;

        //[NotMapped]
        //public string? Password { get; set; }
        //[NotMapped]
        //[Compare(nameof(Password))]
        //public string? ConfirmPassword { get; set; }
    }
}
