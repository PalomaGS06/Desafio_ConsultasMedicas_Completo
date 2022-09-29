using APIConsultasMedicas.Interfaces;
using APIConsultasMedicas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace APIConsultasMedicas.Controllers
{
    [Authorize(Roles = "Administrador")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdministradorController : ControllerBase
    {
        // Injeção de dependência do repositório
        private readonly IAdministradorRepository repositorio;

        public AdministradorController(IAdministradorRepository _repositorio) //criando um construtor
        {
            repositorio = _repositorio;
        }

        //verbo POST - Inserir/Cadastrar

        /// <summary>
        /// Cadastra/Inclui administradores e seus respectivos Ids
        /// </summary>
        /// <param name="admin"> Dados dos Administradores</param>
        /// <returns>Administrador cadastrado!</returns>
        [HttpPost]
        public IActionResult Cadastrar(Administrador admin)
        {

            try
            {
                admin.Usuario.Senha = BCrypt.Net.BCrypt.HashPassword(admin.Usuario.Senha);
                admin.Usuario.IdTipoUsuario = 4;   // O médico sempre será com o Id 4, não importando qual valor o usuario digitar
                var retorno = repositorio.Inserir(admin);
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
        /// Lista/Busca todos os administradores existentes no BD
        /// </summary>
        /// <returns>Lista de Administradores</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var retorno = repositorio.ListarTodosAdmin();   // retorna a lista de todos admins
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
        /// Lista o Admin por meio de seu Id
        /// </summary>
        /// <param name="id">Dados do administrador selecionado</param>
        /// <returns>Administrador listado pelo ID</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarAdminPorID(int id)
        {
            try
            {
                var retorno = repositorio.BuscarPorId(id);
                if (retorno == null)   // caso o id for igual a 0
                {
                    return NotFound(new
                    {
                        Message = "Administrador não achado na lista..."   // mensagem de erro
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
        /// Altera os dados do administrador
        /// </summary>
        /// <param name="id">Id do Administrador </param>
        /// <param name="admin">Dados do administrador alterado</param>
        /// <returns>Administrador alterado</returns>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Administrador admin)
        {
            try
            {

                //Verificar se os ids batem!
                if (id != admin.Id)    // caso o id for diferente de um Id existente
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
                        Message = "Administrador não encontrado..."   // mensagem de erro
                    });
                }

                admin.Usuario.Senha = BCrypt.Net.BCrypt.HashPassword(admin.Usuario.Senha);
                //Altera efetivamente o admin!
                repositorio.Alterar(admin);

                return Ok(new
                {
                    msg = "Administrador alterado com sucesso!", //mensagem de sucesso
                    admin
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
        /// Altera alguns dos dados do administrador
        /// </summary>
        /// <param name="id">Id selecionado para alteração</param>
        /// <param name="patchAdmin">Dado alterado</param>
        /// <returns>Administrador alterado</returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument patchAdmin)
        {
            if (patchAdmin == null)
            {
                return BadRequest();
            }

            // Temos que buscar o objeto
            var admin = repositorio.BuscarPorId(id); // Admin encontrado
            if (admin == null)
            {
                return NotFound(new
                {
                    Message = "Administrador não encontrado..."   // mensagem de erro
                });
            }

            repositorio.AlterarParcialmente(patchAdmin, admin);

            return Ok(new       // retorna o admin alterado
            {
                msg = "Administrador alterado com sucesso!", //mensagem de sucesso
                admin
            });
        }

        //verbo DELETE - Excluir

        /// <summary>
        /// Deletar administrador através de seu Id
        /// </summary>
        /// <param name="id">Id selecionado para exclusão</param>
        /// <returns>Mensagem de exclusão</returns>
        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                var busca = repositorio.BuscarPorId(id);     //deleta um admin digitando o id dele
                if (busca == null)
                {
                    return NotFound(new
                    {
                        Message = "Administrador não encontrado..."    // mensagem de erro
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
