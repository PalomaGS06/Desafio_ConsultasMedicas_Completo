using ConsultaMedicaVet.Interfaces;
using ConsultaMedicaVet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ConsultaMedicaVet.Controllers
{
    // rota da API
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaController : ControllerBase
    {
        // Injeção de dependência do repositório
        private readonly IConsultaRepository repositorio;

        public ConsultaController(IConsultaRepository _repositorio) //criando um construtor
        {
            repositorio = _repositorio;
        }

        //verbo POST - Inserir/Cadastrar

        /// <summary>
        /// Cadastra/Inclui consulta e seus respectivos Ids
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        ///
        ///   * Usuários: Administrador, Médico e Paciente
        ///       
        /// </remarks>    
        /// <param name="consulta"> Dados das Consultas</param>
        /// <returns>Consulta cadastrada!</returns>
        [Authorize(Roles = "Administrador, Medico, Paciente")]
        [HttpPost]
        public IActionResult Cadastrar(Consulta consulta)
        {

            try
            {
                var retorno = repositorio.Inserir(consulta); // retorno dos dados inseridos
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
        /// Lista/Busca todos as consultas existentes no BD
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        ///
        ///   * Usuários: Administrador, Médico e Paciente
        ///       
        /// </remarks>    
        /// <returns>Lista de consultas</returns>
        [Authorize(Roles = "Administrador, Medico, Paciente")]
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var retorno = repositorio.ListarTodas();  // retorna a lista de todas consultas
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
        /// Lista a consulta por meio de seu Id
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        ///
        ///   * Usuários: Administrador, Médico e Paciente
        ///       
        /// </remarks>    
        /// <param name="id">Dados da consulta selecionada</param>
        /// <returns>Consulta listada pelo ID</returns>
        [Authorize(Roles = "Administrador, Medico, Paciente")]
        [HttpGet("{id}")]
        public IActionResult BuscarConsultaPorID(int id)  
        {
            try
            {
                var retorno = repositorio.BuscarPorId(id);  // retorna a lista dos dados da consulta buscada pelo Id
                if (retorno == null)  // caso o id for igual a 0 
                {
                    return NotFound(new
                    {
                        Message = "Consulta não achada na lista !!"  // mensagem de erro
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

        //verbo PUT - Alterar/Atualizar

        /// <summary>
        /// Altera os dados da consulta
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        ///
        ///   * Usuários: Administrador e Médico 
        ///       
        /// </remarks>   
        /// <param name="id">Id da consulta </param>
        /// <param name="consulta">Dados da consulta alterada</param>
        /// <returns>Consulta alterada</returns>
        [Authorize(Roles = "Administrador, Medico")]
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Consulta consulta)
        {
            try
            {

                //Verificar se os ids batem!
                if (id != consulta.Id)   // caso o id for diferente de um Id existente 
                {
                    return BadRequest();
                }

                //Verificar se o id existe no banco!
                var retorno = repositorio.BuscarPorId(id);
                if (retorno == null)   // caso o id for igual a 0 
                {
                    return NotFound(new
                    {
                        Message = "Consulta não encontrada !!"  // mensagem de erro
                    });
                }

                //Altera efetivamente a consulta!
                repositorio.Alterar(consulta);

                return NoContent();  // caso contrário, o código 404 de sucesso será exibido

            }
            catch (System.Exception e)
            {
                return StatusCode(500, new
                {
                    Error = "Falha na transação !!", // mensagem de erro
                    Message = e.Message,
                });
            }

        }

        //verbo PATCH - Alterar parcialmente

        /// <summary>
        /// Altera alguns dos dados da consulta
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        ///
        ///   * Usuários: Administrador e Médico
        ///       
        /// </remarks>    
        /// <param name="id">Id selecionado para alteração</param>
        /// <param name="patchConsulta">Dado alterado</param>
        /// <returns>Consulta alterada</returns>
        [Authorize(Roles = "Administrador, Medico")]
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument patchConsulta)
        {
            if (patchConsulta == null)
            {
                return BadRequest();     // resposta de erro padrão
            }

            // Temos que buscar o objeto
            var consulta = repositorio.BuscarPorId(id);  //consulta encontrada
            if (consulta == null)
            {
                return NotFound(new
                {
                    Message = "Consulta não encontrada !!"
                });
            }

            //Altera parcialmente a consulta!
            repositorio.AlterarParcialmente(patchConsulta, consulta);
            return Ok(consulta); // retorna a consulta alterada
        }

        //verbo DELETE - Excluir

        /// <summary>
        /// Deletar consulta através de seu Id
        /// </summary>
        /// <remarks>
        /// 
        /// Acesso permitido:
        ///
        ///   * Usuários: Administrador e Médico 
        ///       
        /// </remarks>   
        /// <param name="id">Id selecionado para exclusão</param>
        /// <returns>Mensagem de exclusão</returns>
        [Authorize(Roles = "Administrador, Medico")]
        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                var busca = repositorio.BuscarPorId(id);     //deleta uma consulta digitando o id dela
                if (busca == null)
                {
                    return NotFound(new
                    {
                        Message = "Consulta não encontrada !!"  // mensagem de erro
                    });
                }

                repositorio.Excluir(busca);

                return NoContent(); // Sucesso

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
    }
}
