using Application.Dtos.WorkExperienceDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services
{
    public class WorkExperienceService : IWorkExperienceService
    {
        private readonly IWorkExperienceRepository _repository;
        private readonly IMapper _mapper;
        public WorkExperienceService(IWorkExperienceRepository repository, IMapper mapper) 
        { 
            _repository = repository; 
            _mapper = mapper;
        }

        public async Task<WorkExperience?> Get(string title) 
        {
            return await _repository.GetByTitle(title);
        }

        public async Task<WorkExperience?> Get(int id) 
        {
            return await _repository.Get(id);
            
        }

        public async Task<List<WorkExperience>> Get() 
        {
            return await _repository.Get();
        }

        public async Task<WorkExperience?> AddWorkExperience(WorkExperienceForAdd request, string userEmail) 
        {
            if (request == null)
            {
                throw new InvalidOperationException("Debes completar el formulario correctamente.");
            }

            WorkExperience workExperience = _mapper.Map<WorkExperience>(request);
            workExperience.UserEmail = userEmail;

            await _repository.Add(workExperience);
            return workExperience;
        }

        public async Task<WorkExperience?> Update(WorkExperienceForEditDto request, string title, string userEmail)
        {
            WorkExperience? workExperienceToEdit = await _repository.GetByTitle(title);
            if (workExperienceToEdit == null)
            {
                throw new InvalidOperationException("No se encontró la experiencia laboral.");
            }
            if (userEmail != workExperienceToEdit.UserEmail)
            {
                throw new InvalidOperationException("Esta experiencia laboral no pertenece a tu usuario.");
            }
            WorkExperience workExperienceEdited = _mapper.Map(request, workExperienceToEdit);
            await _repository.Update(workExperienceEdited);
            return workExperienceEdited;
        }
        
        public async Task Delete(string title, string userEmail) 
        {
            WorkExperience? workExperience = await _repository.GetByTitle(title);
            if (workExperience == null)
            {
                throw new NotFoundException("No se encontró una experiencia laboral con ese título.");
            }
            if (userEmail == workExperience.UserEmail)
            {
                await _repository.Delete(title);
            }
            if (userEmail != workExperience.UserEmail)
            {
                throw new InvalidOperationException("Esta experiencia laboral no pertenece a tu usuario.");
            }
        }
    }
}
