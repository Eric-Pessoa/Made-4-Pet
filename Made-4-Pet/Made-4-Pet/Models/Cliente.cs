using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Made_4_Pet.Models
{
    [Table("Tb_Cliente")]
    public class Cliente
    {
        public string ClienteId { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MinLength(6, ErrorMessage = "O nome deve ter 6 caracteres ou mais.")]
        [MaxLength(50, ErrorMessage = "O nome deve ter 50 caracteres ou menos.")]
        [Display(Name = "Qual é o seu nome?")]
        public string Nome { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Informe o E-mail.")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(35, ErrorMessage = "O e-mail deve ter no máximo 35 caracteres.")]
        public string Email { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Informe a senha.")]
        [MinLength(6, ErrorMessage = "A senha deve ter, no mínimo, 6 caracteres.")]
        [MaxLength(25)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        public Endereco Endereco { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [MinLength(14, ErrorMessage = "O CPF deve ter 11 caracteres.")]
        [MaxLength(14, ErrorMessage = "O CPF deve ter 11 caracteres.")]
        public string CPF { get; set; }

        [RegularExpression(@"(^[0-9]{2})([9]{1})?([0-9]{8})", ErrorMessage = "O telefone deve ter 11 caracteres sem pontuação (ddd + número). Ex: 11912345678.")]
        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [MaxLength(15, ErrorMessage = "O telefone deve ter no máximo 15 caracteres.")]
        [MinLength(13, ErrorMessage = "O telefone deve ter no mínimo 13 caracteres.")]
        public string Telefone { get; set; }

    }
}
