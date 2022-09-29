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
    public class PacienteControllerTests
    {
        // Preparação  
        private readonly Mock<IPacienteRepository> _mock; // A função Mock cria um repositório "fake" para usá-lo no controller
        private readonly PacienteController _ctl;

        public PacienteControllerTests()  // Gerando um método construtor
        {
            _mock = new Mock<IPacienteRepository>();
            _ctl = new PacienteController(_mock.Object);
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
                Ativo = true,
                Carteirinha = "963852741",
                DataNascimento = DateTime.Now,
                IdUsuario = 10,
                Usuario = new Usuario
                {
                    Nome = "Paciente",
                    Email = "pacienteteste@testando.com",
                    Senha = "987456abc",
                    IdTipoUsuario = 2,
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
