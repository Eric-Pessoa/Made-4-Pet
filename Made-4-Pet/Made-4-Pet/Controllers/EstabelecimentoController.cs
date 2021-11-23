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
            IList<string> horarios = new List<string>();
            //horarios.Add("19h");
            ViewBag.horarios = horarios;
            return View();
        }
    }
}
