using System.ComponentModel.DataAnnotations;


namespace Application.Dtos.UserDtos
{
    public class UserForAddRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Summary { get; set; }
        public int? Age { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Adress { get; set; }
        public string? Phone { get; set; }
        public string? LinkedInLink { get; set; }
        public string? GitHubLink { get; set; }
    }
}
