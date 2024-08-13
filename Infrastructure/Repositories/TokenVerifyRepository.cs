using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TokenVerifyRepository : BaseRepository<TokenVerify>, ITokenVerifyRepository
    {
        private readonly ApplicationContext _context;

        public TokenVerifyRepository(ApplicationContext context) : base(context) 
        { 
            _context = context; 
        }

        public async Task<TokenVerify> GetByToken(string token, TokenType type)
        {
            try
            {
                TokenVerify? tokenVerifyEmail = await _context.TokenVerifies.FirstOrDefaultAsync(t => t.Token == token && t.TokenType == type);
                return tokenVerifyEmail;
            }
            catch
            {
                throw;
            }
        }
        public async Task<TokenVerify> CreateToken(TokenVerify token)
        {
            try
            {
                _context.TokenVerifies.Add(token);
                await _context.SaveChangesAsync();
                return token;
            }
            catch
            {
                throw;
            }
        }
    }
}
