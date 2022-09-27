using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ConsultaMedicaVet.Models
{
    public class Medico 
    {
        [Key]  // primary key 
        public int Id { get; set; }
        [Required]   // campo obrigatório
        public string CRM { get; set; }

        [ForeignKey("Especialidade")]  // foreign key IdEspecialidade
        public int IdEspecialidade { get; set; }
        public Especialidade Especialidade { get; set; }  // classe Especialidade como objeto

        [ForeignKey("Usuario")]   // foreign key IdUsuario
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }  // classe Usuario como objeto

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public virtual ICollection<Consulta> Consulta { get; set; }  // lista de consultas 
    }
}
