using Domain.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        public AuthService(IConfiguration Configuration, UserManager<ApplicationUser> userManager)
        {
            _configuration = Configuration;
            //_signInManager = signInManager;
            _userManager = userManager;
            //_roleManager = roleManager;
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

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
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
            return null;
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
            ApplicationUser user = new ApplicationUser()
            {
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.Username,
                PhoneNumber = request.PhoneNumber
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
    }
}
