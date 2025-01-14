﻿using JWTAuthManager.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTAuthManager
{
    public class JwtTokenHandler
    {
        public const string JWT_SECURITY_KEY = "XPAUHj5Okj0QVnQGgdURwZK7Kip0VnqHaTQgRcJd4mA=";
        private const int JWT_TOKEN_VALIDITY_MINS = 360;
        private readonly List<UserAccount> _userAccountList;
        public JwtTokenHandler()
        {
            _userAccountList =
            [
                new() {UserName = "admin", Password = "admin123", Role= "Administrator"},
                new() {UserName = "user01", Password = "user01", Role= "User"},
            ];
        }
        public AuthenticationResponse? GenerateJwtToken(AuthenticationRequest authenticationRequest)
        {
            if (string.IsNullOrWhiteSpace(authenticationRequest.UserName) || string.IsNullOrWhiteSpace(authenticationRequest.Password))
                return null;

            //validacion
            var userAccount = _userAccountList.Where(x => x.UserName == authenticationRequest.UserName && x.Password == authenticationRequest.Password).FirstOrDefault();
            if (userAccount == null) return null;

            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
            var claimsIdentity = new ClaimsIdentity(
                new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Name, authenticationRequest.UserName),
                    new Claim(ClaimTypes.Role, userAccount.Role),
                }
            );
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature
                );
            var securityTokenDescriptor = new SecurityTokenDescriptor { 
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signingCredentials
            };
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return new AuthenticationResponse
            {
                UserName = userAccount.UserName,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                JwtToken = token,
            };
        }
    }
}
