using Application.Dtos.AuthDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Auth
{
    public class AuthValidator : AbstractValidator<AuthenticationBodyRequest>
    {
        public AuthValidator() 
        {
            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("El campo es obligatorio.")
                .EmailAddress().WithMessage("Por favor, ingrese una dirección de correo electrónico válida.");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("El campo es obligatorio.")
                .Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$")
                .WithMessage("La contraseña debe contener al menos una letra minúscula, una mayúscula, un número y al menos 8 caracteres.");
        }
    }
}
