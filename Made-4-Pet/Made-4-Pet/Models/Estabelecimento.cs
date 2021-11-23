using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Made_4_Pet.Models
{
    [Table("Tb_Estabelecimento")]
    public class Estabelecimento
    {
        public string EstabelecimentoId { get; set; }

        [Display(Name = "Nome do estabelecimento")]
        [Required(ErrorMessage = "Informe o nome do estabelecimento.")]
        [MinLength(3, ErrorMessage = "O nome do estabelecimento deve ter, no mínimo, 3 caracteres.")]
        [MaxLength(40, ErrorMessage = "O nome do estabelecimento deve ter, no máximo, 40 caracteres.")]
        public string Nome { get; set; }

        [MinLength(18, ErrorMessage = "O CNPJ deve ter 14 caracteres.")]
        [MaxLength(18, ErrorMessage = "O CNPJ deve ter 14 caracteres.")]
        [Required(ErrorMessage = "Informe o CNPJ.")]
        public string CNPJ { get; set; }

        // um-para-um
        public Endereco Endereco { get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "Informe o telefone.")]
        [RegularExpression(@"(^[(])([0-9]{2})([)])( {1})([9]{1})?([0-9]{3,4})([-])([0-9]{4})", ErrorMessage = "O telefone deve seguir o seguinte formato:  (11) 91234-5678, ou (11) 1234-5678")]
        [MinLength(13, ErrorMessage = "O telefone está incompleto, veja se o DDD foi adicionado.")]
        [MaxLength(15)]
        public string Telefone { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Informe o E-mail.")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(35, ErrorMessage = "O e-mail não pode ultrapassar 35 caracteres.")]
        public string Email { get; set; }

    }
}
