using Application.Dtos.AuthDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Auth
{
    public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordRequestValidator() 
        {
            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("El campo es obligatorio.")
                .EmailAddress().WithMessage("Por favor, ingrese una dirección de correo electrónico válida.");
        }
    }
}
