
using Domain.User;
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
    }
}
