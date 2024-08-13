using Application.Dtos.WorkExperienceDtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class WorkExperienceService
    {
        private readonly IWorkExperienceRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public WorkExperienceService(IWorkExperienceRepository repository, IMapper mapper, IUserRepository userRepository) 
        { 
            _repository = repository; 
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public WorkExperience? Get(string title) 
        {
            return _repository.GetByTitle(title);
        }

        public WorkExperience? Get(int id) 
        {
            return _repository.Get(id);
        }

        public List<WorkExperience> Get() 
        {
            return _repository.Get();
        }

        public WorkExperience AddWorkExperience(WorkExperienceForAdd request, string userEmail) 
        {
            if (request == null)
            {
                throw new InvalidOperationException("Debes completar el formulario correctamente.");
            }

            WorkExperience workExperience = _mapper.Map<WorkExperience>(request);
            workExperience.UserEmail = userEmail;

            _repository.Add(workExperience);
            return workExperience;
        }

        public void Update(WorkExperienceForEditDto request, string title, string userEmail)
        {
            WorkExperience workExperienceToEdit = _repository.GetByTitle(title);
            if (workExperienceToEdit == null)
            {
                throw new InvalidOperationException("No se encontró la experiencia laboral.");
            }
            if (userEmail == workExperienceToEdit.UserEmail)
            {
                WorkExperience workExperienceEdited = _mapper.Map(request, workExperienceToEdit);
                _repository.Update(workExperienceEdited);
            }
            if (userEmail != workExperienceToEdit.UserEmail)
            {
                throw new InvalidOperationException("Esta experiencia laboral no pertenece a tu usuario.");
            }
        }
        
        public void Delete(string title, string userEmail) 
        {
            WorkExperience? workExperience = _repository.GetByTitle(title);
            if (userEmail == workExperience.UserEmail)
            {
                _repository.Delete(title);
            }
            if (userEmail != workExperience.UserEmail)
            {
                throw new InvalidOperationException("Esta experiencia laboral no pertenece a tu usuario.");
            }
        }
    }
}
