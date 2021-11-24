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
using Newtonsoft.Json.Linq;

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
        JObject json;
        List<Estabelecimento> estabelecimentos;


        public IActionResult Index()
        {
            Cliente cliente = new Cliente();
            HttpContext.Session.SetObjectAsJson("UserSession", cliente);
            IList<string> horarios = new List<string>();
            //horarios.Add("19h");
            ViewBag.horarios = horarios;
            return View();
        }
        [HttpGet]
        public IActionResult ProcuraPorServico()
        {
            return View();
        }

        public IActionResult CadastroPrestador()
        {
            //IList<Categoria> categorias = new List<Categoria>();
            //categorias.Add(Categoria.BanhoETosa);
            ViewBag.categorias = new List<string>(new string[] { "Creche", "Banho e Tosa", "Hotel", "Parque", "Comércio", "Veterinária", "Hospital" });
            return View();
        }

        [HttpPost]
        public IActionResult CadastroPrestador(Estabelecimento estabelecimento, IList<string> categorias)
        {
            client = new FireSharp.FirebaseClient(config);
            PushResponse response = client.Push("estabelecimento/", estabelecimento);
            estabelecimento.EstabelecimentoId = response.Result.name;
            foreach (var c in categorias)
            {
                estabelecimento.Categorias.Append(c);
            }
            SetResponse setResponse = client.Set("estabelecimento/" + estabelecimento.EstabelecimentoId, estabelecimento);
            TempData["Sucesso"] = "Cadastrado com sucesso";
            return RedirectToAction("index", "home");
        }

        [HttpPost]
        public IActionResult ProcuraPorServico(string nomeBusca = null, string categoria = null)
        {
            client = new FireSharp.FirebaseClient(config);
            estabelecimentos = new List<Estabelecimento>();
            FirebaseResponse response = client.Get("/estabelecimento/");

            if (response.Body != "null")
            {
                json = JObject.Parse(response.Body);
                foreach (var i in json)
                {
                    var estabelecimento = i.Value.ToObject<Estabelecimento>();
                    estabelecimentos.Add(estabelecimento);
                }

                if (nomeBusca != null)
                {
                    var filter = estabelecimentos.FindAll(i => i.Nome.ToLower().Contains(nomeBusca.ToLower()));
                    return View(filter);
                }
                else if(categoria != null)
                {
                    switch (categoria)
                    {
                        case "banhoETosa":
                            var filter = estabelecimentos.FindAll(i => i.Nome.ToLower().Contains(categoria.ToLower()));
                             return View();
                        case "saude":
                            return View();
                        case "crecheEHotel":
                            return View();
                    }
                }
            }
            return View();
        }

    }
}
