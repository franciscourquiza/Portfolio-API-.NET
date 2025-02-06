using Application.Dtos.UserDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.User
{
    public class UserForAddValidator : AbstractValidator<UserForAddRequest>
    {
        public UserForAddValidator() 
        {
            RuleFor(user => user.Name)
                .NotEmpty().WithMessage("El campo es obligatorio.")
                .Matches("^[a-zA-Z ]{1,50}$").WithMessage("El campo debe contener solo letras y hasta 50 caracteres.");

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("El campo es obligatorio.")
                .EmailAddress().WithMessage("Por favor, ingrese una dirección de correo electrónico válida.");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("El campo es obligatorio.")
                .Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$")
                .WithMessage("La contraseña debe contener al menos una letra minúscula, una mayúscula, un número y al menos 8 caracteres.");

            RuleFor(user => user.Summary)
                .Matches("^[a-zA-Z0-9 ]{1,1500}$")
                .WithMessage("El campo solo puede contener hasta 1500 caracteres.")
                .When(user => user.Summary != null);

            RuleFor(user => user.Age)
                .NotEmpty().WithMessage("El campo es obligatorio.")
                .InclusiveBetween(18, 130).WithMessage("La edad permitida es entre 18 y 130 años.");

            RuleFor(user => user.Country)
                .Matches("^[a-zA-Z]{1,30}$")
                .WithMessage("El campo solamente permite utilizar hasta 30 caracteres, los cuales solamente deben ser letras.")
                .When(user => user.Country != null);

            RuleFor(user => user.State)
                .Matches("^[a-zA-Z ]{1,30}$")
                .WithMessage("El campo solamente permite utilizar hasta 30 caracteres, los cuales solamente deben ser letras.")
                .When(user => user.State != null);

            RuleFor(user => user.City)
                .Matches("^[a-zA-Z ]{1,30}$")
                .WithMessage("El campo solamente permite utilizar hasta 30 caracteres, los cuales solamente deben ser letras.")
                .When(user => user.City != null);

            RuleFor(user => user.Adress)
                .Matches("^[a-zA-Z0-9 ]+$")
                .WithMessage("Solamente se pueden ingresar letras y números.")
                .When(user => user.Adress != null);

            RuleFor(user => user.Phone)
                .Matches("^\\d{10}$")
                .WithMessage("El número debe estar compuesto por 10 cifras.")
                .When(user => user.Phone != null);

            RuleFor(user => user.LinkedInLink)
                .Matches("^(https?:\\/\\/)?([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,}(:\\d+)?(\\/.*)?$")
                .WithMessage("Solamente puedes ingresar URLs o Links aquí.")
                .When(user => user.LinkedInLink != null);

            RuleFor(user => user.GitHubLink)
                .Matches("^(https?:\\/\\/)?([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,}(:\\d+)?(\\/.*)?$")
                .WithMessage("Solamente puedes ingresar URLs o Links aquí.")
                .When(user => user.GitHubLink != null);
        }
    }
}
