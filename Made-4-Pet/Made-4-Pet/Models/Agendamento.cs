using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Made_4_Pet.Models
{
    public class Agendamento
    {
        public int EstabId { get; set; }

        public Estabelecimento Estab { get; set; }

        public int ClienteId { get; set; }

        public Cliente Cliente { get; set; }

        public string Servico { get; set; }

        //[Display(Name = "Data")]
        //[Required(ErrorMessage = "É obrigatório escolher uma data para realizar um agendamento.")]
        //public DateTime DataAgendamento { get; set; }
        
        [Display(Name = "Hora")]
        [Required(ErrorMessage = "É obrigatório escolher um horário para realizar um agendamento.")]
        public DateTime HorarioAgendamento { get; set; }

    }
}
