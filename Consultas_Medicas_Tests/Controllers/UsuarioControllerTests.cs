using ConsultaMedicaVet.Controllers;
using ConsultaMedicaVet.Interfaces;
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
    public class UsuarioControllerTests
    {
        // Preparação  
        private readonly Mock<IUsuarioRepository> _mock; // A função Mock cria um repositório "fake" para usá-lo no controller
        private readonly UsuarioController _ctl;

        public UsuarioControllerTests()  // Gerando um método construtor
        {
            _mock = new Mock<IUsuarioRepository>();
            _ctl = new UsuarioController(_mock.Object);
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
        public void TestStatusCodeSuccessUsers()
        {
            // Execução - Act
            var actionResult = _ctl.Listar();
            var result = actionResult as OkObjectResult;
            // Retorno
            Assert.Equal(200, result.StatusCode);  // Status Code 200
        }

        /// <summary>
        /// Esse teste precisa retornar o método Get de usuários Médicos, mas com o status de code de sucesso = 200
        /// </summary>
        [Fact]
        public void TestStatusCodeSuccessMedicos()
        {
            // Execução - Act
            var actionResult = _ctl.ListarMedico();
            var result = actionResult as OkObjectResult;
            // Retorno
            Assert.Equal(200, result.StatusCode);  // Status Code 200
        }

        /// <summary>
        /// Esse teste precisa retornar o método Get de usuários Pacientes, mas com o status de code de sucesso = 200
        /// </summary>
        [Fact]
        public void TestStatusCodeSuccessPaciente()
        {
            // Execução - Act
            var actionResult = _ctl.ListarPaciente();
            var result = actionResult as OkObjectResult;
            // Retorno
            Assert.Equal(200, result.StatusCode);  // Status Code 200
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
