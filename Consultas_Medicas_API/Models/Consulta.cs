using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultaMedicaVet.Models
{
    public class Consulta
    {
        // Na classe Model, haverá todos os atributos/colunas que compõe a classe Consulta

        [Key] // primary key 
        public int Id { get; set; }
        [Required] // campo obrigatório
        public DateTime DataHora { get; set; } = DateTime.Now; //função DateTime.now para utilizar o horário atual sem precisar
                                                               // digitar a data e hora manualmente

        [ForeignKey("Medico")] // foreign key IdMedico
        public int IdMedico { get; set; }
        public Medico Medico { get; set; } // classe Medico como objeto

        [ForeignKey("Paciente")]    // foreign key IdPaciente
        public int IdPaciente { get; set; }
        public Paciente Paciente { get; set; }  // classe Paciente como objeto
    }
}
