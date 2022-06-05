
using Domain.User;
using PrizeBondChecker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.auth
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(Login request);
        Task<RegisterResponse> RegisterAsync(Register request);
        Task<List<Users>> GetAllUsers();
        Task<Users> GetUserById(Guid userId);
    }
}
