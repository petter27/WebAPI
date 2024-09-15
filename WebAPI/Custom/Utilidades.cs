using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebAPI.Models;


namespace WebAPI.Custom
{
    public class Utilidades
    {
        private readonly IConfiguration _configuration;
        public Utilidades(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string encriptarSHA256(string texto)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // computar el hash - retorna un array de bytes
                byte[] byter = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(texto));

                // convertir el array de bytes a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < byter.Length; i++)
                {
                    builder.Append(byter[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public string generarJWT(Usuario _usuario)
        {
            var userClaims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, _usuario.IdUsuario.ToString()),
            new Claim(ClaimTypes.Email, _usuario.Correo!)
        };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // crear detalle del token 
            var jwtConfig = new JwtSecurityToken(
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
        }
    }

}
