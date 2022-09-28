using ConsultaMedicaVet.Interfaces;
using ConsultaMedicaVet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ConsultaMedicaVet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        // Injeção de dependência do repositório
        private readonly IPacienteRepository repositorio;

        public PacienteController(IPacienteRepository _repositorio) //criando um construtor
        {
            repositorio = _repositorio;
        }

        //verbo POST - Inserir/Cadastrar

        /// <summary>
        /// Cadastra/Inclui pacientes e seus respectivos Ids
        /// </summary>
        /// <param name="paciente"> Dados dos Pacientes</param>
        /// <returns>Paciente cadastrado!</returns>
        [HttpPost]
        public IActionResult Cadastrar(Paciente paciente)
        {

            try
            {
                paciente.Usuario.Senha = BCrypt.Net.BCrypt.HashPassword(paciente.Usuario.Senha);
                paciente.Usuario.IdTipoUsuario = 2;  // O paciente sempre será com o Id 2, não importando qual valor o usuario digitar
                var retorno = repositorio.Inserir(paciente);
                return Ok(retorno);

            }
            catch (System.Exception e)
            {
       
                return StatusCode(500, new
                {
                    Error = "IdTipoUsuario deve ser igual a 2!",  // mensagem de erro
                    Message = e.Message,
                });
            }

        }

        //verbo GET - Buscar/Listar

        /// <summary>
        /// Lista/Busca todos os pacientes existentes no BD
        /// </summary>
        /// <returns>Lista de Pacientes com consultas</returns>
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
                    Error = "Falha de transação!",  // mensagem de erro
                    Message = e.Message,
                });

            }

        }

        //verbo GET - Buscar/Listar por ID

        /// <summary>
        /// Lista o paciente por meio de seu Id
        /// </summary>
        /// <param name="id">Dados do paciente selecionado</param>
        /// <returns>Paciente listado pelo ID</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarPacientePorID(int id)
        {
            try
            {
                var retorno = repositorio.BuscarPorId(id);
                if (retorno == null)
                {
                    return NotFound(new
                    {
                        Message = "Paciente não achado na lista!"  // mensagem de erro
                    });
                }

                return Ok(retorno);

            }
            catch (System.Exception e)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação!",  // mensagem de erro
                    Message = e.Message,
                });
            }
        }

        //verbo PUT - Alterar/Atualizar

        /// <summary>
        /// Altera os dados do Paciente
        /// </summary>
        /// <param name="id">Id do Paciente </param>
        /// <param name="paciente">Dados do paciente alterado</param>
        /// <returns>Paciente alterado</returns>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Paciente paciente)
        {
            try
            {

                //Verificar se os ids batem!
                if (id != paciente.Id)
                {
                    return BadRequest();
                }
                               
                //Verificar se o id existe no banco!
                var retorno = repositorio.BuscarPorId(id);
                if (retorno == null)
                {
                    return NotFound(new
                    {
                        Message = "Paciente não encontrado!"  // mensagem de erro
                    });
                }

                paciente.Usuario.Senha = BCrypt.Net.BCrypt.HashPassword(paciente.Usuario.Senha);
                //Altera efetivamente o paciente!
                repositorio.Alterar(paciente);

                return Ok(new
                {
                    msg = "Paciente alterado com sucesso!", //mensagem de sucesso
                    paciente
                });

                // return NoContent();  //retorna o código 404 de sucesso que será exibido

            }
            catch (System.Exception e)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação!",  // mensagem de erro
                    Message = e.Message,
                });
            }

        }

        //verbo PATCH - Alterar parcialmente

        /// <summary>
        /// Altera alguns dos dados do Paciente
        /// </summary>
        /// <param name="id">Id selecionado para alteração</param>
        /// <param name="patchPaciente">Dado alterado</param>
        /// <returns>Paciente alterado</returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument patchPaciente)
        {
            if (patchPaciente == null)
            {
                return BadRequest();  // mensagem de erro padrão
            }

            // Temos que buscar o objeto
            var paciente = repositorio.BuscarPorId(id); //paciente encontrado
            if (paciente == null)
            {
                return NotFound(new
                {
                    Message = "Paciente não encontrado!"  // mensagem de erro
                });
            }

            repositorio.AlterarParcialmente(patchPaciente, paciente);

            return Ok(new
            {
                msg = "Paciente alterado com sucesso!", //mensagem de sucesso
                paciente
            });
        }

        //verbo DELETE - Excluir

        /// <summary>
        /// Deletar paciente através de seu Id
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
                        Message = "Paciente não encontrado!"  // mensagem de erro
                    });
                }

                repositorio.Excluir(busca);

                return NoContent(); // Status 204 de sucesso

            }
            catch (System.Exception e)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação!",  // mensagem de erro
                    Message = e.Message,
                });
            }

        }
    }
}
