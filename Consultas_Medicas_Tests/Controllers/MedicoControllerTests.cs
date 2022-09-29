using ConsultaMedicaVet.Controllers;
using ConsultaMedicaVet.Interfaces;
using ConsultaMedicaVet.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Consultas_Medicas_Tests.Controllers
{
    public class MedicoControllerTests
    {
        // Preparação  
        private readonly Mock<IMedicoRepository> _mock; // A função Mock cria um repositório "fake" para usá-lo no controller
        private readonly MedicoController _ctl;

        public MedicoControllerTests()  // Gerando um método construtor
        {
            _mock = new Mock<IMedicoRepository>();
            _ctl = new MedicoController(_mock.Object);
        }
        /// <summary>
        /// Esse teste precisa retornar o método com um resultado de Ok
        /// </summary>
        [Fact]  // Determina o que é um teste
        public void TestActionResultReturnOkBuscas()
        {

            // Execução
            var result = _ctl.Listar(); // Ele busca o método GET criado no Controller verdadeiro
            // Retorno
            Assert.IsType<OkObjectResult>(result);
        }
        /// <summary>
        /// Esse teste precisa retornar o método, mas com o status de code de sucesso = 200
        /// </summary>
        [Fact]
        public void TestStatusCodeSuccessBuscas()
        {
            // Execução - Act
            var actionResult = _ctl.Listar();
            var result = actionResult as OkObjectResult;
            // Retorno
            Assert.Equal(200, result.StatusCode); // Status Code 200
        }
        /// <summary>
        /// Esse teste precisa retornar o método com um resultado de Ok
        /// </summary>
        [Fact]
        public void TestInserirBusca()
        {
            var result = _ctl.Cadastrar(new() // Método PUT do Controller verdadeiro
            {
                CRM = "741963852",
                IdUsuario = 10,
                IdEspecialidade = 1,
                Usuario = new Usuario
                {
                    Nome = "Medico",
                    Email = "medicoteste@testando.com",
                    Senha = "987654abc",
                    IdTipoUsuario = 1,
                }
            });
            Assert.IsType<OkObjectResult>(result);
        }
        /// <summary>
        /// Esse teste precisa retornar um resultado não nulo
        /// </summary>
        [Fact]
        public void TestActionResultBuscaNotNull()
        {
            // Execução 
            var actionResult = _ctl.Listar();
            // Retorno
            Assert.NotNull(actionResult);
        }
    }
}
