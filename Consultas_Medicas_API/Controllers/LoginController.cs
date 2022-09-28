using APIConsultasMedicas.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIConsultasMedicas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly ILoginRepository repo;

        public LoginController(ILoginRepository _loginRepository)
        {
            repo = _loginRepository;
        }


        [HttpPost]
        public IActionResult Logar(string email, string senha)
        {
            var logar = repo.Logar(email, senha);
            if (logar == null)
                return Unauthorized();

            return Ok (new
            { 
                token = logar
            });
        }
    }
}
