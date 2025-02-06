using Application.Dtos.ProyectDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services
{
    public class ProyectService : IProyectService
    {
        private readonly IProyectRepository _repository;
        private readonly IMapper _mapper;
        public ProyectService(IProyectRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Proyect?> GetByTitle(string title) 
        {
            Proyect? proyect = await _repository.GetByTitle(title);
            return proyect;
        }
        public async Task<Proyect?> Get(int id) 
        {
            Proyect? proyect = await _repository.Get(id);
            return proyect;
        }
        public async Task<List<Proyect>> Get() 
        {
            return await _repository.Get();
        }
        public async Task<Proyect> Add(ProyectForAddDto request, string userEmail) 
        {
            if (request == null)
            {
                throw new InvalidOperationException("Debes completar el formulario correctamente.");
            }

            Proyect proyect = _mapper.Map<Proyect>(request);
            proyect.UserEmail = userEmail;
           
            await _repository.Add(proyect);
            return proyect;
        }
        public async Task<Proyect> Update(ProyectForEditDto request, string title, string userEmail)
        {
            Proyect? proyectToEdit = await _repository.GetByTitle(title);
            if (proyectToEdit == null)
            {
                throw new InvalidOperationException("No se encontró el proyecto.");
            }
            if (userEmail != proyectToEdit.UserEmail)
            {
                throw new InvalidOperationException("Este proyecto no pertenece a tu usuario.");
            }
            Proyect proyectEdited = _mapper.Map(request, proyectToEdit);
            await _repository.Update(proyectEdited);
            return proyectEdited;
        }
        public async Task Delete(string title, string userEmail) 
        {
            Proyect? proyect = await _repository.GetByTitle(title);
            if (proyect == null)
            {
                throw new NotFoundException("No se encontró un proyecto con ese título.");
            }
            if (userEmail == proyect.UserEmail)
            {
                await _repository.Delete(title);
            }
            if (userEmail != proyect.UserEmail)
            {
                throw new InvalidOperationException("Este proyecto no pertenece a tu usuario.");
            }
        }
    }
}
