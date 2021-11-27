using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Made_4_Pet.Models;
using Made_4_Pet.SessionHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Made_4_Pet.Controllers
{
    public class HomeController : Controller
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "IhjTIVux94nQcsVA6MOiP5ndaQEc2vSdJ83bfbce",
            BasePath = "https://made4pet-cb9c3-default-rtdb.firebaseio.com/"
        };
        IFirebaseClient client;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string senha)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("/cliente/");
            if(response.Body != "null")
            {
                JObject json = JObject.Parse(response.Body);

                foreach (var g in json)
                {
                    var cliente = g.Value.ToObject<Cliente>();
                    if (cliente.Email == email && cliente.Senha == senha)
                    {
                        HttpContext.Session.SetObjectAsJson("UserSession", cliente);
                        TempData["Sucesso"] = "Seja bem-vindo(a) de volta!";
                        return RedirectToAction("Index", "home");
                    }
                }
                TempData["Erro"] = "Dados de login incorretos! Tente novamente.";
                return View();
            }
            TempData["Info"] = "Não há nenhum usuário cadastrado no sistema. Seja o primeiro!";
            return RedirectToAction("Cadastro");

        }

        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    client = new FireSharp.FirebaseClient(config);
                    FirebaseResponse clientes = client.Get("/cliente/");
                    if (clientes.Body != "null")
                    {
                        JObject json = JObject.Parse(clientes.Body);
                        foreach (var e in json)
                        {
                            var clienteBanco = e.Value.ToObject<Cliente>();
                            if (clienteBanco.Email == cliente.Email)
                            {
                                TempData["Info"] = "Já existe um cliente cadastrado com esse e-mail!";
                                return View();
                            }
                        }
                    }
                    PushResponse response = client.Push("cliente/", cliente);
                    cliente.ClienteId = response.Result.name;
                    SetResponse setResponse = client.Set("cliente/" + cliente.ClienteId, cliente);
                    HttpContext.Session.SetObjectAsJson("UserSession", cliente);
                    TempData["Sucesso"] = "Cadastrado com sucesso";
                    return RedirectToAction("Index", "home");
                }
                catch (Exception)
                {
                    TempData["Erro"] = "Ocorreu um erro ao realizar o cadastro... tente novamente mais tarde!";
                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
