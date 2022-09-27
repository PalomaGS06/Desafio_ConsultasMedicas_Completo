using ConsultaMedicaVet.Interfaces;
using ConsultaMedicaVet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ConsultaMedicaVet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {
        // Injeção de dependência do repositório
        private readonly ITipoUsuarioRepository repositorio;

        public TipoUsuarioController(ITipoUsuarioRepository _repositorio) //criando um construtor
        {
            repositorio = _repositorio;
        }

        //verbo GET - Buscar/Listar

        /// <summary>
        /// Lista/Busca todos os tipos de usuários existentes no BD
        /// </summary>
        /// <returns>Lista de tipos de usuários</returns>
        
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var retorno = repositorio.ListarTodos();
                return Ok(retorno);

            }
            catch (System.Exception e)
            {
                return StatusCode(500, new
                {
                    Error = "Falha no servidor!!",  // mensagem de erro
                    Message = e.Message,
                });

            }

        }

        //verbo GET - Buscar/Listar por ID

        /// <summary>
        /// Lista o tipo de usuário por meio de seu Id
        /// </summary>
        /// <param name="id">Dados do tipo de usuário selecionado</param>
        /// <returns>Tipo de usuário listado pelo ID</returns>
        
        [HttpGet("{id}")]
        public IActionResult BuscarTipoUsuarioPorID(int id)
        {
            try
            {
                var retorno = repositorio.BuscarPorId(id);
                if (retorno == null)
                {
                    return NotFound(new
                    {
                        Message = "Tipo de usuario não encontrado na lista!!"  // mensagem de erro
                    });
                }

                return Ok(retorno);

            }
            catch (System.Exception e)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação!!",  // mensagem de erro
                    Message = e.Message,
                });
            }
        }

        //verbo PUT - Alterar/Atualizar

        /// <summary>
        /// Altera o tipo de usuário
        /// </summary>
        /// <param name="id">Id do tipo de usuário </param>
        /// <param name="tipoUsuario">Tipo de usuário alterado</param>
        /// <returns>Tipo de usuário alterado</returns>
         
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, TipoUsuario tipoUsuario)
        {
            try
            {

                //Verificar se os ids batem!
                if (id != tipoUsuario.Id)
                {
                    return BadRequest(); // erro padrão de resposta
                }

                //Verificar se o id existe no banco!
                var retorno = repositorio.BuscarPorId(id);
                if (retorno == null)
                {
                    return NotFound(new
                    {
                        Message = "Tipo de usuario não encontrado!!"  // mensagem de erro
                    });
                }

                //Altera efetivamente o tipo!
                repositorio.Alterar(tipoUsuario);

                return NoContent();

            }
            catch (System.Exception e)
            {
                return StatusCode(500, new
                {
                    Error = "Falha no servidor!!",  // mensagem de erro
                    Message = e.Message,
                });
            }

        }
       
    }
}
