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
    public class EspecialidadeController : ControllerBase
    {
        // Injeção de dependência do repositório
        private readonly IEspecialidadeRepository repositorio;

        public EspecialidadeController(IEspecialidadeRepository _repositorio) //criando um construtor
        {
            repositorio = _repositorio;
        }

        //verbo POST - Inserir/Cadastrar

        /// <summary>
        /// Cadastra/Inclui especialidades e seus respectivos Ids
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        ///
        ///   * Usuários: Administrador, Médico e Paciente
        ///       
        /// </remarks>   
        /// <param name="especialidade"> Dados das Especialidades</param>
        /// <returns>Especialidade cadastrada!</returns>
        [Authorize(Roles = "Administrador, Medico, Paciente")]
        [HttpPost]
        public IActionResult Cadastrar(Especialidade especialidade)
        {

            try
            {
                var retorno = repositorio.Inserir(especialidade);  // retorno dos dados inseridos
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
        /// Lista/Busca todos as especialidades existentes no BD
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        ///
        ///   * Usuários: Administrador, Médico e Paciente
        ///       
        /// </remarks>    
        /// <returns>Lista de Especialidades </returns>
        [Authorize(Roles = "Administrador, Medico, Paciente")]
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var retorno = repositorio.ListarTodas();   // retorna a lista de todas consultas
                return Ok(retorno);

            }
            catch (System.Exception e)
            {
                return StatusCode(500, new
                {
                    Error = "Falha no sistema !!",  // mensagem de erro
                    Message = e.Message,
                });

            }

        }
        //verbo GET - Buscar/Listar por ID

        /// <summary>
        /// Lista a especialidade por meio de seu Id
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        ///
        ///   * Usuários: Administrador, Médico e Paciente
        ///       
        /// </remarks>   
        /// <param name="id">Dados da especialidade selecionada</param>
        /// <returns>Especialidade listada pelo ID</returns>
        [Authorize(Roles = "Administrador, Medico, Paciente")]
        [HttpGet("{id}")]
        public IActionResult BuscarEspecialidadePorID(int id)
        {
            try
            {
                var retorno = repositorio.BuscarPorId(id);  // retorna a lista dos dados da consulta buscada pelo Id
                if (retorno == null)    // caso o id for igual a 0 
                {
                    return NotFound(new
                    {
                        Message = "Especialidade não encontrada na lista !!" // mensagem de erro
                    });
                }

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

        //verbo PUT - Alterar/Atualizar

        /// <summary>
        /// Altera os dados da especialidade
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        ///
        ///   * Usuários: Administrador, Médico e Paciente
        ///       
        /// </remarks>   
        /// <param name="id">Id da especialidade </param>
        /// <param name="especialidade">Dados da especialidade alterada</param>
        /// <returns>Especialidade alterada</returns>
        [Authorize(Roles = "Administrador, Medico, Paciente")]
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Especialidade especialidade)
        {
            try
            {

                //Verificar se os ids batem!
                if (id != especialidade.Id)  // caso o id for diferente de um Id existente
                {
                    return BadRequest();
                }

                //Verificar se o id existe no banco!
                var retorno = repositorio.BuscarPorId(id);
                if (retorno == null)
                {
                    return NotFound(new
                    {
                        Message = "Especialidade não encontrada !!"    // mensagem de erro
                    });
                }

                //Altera efetivamente a especialidade!
                repositorio.Alterar(especialidade);

                return NoContent();  // caso contrário, o código 404 de sucesso será exibido

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

        //verbo PATCH - Alterar parcialmente

        /// <summary>
        /// Altera alguns dos dados da especialidade
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        ///
        ///   * Usuários: Administrador, Médico e Paciente
        ///       
        /// </remarks>   
        /// <param name="id">Id selecionado para alteração</param>
        /// <param name="patchEspecialidade">Dado alterado</param>
        /// <returns>Especialidade alterada</returns>
        [Authorize(Roles = "Administrador, Medico, Paciente")]
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument patchEspecialidade)
        {
            if (patchEspecialidade == null)
            {
                return BadRequest();    // resposta de erro padrão
            }

            // Temos que buscar o objeto
            var especialidade = repositorio.BuscarPorId(id); //especialidade encontrada
            if (especialidade == null)
            {
                return NotFound(new
                {
                    Message = "Especialidade não encontrada !!"    // mensagem de erro
                });
            }
            //Altera parcialmente a especialidade!
            repositorio.AlterarParcialmente(patchEspecialidade, especialidade);
            return Ok(especialidade);
        }

        //verbo DELETE - Excluir

        /// <summary>
        /// Deletar especialidade através de seu Id
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        ///
        ///   * Usuários: Administrador, Médico e Paciente
        ///       
        /// </remarks>    
        /// <param name="id">Id selecionado para exclusão</param>
        /// <returns>Mensagem de exclusão</returns>
        [Authorize(Roles = "Administrador, Medico, Paciente")]
        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                var busca = repositorio.BuscarPorId(id);     //deleta uma especialidade digitando o id dela
                if (busca == null)
                {
                    return NotFound(new
                    {
                        Message = "Especialidade não encontrada !!"    // mensagem de erro
                    });
                }

                repositorio.Excluir(busca);

                return NoContent(); // Status 204 de sucesso

            }
            catch (System.Exception e)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação!!",    // mensagem de erro
                    Message = e.Message,
                });
            }

        }

    }
}
