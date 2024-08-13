using Application.Dtos.ProyectDtos;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProyectService
    {
        private readonly IProyectRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public ProyectService(IProyectRepository repository, IMapper mapper, IUserRepository userRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public Proyect? GetByTitle(string title) 
        {
            return _repository.GetByTitle(title);
        }
        public Proyect? Get(int id) 
        {
            return _repository.Get(id);
        }
        public List<Proyect> Get() 
        {
            return _repository.Get();
        }
        public Proyect Add(ProyectForAddDto request, string userEmail) 
        {
            if (request == null)
            {
                throw new InvalidOperationException("Debes completar el formulario correctamente.");
            }

            Proyect proyect = _mapper.Map<Proyect>(request);
            proyect.UserEmail = userEmail;
           
            _repository.Add(proyect);
            return proyect;
        }
        public void Update(ProyectForEditDto request, string title, string userEmail)
        {
            Proyect proyectToEdit = _repository.GetByTitle(title);
            if (proyectToEdit == null)
            {
                throw new InvalidOperationException("No se encontró el proyecto.");
            }
            if (userEmail == proyectToEdit.UserEmail)
            {
                Proyect proyectEdited = _mapper.Map(request, proyectToEdit);
                _repository.Update(proyectEdited);
            }
            if (userEmail != proyectToEdit.UserEmail)
            {
                throw new InvalidOperationException("Este proyecto no pertenece a tu usuario.");
            }
        }
        public void Delete(string title, string userEmail) 
        {
            Proyect? proyect = _repository.GetByTitle(title);
            if (userEmail == proyect.UserEmail)
            {
                _repository.Delete(title);
            }
            if (userEmail != proyect.UserEmail)
            {
                throw new InvalidOperationException("Este proyecto no pertenece a tu usuario.");
            }
        }
    }
}
