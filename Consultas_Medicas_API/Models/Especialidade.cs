using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConsultaMedicaVet.Models
{
    public class Especialidade
    {
        // Na classe Model, haverá todos os atributos/colunas que compõe a classe Consulta

        [Key]  // primary key
        public int Id { get; set; }
        [Required]  // campo obrigatório
        public string Categoria { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public virtual ICollection<Medico> Medico { get; set; }  // lista de médicos 


    }
}
