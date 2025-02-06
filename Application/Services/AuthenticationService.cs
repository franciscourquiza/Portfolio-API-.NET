using Application.Dtos.AuthDtos;
using Domain.Entities;
using Domain.Interfaces;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Application.Interfaces;
namespace Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _repository;
        private readonly ITokenVerifyRepository _tokenVerifyRepository;
        private readonly IEmailService _emailService;
        private readonly IPasswordHasher<User> _passwordHasher;
        public AuthenticationService(IUserRepository repository, ITokenVerifyRepository tokenVerifyRepository, IEmailService emailService, IPasswordHasher<User> passwordHasher) 
        { 
            _repository = repository;
            _tokenVerifyRepository = tokenVerifyRepository;
            _emailService = emailService;
            _passwordHasher = passwordHasher;
        }

        public async Task<Tuple<bool,User?>> ValidateUser(string email, string password)
        {
            User? userForLogin = await _repository.GetByEmail(email);
            if (userForLogin != null)
            {
                if (userForLogin.Password == password)
                    return new Tuple<bool, User?>(true, userForLogin);
                return new Tuple<bool, User?>(false, userForLogin);
            }
            return new Tuple<bool, User?>(false, null);
        }

        public async Task<bool> RequestResetPassword(ResetPasswordRequest request)
        {
            try
            {
                User? user = await _repository.GetByEmail(request.Email);
                if (user == null) throw new KeyNotFoundException($"Cuenta con el email {request.Email} no encontrada.");
                string token = GenerateVerificationCode();
                TokenVerify tokenVerify = new TokenVerify
                {
                    UserEmail = user.Email,
                    Token = token,
                    CreatedAt = DateTime.UtcNow,
                    TokenType = TokenType.PasswordReset,
                    ExpirationDate = DateTime.UtcNow.AddDays(7)
                };
                await _tokenVerifyRepository.CreateToken(tokenVerify);
                await _emailService.SendPasswordResetEmail(user, token);
                return true;
            }
            catch (KeyNotFoundException) { throw; }
            catch (InvalidOperationException) { throw; }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred: {ex.Message}", ex);
            }
        }

        public async Task<bool> ResetPassword(string token)
        {
            try 
            {
                TokenVerify tokenVerify = await _tokenVerifyRepository.GetByToken(token, TokenType.PasswordReset);
                if (tokenVerify == null || tokenVerify.ExpirationDate < DateTime.UtcNow) throw new SecurityTokenException("Invalid token or expired");
                User? user = await _repository.GetByEmail(tokenVerify.UserEmail);
                if (user == null) throw new KeyNotFoundException($"User not found");
                string newPassword = GenerateRandomPassword();
                user.Password = newPassword;
                await _repository.Update(user);
                await _tokenVerifyRepository.Delete(tokenVerify);
                await _emailService.SendNewPasswordEmail(user, newPassword);
                return true;
            }
            catch (SecurityTokenException) { throw; }
            catch (KeyNotFoundException) { throw; }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred: {ex.Message}", ex);
            }
        }

        private string GenerateVerificationCode() 
        { 
            using (var rng = RandomNumberGenerator.Create())
            {
                var tokenData = new byte[32];
                rng.GetBytes(tokenData);
                return Convert.ToBase64String(tokenData);
            }
        }

        private string GenerateRandomPassword(int length = 10)
        {
            const string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()?_-0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(validChars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        //public Tuple<bool, User?> Validate(string email, string password)
        //{
        //    User? userForLogin = _repository.GetByEmail(email);
        //    if (userForLogin != null)
        //    {
        //        if (userForLogin.Password == password)
        //        {
        //            return new Tuple<bool, User?>(true, userForLogin);
        //        }
        //        return new Tuple<bool, User?>(false, userForLogin);                METODO ALTERNATIVO SIN USER MODEL, USANDO TUPLAS
        //    }
        //    return new Tuple<bool, User?>(false, null);
        //} 
    }
}
