using ConsultaMedicaVet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Consultas_Medicas_Tests.Models
{
    public class UsuarioTests
    {
        /// <summary>
        ///Esse teste precisa retornar um resultado não nulo
        /// </summary>
        [Fact]
        public void TestReturnUserNotNull()
        {
            // Preparação
            Usuario usuario;

            // Execução
            usuario = new Usuario();

            // Retorno esperado
            Assert.NotNull(usuario);

        }
    }
}
