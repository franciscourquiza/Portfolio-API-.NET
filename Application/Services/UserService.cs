using Application.Dtos.UserDtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        
        public UserWithoutPasswordDto? Get(string name) 
        {
            UserWithoutPasswordDto? user = Get(name);
            if (user.Name == name)
            {
                return user;
            }
            return null;
        }
        public List<User> Get() 
        {
            return _repository.Get();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _repository.GetByEmailAsync(email);
        }

        public UserWithoutPasswordDto? GetUserWithoutPassword(string email)
        {
            User? user = _repository.GetByEmail(email);
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

        public UserWithoutPasswordDto? GetUserWithoutPasswordByName(string name)
        {
            User? user = _repository.Get(name);
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

        public User? GetEmailForCreation(string email)
        {
            return _repository.GetByEmail(email);
        }
        public void Add(UserForAddRequest request)
        {
            User? user = _mapper.Map<User>(request);
            if (user == null)
            {
                throw new ArgumentException(nameof(request));
            }
            _repository.Add(user);
        }
        public void Update(UserForEditDto request, string email)
        {
            User userToEdit = _repository.GetByEmail(email);
            User userEdited = _mapper.Map(request, userToEdit);
            _repository.Update(userEdited);
        }
        public void Delete(string email)
        {
            _repository.DeleteByEmail(email);
        }
     
    }
}
