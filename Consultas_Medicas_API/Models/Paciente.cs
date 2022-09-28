using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultaMedicaVet.Models
{
    public class Paciente
    {
        // Na classe Model, haverá todos os atributos/colunas que compõe a classe Paciente

        [Key]  // primary key 
        public int Id { get; set; }
        [Required]   // campo obrigatório
        public string Carteirinha { get; set; }
        public DateTime DataNascimento { get; set; } = DateTime.Now;
        public bool Ativo { get; set; }

        [ForeignKey("Usuario")] // foreign key IdUsuario
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }  // classe Usuario como objeto
        public virtual ICollection<Consulta> Consulta { get; set; }  // lista de Consultas 

    }
}
