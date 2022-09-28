using ConsultaMedicaVet.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIConsultasMedicas.Models
{
    // Classe referente aos desenvolvedores da API, sendo usuários administradores com autorização de acesso
    public class Administrador
    {
        [Key]  // primary key 
        public int Id { get; set; }

        public string TipoAcesso { get; set; } // Tipo de acesso, no caso: Admin


        [Required(ErrorMessage = "Favor, informar seu CPF de 11 dígitos e somente com números!")]
        public string CPF { get; set; } // CPF como obrigatório para a identificação do desenvolvedor admin


        [ForeignKey("Usuario")]   // foreign key IdUsuario
    public int IdUsuario { get; set; }
    public Usuario Usuario { get; set; }  // classe Usuario como objeto

    }
}
