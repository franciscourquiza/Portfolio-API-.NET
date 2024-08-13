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
        [Required]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "El campo debe contener solo letras.")]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Por favor, ingrese una dirección de correo electrónico válida.")]
        public string Email { get; set; }

        [RegularExpression("^.{0,1500}$", ErrorMessage = "El campo solo puede contener hasta 1500 caracteres.")]
        public string? Summary { get; set; }

        [RegularExpression("^(1[0-2][0-9]|130|[1-9][0-9])$", ErrorMessage = "La edad permitida es de 10 a 130 años, ingrese una correcta.")]
        public int? Age { get; set; }

        [RegularExpression("^[a-zA-Z ]{1,30}$", ErrorMessage = "El campo solamente permite utilizar hasta 30 caracteres, los cuales solamente deben ser letras.")]
        public string? Country { get; set; }

        [RegularExpression("^[a-zA-Z ]{1,30}$", ErrorMessage = "El campo solamente permite utilizar hasta 30 caracteres, los cuales solamente deben ser letras.")]
        public string? State { get; set; }

        [RegularExpression("^[a-zA-Z ]{1,30}$", ErrorMessage = "El campo solamente permite utilizar hasta 30 caracteres, los cuales solamente deben ser letras.")]
        public string? City { get; set; }

        [RegularExpression("^[a-zA-Z ]{1,30}$", ErrorMessage = "El campo solamente permite utilizar hasta 30 caracteres, los cuales solamente deben ser letras.")]
        public string? Adress { get; set; }

        [RegularExpression("^\\d{10}$", ErrorMessage = "El numero debe estar compuesto por 10 cifras.")]
        public string? Phone { get; set; }

        [RegularExpression("^(https?:\\/\\/)?([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,}(:\\d+)?(\\/.*)?$", ErrorMessage = "Solamente puedes ingresar URLs o Links aquí.")]
        public string? LinkedInLink { get; set; }

        [RegularExpression("^(https?:\\/\\/)?([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,}(:\\d+)?(\\/.*)?$", ErrorMessage = "Solamente puedes ingresar URLs o Links aquí.")]
        public string? GitHubLink { get; set; }
    }
}
