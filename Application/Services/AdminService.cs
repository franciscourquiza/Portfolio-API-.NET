using Application.Dtos.AdminDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services
{
    public class AdminService : IAdminService
    {
        public readonly IAdminRepository _repository;
        private readonly IMapper _mapper;
        public AdminService(IAdminRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Admin?> GetAdminByName(string name)
        {
            Admin? adminToSearch = await _repository.GetByName(name);
            if (adminToSearch == null) 
            {
                throw new NotFoundException("No se encontró el admin.");
            }
            return adminToSearch;
        }
        public async Task<Admin?> GetAdminForCreation(string email)
        {
            Admin? adminToSearch = await _repository.GetByEmail(email);
            return adminToSearch;
        }
        public async Task<Admin?> GetAdminByEmail(string email)
        {
            Admin? adminToSearch = await _repository.GetByEmail(email);
            if (adminToSearch == null)
            {
                throw new NotFoundException("No se encontró el admin");
            }
            return adminToSearch;
        }
        public async Task<List<Admin>> GetAllAdmins()
        {
            return await _repository.Get();
        }
        public async Task<Admin> AddAdmin(AdminForAddDto request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            Admin? userForAdmin = new Admin()
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
            await _repository.Add(userForAdmin);
            return userForAdmin;
        }
        public async Task<Admin> UpdateAdmin(AdminForEditDto request, string email)
        {
            Admin? adminToEdit = await _repository.GetByEmail(email);
            if (adminToEdit == null)
            {
                throw new NotFoundException("No se encontró el admin.");
            }
            Admin adminEdited = _mapper.Map(request, adminToEdit);
            await _repository.Update(adminEdited);
            return adminEdited;
        }
        public async Task DeleteAdminByEmail(string email)
        {
            Admin? adminToDelete = await _repository.GetByEmail(email);
            if (adminToDelete == null) 
            {
                throw new NotFoundException("No se encontró el admin.");
            }
            await _repository.DeleteByEmail(email);
        }
    }
}
