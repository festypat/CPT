using CPT.Helper.Configuration;
using CPT.Helper.ViewModel;
using CPT.Infrastructure.TokenService.Interface;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace CPT.Infrastructure.TokenService.Service
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly TokenConfig _tokenConfig;
        // private readonly JwtTokenLogger _jwtTokenLogger;
        public TokenGenerator(IOptions<TokenConfig> tokenConfig)
        {
            _tokenConfig = tokenConfig.Value;
            // _jwtTokenLogger = jwtTokenLogger ?? throw new ArgumentNullException(nameof(jwtTokenLogger));
        }
        public async Task<TokenGeneratorViewModel> GenerateToken(TokenUserDetailsViewModel tokenUserDetails)
        {
            try
            {

                var tokenHandler = new JwtSecurityTokenHandler();

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, tokenUserDetails.Username),
                        new Claim(ClaimTypes.Email, tokenUserDetails.Email),
                       // new Claim(ClaimTypes.Role, tokenUserDetails.Role == default ? "User" : tokenUserDetails.Role),
                        new Claim(nameof(ClaimswrapperViewModel.CustomerId),  Convert.ToString(tokenUserDetails.CustomerId == default ? 0 : tokenUserDetails.CustomerId)),
                        new Claim(nameof(ClaimswrapperViewModel.Fullname),  tokenUserDetails.Fullname == default ? string.Empty : tokenUserDetails.Fullname),
                    }),

                    //Expires = DateTime.UtcNow.AddHours(Convert.ToInt32(_tokenConfig.TokenExpiration)),
                    Expires = DateTime.Now.AddHours(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("874384$32380349$#YYYCX212834*193@")), SecurityAlgorithms.HmacSha256Signature)
                    //SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_tokenConfig.SecretKey)), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                // _jwtTokenLogger.LogActivities("JwtTokenLogger-Module", $"{"Token response is successful for user --"}{" - "}{clientId}{" - "}", false);

                return await Task.Run(() =>
                {
                    return new TokenGeneratorViewModel
                    {
                        ResponseCode = "00",
                        Accesstoken = tokenHandler.WriteToken(token),
                        Username = tokenUserDetails.Username,
                        Email = tokenUserDetails.Email,
                        TokenExpire = $"{Convert.ToString(_tokenConfig.TokenExpiration)}{" hour"}",
                        Message = "Success",
                        TokenType = _tokenConfig.TokenType,
                        Role = tokenUserDetails.Role,
                        CustomerId = tokenUserDetails.CustomerId
                    };
                });
            }
            catch (Exception ex)
            {
                // _jwtTokenLogger.LogActivities("JwtTokenLogger-Module", $"{"Error occoured while creating user token --"}{" - "}{clientId}{" - "}{ex}", true);

                return await Task.Run(() =>
                {
                    return new TokenGeneratorViewModel
                    {
                        ResponseCode = "03",
                        Username = tokenUserDetails.Username,
                        Message = ex.ToString(),
                    };
                });
            }

        }
    }
}
