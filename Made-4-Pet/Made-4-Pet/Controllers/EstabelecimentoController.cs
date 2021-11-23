using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Made_4_Pet.SessionHelpers;
using Made_4_Pet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FireSharp.Interfaces;
using FireSharp.Config;
using FireSharp.Response;

namespace Made_4_Pet.Controllers
{
    public class EstabelecimentoController : Controller
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "IhjTIVux94nQcsVA6MOiP5ndaQEc2vSdJ83bfbce",
            BasePath = "https://made4pet-cb9c3-default-rtdb.firebaseio.com/"
        };
        IFirebaseClient client;

        public IActionResult Index()
        {
            Cliente cliente = new Cliente();
            HttpContext.Session.SetObjectAsJson("UserSession", cliente);
            IList<string> horarios = new List<string>();
            //horarios.Add("19h");
            ViewBag.horarios = horarios;
            return View();
        }
        public IActionResult ProcuraPorServico()
        {
            return View();
        }
        public IActionResult CadastroPrestador()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastroPrestador(Estabelecimento estabelecimento)
        {
            client = new FireSharp.FirebaseClient(config);
            PushResponse response = client.Push("estabelecimento/", estabelecimento);
            estabelecimento.EstabelecimentoId = response.Result.name;
            SetResponse setResponse = client.Set("estabelecimento/" + estabelecimento.EstabelecimentoId, estabelecimento);
            TempData["Sucesso"] = "Cadastrado com sucesso";
            return RedirectToAction("Index", "Estabelecimento");
        }

    }
}
