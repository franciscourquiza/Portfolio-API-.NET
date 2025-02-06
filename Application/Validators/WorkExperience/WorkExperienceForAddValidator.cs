using FluentValidation;
using Application.Dtos.WorkExperienceDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.WorkExperience
{
    public class WorkExperienceForAddValidator : AbstractValidator<WorkExperienceForAdd>
    {
        public WorkExperienceForAddValidator() 
        {
            RuleFor(workExperience => workExperience.Title)
                .NotEmpty().WithMessage("Se requiere un título.")
                .MaximumLength(40).WithMessage("El título debe contener hasta 40 caracteres como máximo.")
                .Matches(@"^[a-zA-Z\s]+$").WithMessage("El título solamente debe contener letras.");
            RuleFor(workExperience => workExperience.Description)
                .NotEmpty().WithMessage("Se requiere una descripción.")
                .MaximumLength(3000).WithMessage("El título debe contener hasta 3000 caracteres como máximo.");
        }
    }
}
