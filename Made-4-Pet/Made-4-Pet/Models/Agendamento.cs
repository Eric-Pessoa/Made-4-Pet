using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Made_4_Pet.Models
{
    public class Agendamento
    {
        public string AgendamentoId { get; set; }

        public string EstabId { get; set; }

        public string ClienteId { get; set; }

        public string DataAgendamento { get; set; }

        public string HorarioAgendamento { get; set; }

    }
}
