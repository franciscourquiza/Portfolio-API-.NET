using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TokenVerify
    {
        public int Id { get; set; }

        [ForeignKey("UserEmail")]
        public string UserEmail { get; set; }
        public string Token { get; set; }
        public TokenType TokenType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
    public enum TokenType 
    {
        EmailVerification,
        PasswordReset
    }
}
