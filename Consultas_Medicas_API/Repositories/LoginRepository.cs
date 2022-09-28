using APIConsultasMedicas.Interfaces;
using APIConsultasMedicas.Models;
using ConsultaMedicaVet.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace APIConsultasMedicas.Repositories
{
    public class LoginRepository : ILoginRepository
    {

        private readonly ConsultaMedVetContext _context;
        public LoginRepository(ConsultaMedVetContext ctx)
        {
            _context = ctx ;
        }

        public string Logar(Logar login)
        {
            var usuario = _context.Usuarios
                 .Where(u => u.Email == login.Email)
                 .Include(t => t.TipoUsuario)
                 .FirstOrDefault();

            if (usuario != null && login.Senha != null && usuario.Senha.Contains("$2b$")) // Condição para a validação da senha
            {
                bool validada = BCrypt.Net.BCrypt.Verify(login.Senha, usuario.Senha); // Faz a verificação do que foi salvo. Método aplicado ao logar
               
            }

            return null;

        }

    }     
}
