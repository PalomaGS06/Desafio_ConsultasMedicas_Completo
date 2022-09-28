using APIConsultasMedicas.Models;
using ConsultaMedicaVet.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsultaMedicaVet.Contexts
{
    public class ConsultaMedVetContext : DbContext
    {

        public ConsultaMedVetContext(DbContextOptions<ConsultaMedVetContext> options) : base(options)
        {

        }

        // adicionando cada classe como entidades e suas instâncias de contexto
        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<Especialidade> Especialidade { get; set; }
        public DbSet<Administrador> Administrador { get; set; }
        public DbSet<Medico> Medico { get; set; }
        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<TipoUsuario> TipoUsuario { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

    }
}
