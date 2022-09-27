using ConsultaMedicaVet.Interfaces;
using ConsultaMedicaVet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ConsultaMedicaVet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        // Injeção de dependência do repositório
        private readonly IUsuarioRepository repositorio;

        public UsuarioController(IUsuarioRepository _repositorio) //criando um construtor
        {
            repositorio = _repositorio;
        }

        //verbo POST - Inserir/Cadastrar

        /// <summary>
        /// Cadastra/Inclui usuarios e seus respectivos Ids
        /// </summary>
        /// <param name="usuario"> Dados dos Usuários</param>
        /// <returns>Usuário cadastrado!</returns>
        [HttpPost]
        public IActionResult Cadastrar(Usuario usuario)
        {

            try
            {
                var retorno = repositorio.Inserir(usuario);
                return Ok(retorno);

            }
            catch (System.Exception e)
            {
                // return BadRequest(e.Message);            
                return StatusCode(500, new
                {
                    Error = "Falha na transação !!",  // mensagem de erro
                    Message = e.Message,
                });
            }

        }

        //verbo GET - Buscar/Listar

        /// <summary>
        /// Lista/Busca todos os usuarios existentes no BD
        /// </summary>
        /// <returns>Lista de Usuários</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var retorno = repositorio.ListarTodosUsers();
                return Ok(retorno);

            }
            catch (System.Exception e)
            {
                return StatusCode(500, new
                {
                    Error = "Falha de transação !!",  // mensagem de erro
                    Message = e.Message,
                });

            }


        }

        //verbo GET - Buscar/Listar por ID

        /// <summary>
        /// Lista o usuario por meio de seu Id
        /// </summary>
        /// <param name="id">Dados do usuario selecionado</param>
        /// <returns>Usuário listado pelo ID</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarUsuarioPorID(int id)
        {
            try
            {
                var retorno = repositorio.BuscarPorId(id);
                if (retorno == null)
                {
                    return NotFound(new
                    {
                        Message = "Usuario não achado na lista !!"  // mensagem de erro
                    });
                }

                return Ok(retorno);

            }
            catch (System.Exception e)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação !!",   // mensagem de erro
                    Message = e.Message,
                });
            }
        }

        //verbo GET - Buscar/Listar por médico

        /// <summary>
        /// Lista o usuário médico
        /// </summary>
        /// <returns>Médico listado</returns>
        /// 
        [HttpGet("Medico")]
        public IActionResult ListarMedico()
        {
            try
            {
                var retorno = repositorio.ListarMedicosUsers();
                
                return Ok(retorno);

            }
            catch (System.Exception e)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação !!",    // mensagem de erro
                    Message = e.Message,
                });
            }
        }

        //verbo GET - Buscar/Listar por paciente

        /// <summary>
        /// Lista o usuário paciente 
        /// </summary>
        /// <returns>Paciente listado</returns>
        [HttpGet("Paciente")]
        public IActionResult ListarPaciente()
        {
            try
            {
                var retorno = repositorio.ListarPacientesUsers();

                return Ok(retorno);

            }
            catch (System.Exception e)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação !!",      // mensagem de erro
                    Message = e.Message,
                });
            }
        }

        //verbo PUT - Alterar/Atualizar

        /// <summary>
        /// Altera os dados do usuario
        /// </summary>
        /// <param name="id">Id do usuario </param>
        /// <param name="usuario">Dados do usuario alterado</param>
        /// <returns>Usuário alterado</returns>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Usuario usuario)
        {
            try
            {

                //Verificar se os ids batem!
                if (id != usuario.Id)
                {
                    return BadRequest();
                }

                //Verificar se o id existe no banco!
                var retorno = repositorio.BuscarPorId(id);
                if (retorno == null)
                {
                    return NotFound(new
                    {   
                        Message = "Usuario não encontrado !!"     // mensagem de erro
                    });
                }

                //Altera efetivamente o usuário!
                repositorio.Alterar(usuario);

                return NoContent();

            }
            catch (System.Exception e)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação !!",      // mensagem de erro
                    Message = e.Message,
                });
            }

        }

        //verbo PATCH - Alterar parcialmente

        /// <summary>
        /// Altera alguns dos dados do usuario
        /// </summary>
        /// <param name="id">Id selecionado para alteração</param>
        /// <param name="patchUsuario">Dado alterado</param>
        /// <returns>Usuário alterado</returns>

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument patchUsuario)
        {
            if (patchUsuario == null)
            {
                return BadRequest();
            }

            // Temos que buscar o objeto
            var usuario = repositorio.BuscarPorId(id); //usuario encontrado
            if (usuario == null)
            {
                return NotFound(new
                {
                    Message = "Usuário não encontrado !!"     // mensagem de erro
                });
            }

            repositorio.AlterarParcialmente(patchUsuario, usuario);
            return Ok(usuario);
        }

        //verbo DELETE - Excluir

        /// <summary>
        /// Deletar usuario através de seu Id
        /// </summary>
        /// <param name="id">Id selecionado para exclusão</param>
        /// <returns>Mensagem de exclusão</returns>
        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                var busca = repositorio.BuscarPorId(id);
                if (busca == null)
                {
                    return NotFound(new
                    {
                        Message = "Usuário não encontrado !!"     // mensagem de erro
                    });
                }

                repositorio.Excluir(busca);

                return NoContent(); // Status 204 de sucesso

            }
            catch (System.Exception e)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação!!",   // mensagem de erro
                    Message = e.Message,
                });
            }

        }
    }
}
