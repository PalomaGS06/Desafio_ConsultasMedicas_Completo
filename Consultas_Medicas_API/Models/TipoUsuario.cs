using System.ComponentModel.DataAnnotations;

namespace ConsultaMedicaVet.Models
{
    public class TipoUsuario
    {
        // Na classe Model, haverá todos os atributos/colunas que compõe a classe Consulta

        [Key]   // primary key 
        public int Id { get; set; }
        [Required]  // campo obrigatório
        public string Tipo { get; set; }
    }
}
