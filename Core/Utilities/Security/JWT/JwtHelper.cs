using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Ancryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }  //IConfiguration wepApıdaki appsettingteki değerleri okumamıza yarar.
        private TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;          //accessToken nezaman geçersizleşecek
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>(); //appsetting içindeki TokenOptions bölümüne git ordaki değerleri TokenOptions clasındaki değerler ile mapla
        }
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);  //Bu token nezaman bitecek. Şuandan itibaren dakika ekle _tokenOptionstaki süre bunuda Configurationdan alıyor.
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);//securityKey değerinede git _tokenOptiondaki(appsettingteki) SecurityKey'i kullanarak bizim yazdığımız CreateSecuirtyKey ile securityKey formtaında vermemize yarıyor.
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);//Hangi anahtarı hangi algoritmayı kullanacaktı onuda belirttik.
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);//En son olarak  karşımıza jason web token üretimi çıkacak.Bir JWT de olması gereken şeyleri parantez içerisinde yazıyoruz.
                                                                                                       //Bunlar _tokenOptions bilgileri, hangi user içinse onun bilgisi,
                                                                                                       //neyi kullanarak yapacağımızı signing ile ve bu kişinin claimlerini de operationClaims ile ver diyoruz.
            //CreateJwtSecurityToken bizim aşağıda oluşturduğumuz bir metot
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };

        }

        //Verdiğim tokenOptions'lar, user bilgisi, signingCredentials bilgisi ve operationClaims'leri bilgisini vererek ben bir tane JWT oluşturuyorum.
        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            //oluşturduğum JWT'ye benden istediği ilgili bilgileri burada tek tek veriyorum.
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now, //notBefore bilgisi demek şuandan önceki bir değer verilemez demek.
                claims: SetClaims(user, operationClaims),//kullanıcının claimlerini oluştururken yine yardımcı bir metot oluşturduk.
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        //Kullanıcının(User) kullanıcı bilgilerini ve claimlerini alarak bana bir tane claim listesi oluştur. IEnumarable List'in Base'dir. Buraya List<Claim> de diyebilirdik.
        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            //Burada ilgili claim bilgilerini aşağıdaki yardımcı elemanları kullanarak oluşturuyoruz.
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());

            return claims;
        }
    }
}
