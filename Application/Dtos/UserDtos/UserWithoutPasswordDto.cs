using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.UserDtos
{
    public class UserWithoutPasswordDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
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
