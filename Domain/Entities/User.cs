using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class User
    {
        public string Name { get; set; }
        [Key]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string? Summary { get; set; }
        public int? Age { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Adress { get; set; }
        public string? Phone { get; set; }
        public string? LinkedInLink { get; set; }
        public string? GitHubLink { get; set;}
        [Required]
        public string UserRole { get; set; }
    }

}
