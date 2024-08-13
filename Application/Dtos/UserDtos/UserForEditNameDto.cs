using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.UserDtos
{
    public class UserForEditNameRequest
    {
        [Required]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "El campo debe contener solo letras.")]
        public string Name { get; set; }
    }
}
