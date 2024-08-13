using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITokenVerifyRepository : IBaseRepository<TokenVerify>
    {
        Task<TokenVerify> GetByToken(string token, TokenType type);
        Task<TokenVerify> CreateToken(TokenVerify token);
    }
}
