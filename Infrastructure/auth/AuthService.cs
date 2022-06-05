using Domain.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PrizeBondChecker.Domain;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PrizeBondChecker.Domain.Constants;
using Domain.Exceptions;
using Infrastructure.Repository.Base;

namespace Infrastructure.auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IRepository<Users> _usersRepository;
        public AuthService(IConfiguration Configuration, SignInManager<Users> signInManager, UserManager<Users> userManager, RoleManager<ApplicationRole> roleManager, IRepository<Users> usersRepository)
        {
            _configuration = Configuration;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _usersRepository = usersRepository;
        }

        public async Task<LoginResponse> LoginAsync(Login request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        //new Claim(JwtRegisteredClaimNames.NameIdentifier, user.Id),
                        new Claim(JwtRegisteredClaimNames.Jti,user.Id.ToString()),
                    };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["Tokens:Issuer"],
                    audience: _configuration["Tokens:Audience"],
                    expires: DateTime.Now.AddHours(Convert.ToInt32(_configuration["Tokens:ValidityInHours"])),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
                LoginResponse response = new LoginResponse()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo
                };

                return response;
            }
            else
            {
                throw new AuthException(ApplicationMessages.InvalidUserNameOrPassword);
            }
            //return null;
        }

        public async Task<RegisterResponse> RegisterAsync(Register request)
        {
            RegisterResponse response = new RegisterResponse();
            var userExists = await _userManager.FindByNameAsync(request.Username);
            if (userExists != null)
            {
                response.Status = "Error";
                response.Message = "User already exists!";
                return response;
            }
            Users user = new()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.Username,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                
            };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                string store = " ";
                foreach (var error in result.Errors)
                {
                    store += error.Description;
                }
                response.Status = "Error";
                response.Message = $"User creation Failed -{store}";
                return response;
            }

            else
            {
                response.Status = "Success";
                response.Message = "User created successfully!";
                return response;
            }
        }

        public async Task<List<Users>> GetAllUsers()
        {
            return await _usersRepository.GetAllAsync();
        }

    }
}
