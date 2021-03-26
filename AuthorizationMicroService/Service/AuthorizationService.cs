using AuthorizationMicroService.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationMicroService.Service
{
    public class AuthorizationService : IAuthorizatonService
    {
        public readonly AppSetting _appSetting;
        public AuthorizationService(IOptions<AppSetting> appsettings)
        {
            _appSetting = appsettings.Value;
        }
        private List<Credentials> credentials = new List<Credentials>()
        {
            new Credentials{LoginId=50505, Password="qwert"},
            new Credentials{LoginId=60606, Password="qwert"},
            new Credentials{LoginId=70707, Password="qwert"},
            new Credentials{LoginId=80808, Password="qwert"},
            new Credentials{LoginId=90909, Password="qwert"},
        };
        public string Authorization(int loginId, string password)
        {
            var credential = credentials.SingleOrDefault(x => x.LoginId == loginId && x.Password == password);

            //Return null if user is not found
            if (credential == null)
                return null;
            var tokenHandeler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSetting.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,credential.LoginId.ToString()),
                    new Claim(ClaimTypes.Role,"Admin"),
                    new Claim(ClaimTypes.Version,"V3.1")
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandeler.CreateToken(tokenDescriptor);
            credential.Token = tokenHandeler.WriteToken(token);

            credential.Password = null;
            return credential.Token;
        }
    }
}
