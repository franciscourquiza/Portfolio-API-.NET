using Application.Dtos.UserDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<UserWithoutPasswordDto> GetUserWithoutPasswordByName(string name)
        {
            User? user = await _repository.GetByName(name);
            if (user?.Name == name)
            {
                UserWithoutPasswordDto userWithoutPassword = new UserWithoutPasswordDto()
                {
                    Name = name,
                    Email = user.Email,
                    Summary = user.Summary,
                    Age = user.Age,
                    Country = user.Country,
                    State = user.State,
                    City = user.City,
                    Adress = user.Adress,
                    Phone = user.Phone,
                    LinkedInLink = user.LinkedInLink,
                    GitHubLink = user.GitHubLink,
                };
                return userWithoutPassword;
            }
            return null;
        }
        public async Task<UserWithoutPasswordDto> GetUserWithoutPasswordByEmail(string email)
        {
            User? user = await _repository.GetByEmail(email);
            if (user?.Email == email)
            {
                UserWithoutPasswordDto userWithoutPassword = new UserWithoutPasswordDto()
                {
                    Name = user.Name,
                    Email = email,
                    Summary = user.Summary,
                    Age = user.Age,
                    Country = user.Country,
                    State = user.State,
                    City = user.City,
                    Adress = user.Adress,
                    Phone = user.Phone,
                    LinkedInLink = user.LinkedInLink,
                    GitHubLink = user.GitHubLink,
                };
                return userWithoutPassword;
            }
            return null;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _repository.Get();
        }

        public async Task<User> CreateUser(UserForAddRequest request)
        {
            User? user = _mapper.Map<User>(request);

            if (user == null)
            {
                throw new ArgumentException(nameof(request));
            }
            await _repository.Add(user);

            return user;
        }

        public async Task<User> UpdateUser(UserForEditDto request, string userEmail)
        {
            User? userToEdit = await _repository.GetByEmail(userEmail);
            User? userEdited = _mapper.Map(request, userToEdit);
            await _repository.Update(userEdited);
            return userEdited;
        }

        public async Task DeleteUserByEmail(string email)
        {
            User? userToDelete = await _repository.GetByEmail(email);
            if (userToDelete == null) { throw new NotFoundException("Usuario no encontrado."); }
            await _repository.DeleteByEmail(email);
        }

    }
}
