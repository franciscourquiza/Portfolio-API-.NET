using Application.Dtos.EducationDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Application.Services
{
    public class EducationService : IEducationService
    {
        private readonly IEducationRepository _repository;
        private readonly IMapper _mapper;
        public EducationService(IEducationRepository repository, IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Education?> GetByTitle(string title)
        {
            Education? education = await _repository.GetByTitle(title);
            return education;
        }
        public async Task<Education?> GetById(int id)
        {
            Education? education = await _repository.Get(id);
            return education;
        }

        public async Task<List<Education>> Get()
        {
            return await _repository.Get();
        }

        public async Task<Education> Add(EducationForAddDto request, string userEmail)
        {
            if (request == null)
            {
                throw new InvalidOperationException("Debes completar el formulario correctamente.");
            }

            Education education = _mapper.Map<Education>(request);
            education.UserEmail = userEmail;

            await _repository.Add(education);
            return education;
        }
        public async Task<Education?> Update(EducationForEditDto request, string title, string userEmail)
        {
            Education? educationToEdit = await _repository.GetByTitle(title);
            if (educationToEdit == null)
            {
                throw new NotFoundException("No se encontró la educación.");
            }
            if (userEmail != educationToEdit.UserEmail)
            {
                throw new InvalidOperationException("Esta educación no pertenece a tu usuario.");
            }
            Education educationEdited = _mapper.Map(request, educationToEdit);
            await _repository.Update(educationEdited);
            return educationEdited;
        }
        public async Task Delete(string title, string userEmail)
        {
            Education? education = await _repository.GetByTitle(title);
            if (education == null)
            {
                throw new NotFoundException("No se encontró una educación con ese título");
            }
            if (userEmail == education.UserEmail)
            {
                await _repository.Delete(title);
            }
            if (userEmail != education.UserEmail)
            {
                throw new InvalidOperationException("Esta educación no pertenece a tu usuario.");
            }
        }
    }
}
