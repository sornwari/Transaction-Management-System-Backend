using Database;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service
{
    public class TransactionService
    {
        private readonly TMSContext _db;
        private readonly IConfiguration _configuration;
        public TransactionService(TMSContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

       
    }
}
