using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Made_4_Pet.Models
{
    public class Agenda
    {
        public IList<string> Horarios { get; set; }

        public string EstabId { get; set; }

    }
}
