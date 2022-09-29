using APIConsultasMedicas.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Consultas_Medicas_Tests.Models
{
    public class AdministradorTests
    {
        /// <summary>
        ///Esse teste precisa retornar um resultado não nulo
        /// </summary>
        [Fact]
        public void TestReturnAdminNotNull()
        {
            // Preparação
            Administrador admin;

            // Execução
            admin = new Administrador();

            // Retorno esperado
            Assert.NotNull(admin);

        }

    }
}