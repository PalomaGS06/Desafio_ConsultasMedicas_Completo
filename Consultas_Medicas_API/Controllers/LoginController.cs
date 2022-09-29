using APIConsultasMedicas.Interfaces;
using APIConsultasMedicas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIConsultasMedicas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly ILoginRepository _loginRepository;

        public LoginController(ILoginRepository loginRepository) //Método construtor criado
        {
            _loginRepository = loginRepository;
        }


        [HttpPost]
        public IActionResult Logar(Login login)
        {
            var logar = _loginRepository.Logar(login);
            if (logar == null)                              // Se a ação de logar/autenticar for igual à nulo, o retorno será da não permissão do usuário
                return Unauthorized(new {
                    msg = "Usuário não permitido! Por favor, verifique novamente seus dados."   
                });

            return Ok (new
            { 
                token = logar  // returna com um título "token =" e o token criado
            });
        }
    }
}
