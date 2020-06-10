using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Med_App_API.Helper;
using Med_App_API.Models;
using Med_App_API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Med_App_API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        private readonly IMailService _mailService;

        public AuthRepository(UserManager<User> userManager, IConfiguration config, IMailService mailService)
        {
            _userManager = userManager;
            _config = config;
            _mailService = mailService;
        }

        public string GenerateUserName(string firstName, string lastName) =>
            firstName.ToLower().Substring(0, 1) + lastName.ToLower().Substring(0, 4);


        public async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }


        public async Task<UserManagerResponse> GeneratePasswordEmail(User user)
        {
            var emailToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            if (!string.IsNullOrEmpty(emailToken))
            {
                var encodedToken = Encoding.UTF8.GetBytes(emailToken);

                var validToken = WebEncoders.Base64UrlEncode(encodedToken);

                string url = $"{_config["APIUrl"]}/api/auth/email/verify/password?=userid{user.Id}&token={validToken}";

                await _mailService.SendEmailAsync(user.Email, "Forgoten password",
                    "<h1>You requested password reset for your account</h1>" +
                    $"<p>To change old password for a new, please click this <a href='{url}'>link</a></p>" +
                    "<br>" +
                    "<p>If you did not requested changing password, please ignore this email</p>"
                );
                return new UserManagerResponse
                {
                    Message = "Succesfully generated email token",
                    IsSuccess = true
                };
            }

            return new UserManagerResponse
            {
                IsSuccess = false,
                Message = "Failed at generating email token",
            };
        }

        public async Task<UserManagerResponse> GenerateConfirmEmail(User user)
        {
            var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            if (!string.IsNullOrEmpty(emailToken))
            {
                var encodedToken = Encoding.UTF8.GetBytes(emailToken);

                var validToken = WebEncoders.Base64UrlEncode(encodedToken);

                string url = $"{_config["APIUrl"]}/api/auth/email/verify/account?userid={user.Id}&token={validToken}";

                await _mailService.SendEmailAsync(user.Email, "Confirm your email",
                    "<h1>Thank you for registering to the med app</h1>" +
                    $"<p>Please confirm your email by this <a href='{url}'>link</a></p>");
                return new UserManagerResponse
                {
                    Message = "Succesfully generated email token",
                    IsSuccess = true
                };
            }

            return new UserManagerResponse
            {
                IsSuccess = false,
                Message = "Failed at generating email token",
            };
        }

        public async Task<UserManagerResponse> ConfirmPasswordAsync(string id, string token)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return new UserManagerResponse
                {
                    Message = "User not found",
                    IsSuccess = false
                };
            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            // var result = await _userManager.(user, normalToken);

            // if (result.Succeeded)
            //     return new UserManagerResponse
            //     {
            //         Message = "Email confirmed successfully!",
            //         IsSuccess = true,
            //     };

            return new UserManagerResponse
            {
                IsSuccess = false,
                Message = "Email did not confirm",
                // Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task<UserManagerResponse> ConfirmEmailAsync(string id, string token)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return new UserManagerResponse
                {
                    Message = "User not found",
                    IsSuccess = false
                };
            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)
                return new UserManagerResponse
                {
                    Message = "Email confirmed successfully!",
                    IsSuccess = true,
                };

            return new UserManagerResponse
            {
                IsSuccess = false,
                Message = "Email did not confirm",
                Errors = result.Errors.Select(e => e.Description)
            };
        }
    }
}