using ConsultaMedicaVet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Consultas_Medicas_Tests.Models
{
    public class TipoUsuarioTests
    {
        /// <summary>
        ///Esse teste precisa retornar um resultado não nulo
        /// </summary>
        [Fact]
        public void TestReturnUserTypeNotNull()
        {
            // Preparação
            TipoUsuario tipoUsuario;

            // Execução
            tipoUsuario = new TipoUsuario();

            // Retorno esperado
            Assert.NotNull(tipoUsuario);

        }
    }
}
