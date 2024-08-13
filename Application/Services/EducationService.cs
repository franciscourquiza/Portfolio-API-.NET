using Application.Dtos.EducationDtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class EducationService 
    {
        private readonly IEducationRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public EducationService(IEducationRepository repository, IMapper mapper, IUserRepository userRepository) 
        {
            _repository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public Education? GetByTitle(string title)
        {
            return _repository.GetByTitle(title);
        }
        public Education? Get(int id)
        {
            return _repository.Get(id);
        }

        public List<Education> Get()
        {
            return _repository.Get();
        }

        public Education Add(EducationForAddDto request, string userEmail)
        {
            if (request == null)
            {
                throw new InvalidOperationException("Debes completar el formulario correctamente.");
            }

            Education education = _mapper.Map<Education>(request);
            education.UserEmail = userEmail;

            _repository.Add(education);
            return education;
        }
        public void Update(EducationForEditDto request, string title, string userEmail)
        {
            Education educationToEdit = _repository.GetByTitle(title);
            if (educationToEdit == null)
            {
                throw new InvalidOperationException("No se encontró la educación.");
            }
            if (userEmail == educationToEdit.UserEmail)
            {
                Education educationEdited = _mapper.Map(request, educationToEdit);
                _repository.Update(educationEdited);
            }
            if (userEmail != educationToEdit.UserEmail)
            {
                throw new InvalidOperationException("Esta educación no pertenece a tu usuario.");
            }
        }
        public void Delete(string title, string userEmail)
        {
            Education? education = _repository.GetByTitle(title);
            if (userEmail == education.UserEmail)
            {
                _repository.Delete(title);
            }
            if (userEmail != education.UserEmail)
            {
                throw new InvalidOperationException("Esta educación no pertenece a tu usuario.");
            }
        }
    }
}
