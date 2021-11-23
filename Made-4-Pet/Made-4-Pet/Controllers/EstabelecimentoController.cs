using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Made_4_Pet.SessionHelpers;
using Made_4_Pet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Made_4_Pet.Controllers
{
    public class EstabelecimentoController : Controller
    {
        public IActionResult Index()
        {
            Cliente cliente = new Cliente();
            HttpContext.Session.SetObjectAsJson("UserSession", cliente);
            return View();
        }
        public IActionResult ProcuraPorServico()
        {
            return View();
        }
    }
}
