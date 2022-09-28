using System.ComponentModel.DataAnnotations;

namespace APIConsultasMedicas.Models
{
    public class Login
    {
        // Classe referente a autenticação para gerar o Json e fazer o login do usuário

        [Required(ErrorMessage = "Por favor, informar o e-mail do usuário!")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Por favor, inserir um e-mail válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Por favor, informar a senha do usuário!")]
        [MinLength(5, ErrorMessage = "A senha deverá ter no mínimo 5 dígitos")]
        [MaxLength(14, ErrorMessage = "A senha deverá ter no máximo 14 dígitos")]
        public string Senha { get; set; }
    }
}
