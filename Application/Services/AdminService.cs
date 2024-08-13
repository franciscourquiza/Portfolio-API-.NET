using Application.Dtos.AdminDtos;
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
    public class AdminService
    {
        public readonly IAdminRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public AdminService(IAdminRepository repository, IMapper mapper, IUserRepository userRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public Admin? Get(string name)
        {
            Admin? user = _repository.Get(name);
            if (user == null) 
            {
                return null;
            }
            return user;
        }
        public Admin? GetByEmail(string email)
        {
            Admin? user = GetEmailForCreation(email);
            return user;
        }
        public Admin? GetEmailForCreation(string email)
        {
            return _repository.GetByEmail(email);
        }
        public List<Admin> Get()
        {
            return _repository.Get();
        }
        public User AddAdmin(AdminForAddDto request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            User? userForAdmin = new User()
            {
                Email = request.Email,
                Adress = request.Adress,
                Age = request.Age,
                City = request.City,
                Country = request.Country,  
                GitHubLink = request.GitHubLink,
                LinkedInLink = request.LinkedInLink,
                Name = request.Name,
                Password = request.Password,
                Phone = request.Phone,  
                State = request.State,
                Summary = request.Summary,
                UserRole = "Admin",
            };
            _repository.AddAdmin(userForAdmin);
            return userForAdmin;
        }
        public void Update(AdminForEditDto request, string email)
        {
            Admin adminToEdit = _repository.GetByEmail(email);
            if (adminToEdit == null)
            {
                throw new ArgumentNullException(nameof(email));
            }
            Admin adminEdited = _mapper.Map(request, adminToEdit);
            _repository.Update(adminEdited);
        }
        public void Delete(string email)
        {
            var entity = _repository.GetByEmail(email);
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _repository.DeleteByEmail(email);
        }
    }
}
