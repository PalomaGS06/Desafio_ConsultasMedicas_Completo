using APIConsultasMedicas.Interfaces;
using APIConsultasMedicas.Models;
using ConsultaMedicaVet.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace APIConsultasMedicas.Repositories
{
    public class LoginRepository : ILoginRepository
    {

        private readonly ConsultaMedVetContext _context;
        public LoginRepository(ConsultaMedVetContext ctx)
        {
            _context = ctx ;
        }

        public string Logar(Login login) 
        {
            var usuario = _context.Usuarios
                 .Where(u => u.Email == login.Email)
                 .Include(t => t.TipoUsuario)  // O login inclui o ripo de usuário para ele identificar
                 .FirstOrDefault();

            if (usuario != null && login.Senha != null && usuario.Senha.Contains("$2b$")) // Condição para a validação da senha
            {
                bool validada = BCrypt.Net.BCrypt.Verify(login.Senha, usuario.Senha); // Faz a verificação do que foi salvo. Método aplicado ao logar
                                                                                      // Cria as credenciais do JWT

                // Realizando a definição da Claim
                var myClaims = new[]
                {
                        new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                        new Claim(ClaimTypes.Role, usuario.TipoUsuario.Tipo), // Inclui qual o tipo de usuario (administrador, médico ou paciente)
                                                                              // que possui autorização de acessos 

                        new Claim("Cargo", usuario.TipoUsuario.Tipo) // indica qual o cargo (tipo) de usuário

                    };

                // Chaves criadas através da Issuer que foi feita na configuração do JWT no Startup
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("Api-consultas-key"));

                // Credenciais criadas
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                // Token gerado através das informações passadas na configuração do JWT
                var myToken = new JwtSecurityToken(
                    issuer: "consultasMedicas.webAPI",  // local da emissão do token
                    audience: "consultasMedicas.webAPI",
                    claims: myClaims,
                    expires: DateTime.Now.AddMinutes(30), // tempo de expiração do token
                    signingCredentials: creds // assinatura das credenciais
                    );

                return new JwtSecurityTokenHandler().WriteToken(myToken);

            }
            
            return null;
        }

           
    }

 
}
