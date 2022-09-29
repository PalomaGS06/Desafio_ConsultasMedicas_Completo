using ConsultaMedicaVet.Interfaces;
using ConsultaMedicaVet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ConsultaMedicaVet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        // Injeção de dependência do repositório
        private readonly IMedicoRepository repositorio;

        public MedicoController(IMedicoRepository _repositorio) //criando um construtor
        {
            repositorio = _repositorio;
        }

        //verbo POST - Inserir/Cadastrar

        /// <summary>
        /// Cadastra/Inclui médicos e seus respectivos Ids
        /// </summary>
        /// <param name="medico"> Dados dos Médicos</param>
        /// <returns>Médico cadastrado!</returns>
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public IActionResult Cadastrar(Medico medico)
        {

            try
            {
                medico.Usuario.Senha = BCrypt.Net.BCrypt.HashPassword(medico.Usuario.Senha);
                medico.Usuario.IdTipoUsuario = 1;   // O médico sempre será com o Id 1, não importando qual valor o usuario digitar
                var retorno = repositorio.Inserir(medico);
                return Ok(retorno);

            }
            catch (System.Exception e)
            {        
                return StatusCode(500, new
                {
                    Error = "Falha na transação...",   // mensagem de erro
                    Message = e.Message,
                });
            }

        }

        //verbo GET - Buscar/Listar

        /// <summary>
        /// Lista/Busca todos os médicos existentes no BD
        /// </summary>
        /// <returns>Lista de Médicos com consultas</returns>
        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var retorno = repositorio.ListarTodos();   // retorna a lista de todos medicos
                return Ok(retorno);

            }
            catch (System.Exception e)
            {
                return StatusCode(500, new
                {
                    Error = "Falha de transação...",   // mensagem de erro
                    Message = e.Message,
                });

            }

        }

        //verbo GET - Buscar/Listar por ID

        /// <summary>
        /// Lista o médico por meio de seu Id
        /// </summary>
        /// <param name="id">Dados do médico selecionado</param>
        /// <returns>Médico listado pelo ID</returns>
        [Authorize(Roles = "Administrador")]
        [HttpGet("{id}")]
        public IActionResult BuscarMedicoPorID(int id)
        {
            try
            {
                var retorno = repositorio.BuscarPorId(id);
                if (retorno == null)   // caso o id for igual a 0
                {
                    return NotFound(new
                    {
                        Message = "Médico não achado na lista..."   // mensagem de erro
                    });
                }

                return Ok(retorno);

            }
            catch (System.Exception e)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação...",   // mensagem de erro
                    Message = e.Message,
                });
            }
        }

        //verbo PUT - Alterar/Atualizar

        /// <summary>
        /// Altera os dados do médico
        /// </summary>
        /// <param name="id">Id do médico </param>
        /// <param name="medico">Dados do médico alterado</param>
        /// <returns>Médico alterado</returns>
        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Medico medico)
        {
            try
            {

                //Verificar se os ids batem!
                if (id != medico.Id)    // caso o id for diferente de um Id existente
                {
                    return BadRequest(new
                    {
                        message = "O id informado é diferente do id inserido no Json!"
                    });        // resposta de erro padrão
                }

                //Verificar se o id existe no banco!
                var retorno = repositorio.BuscarPorId(id);
                if (retorno == null)
                {
                    return NotFound(new
                    {
                        Message = "Médico não encontrado..."   // mensagem de erro
                    });
                }

                medico.Usuario.Senha = BCrypt.Net.BCrypt.HashPassword(medico.Usuario.Senha);
                //Altera efetivamente o médico!
                repositorio.Alterar(medico);

                return Ok(new
                {
                    msg = "Médico alterado com sucesso!", //mensagem de sucesso
                    medico
                });

            }
            catch (System.Exception e)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação...",   // mensagem de erro
                    Message = e.Message,
                });
            }

        }

        //verbo PATCH - Alterar parcialmente

        /// <summary>
        /// Altera alguns dos dados do médico
        /// </summary>
        /// <param name="id">Id selecionado para alteração</param>
        /// <param name="patchMedico">Dado alterado</param>
        /// <returns>Médico alterado</returns>
        [Authorize(Roles = "Administrador")]
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument patchMedico)
        {
            if (patchMedico == null)
            {
                return BadRequest();
            }

            // Temos que buscar o objeto
            var medico = repositorio.BuscarPorId(id); //médico encontrado
            if (medico == null)
            {
                return NotFound(new
                {
                    Message = "Médico não encontrado..."   // mensagem de erro
                });
            }

            repositorio.AlterarParcialmente(patchMedico, medico);

            return Ok(new       // retorna o medico alterado
            {
                msg = "Médico alterado com sucesso!", //mensagem de sucesso
                medico
            }); 
        }

        //verbo DELETE - Excluir

        /// <summary>
        /// Deletar médico através de seu Id
        /// </summary>
        /// <param name="id">Id selecionado para exclusão</param>
        /// <returns>Mensagem de exclusão</returns>
        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                var busca = repositorio.BuscarPorId(id);     //deleta um medico digitando o id dele
                if (busca == null)
                {
                    return NotFound(new
                    {
                        Message = "Médico não encontrado..."    // mensagem de erro
                    });
                }

                repositorio.Excluir(busca);

                return NoContent();     // Status 204 de sucesso

            }
            catch (System.Exception e)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação...",   // mensagem de erro
                    Message = e.Message,
                });
            }

        }
    }
}
