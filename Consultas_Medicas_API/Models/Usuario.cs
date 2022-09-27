using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ConsultaMedicaVet.Models
{
    public class Usuario
    {
        // Na classe Model, haverá todos os atributos/colunas que compõe a classe Consulta

        [Key]  // primary key/ chave primária
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        [ForeignKey("TipoUsuario")]   // foreign key/ chave estrangeira IdTipoUsuario
        public int IdTipoUsuario { get; set; }
        public TipoUsuario TipoUsuario { get; set; }   // classe TipoUsuario como objeto

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public virtual ICollection<Medico> Medico { get; set; }   // lista de Medicos

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public virtual ICollection<Paciente> Paciente { get; set; }  // lista de Pacientes
    }
}
