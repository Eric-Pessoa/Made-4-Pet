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

        public IActionResult Index(string id, string dataBusca, bool? agendado)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse estabs = client.Get("/estabelecimento/");
            FirebaseResponse agendamentosBanco = client.Get("/agendamento/");
            JObject jsonAgendamentos = new JObject();
            JObject jsonEstabs = JObject.Parse(estabs.Body);
            if (agendamentosBanco.Body != "null")
            {
                jsonAgendamentos = JObject.Parse(agendamentosBanco.Body);
            }
            if (estabs.Body != "null")
            {
                jsonEstabs = JObject.Parse(estabs.Body);
            }

            Estabelecimento estab = new Estabelecimento();
            IList<string> agendamentos = new List<string>();
            var podeAgendar = false;
            string data = "";
            if (dataBusca != null) { data = DateTime.Parse(dataBusca).ToString(); }
            else { data = DateTime.Today.ToString(); }
            ViewBag.data = data;
            foreach (var i in jsonEstabs)
            {
                var e = i.Value.ToObject<Estabelecimento>();
                if (e.EstabelecimentoId == id)
                {
                    estab = e;
                    if (jsonAgendamentos.Count > 0)
                    {
                        foreach (var json in jsonAgendamentos)
                        {
                            var a = json.Value.ToObject<Agendamento>();
                            if (a.EstabId == e.EstabelecimentoId && dataBusca != null)
                            {
                                if (data == a.DataAgendamento)
                                {
                                    agendamentos.Add(a.HorarioAgendamento);
                                }
                            }
                        }
                    }
                    break;
                }
            }
            foreach (var c in estab.Categorias) { if (c == "Banho e Tosa") { podeAgendar = true; } }
            HttpContext.Session.SetObjectAsJson("EstabSession", estab);
            ViewBag.podeAgendar = podeAgendar;
            ViewBag.agendamentos = agendamentos;
            if (agendado != null) { ViewBag.agendado = true; }
            return View(estab);
        }
        [HttpGet]
        public IActionResult ProcuraPorServico()
        {
            return View();
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
                    var filterByName = estabelecimentos.FindAll(i => i.Nome.ToLower().Contains(nomeBusca.ToLower()));
                    var filterByAddress = estabelecimentos.FindAll(i => i.Endereco.Logradouro.ToLower().Contains(nomeBusca.ToLower()));
                    var filterByNeighborhood = estabelecimentos.FindAll(i => i.Endereco.Bairro.ToLower().Contains(nomeBusca.ToLower()));
                    ViewBag.Pesquisa = nomeBusca;
                    var lista = new List<Estabelecimento>();
                    foreach (var i in filterByName) { if (!lista.Contains(i)) { lista.Add(i); } }
                    foreach (var i in filterByAddress) { if (!lista.Contains(i)) { lista.Add(i); } }
                    foreach (var i in filterByNeighborhood) { if (!lista.Contains(i)) { lista.Add(i); } }
                    return View(lista);
                }
                else if (categoria != null)
                {
                    IList<Estabelecimento> listaFiltrada = new List<Estabelecimento>();
                    switch (categoria)
                    {
                        case "banhoETosa":
                            foreach (var c in estabelecimentos)
                            {
                                foreach (var y in c.Categorias)
                                {
                                    if (y == "Banho e Tosa")
                                    {
                                        if (!listaFiltrada.Contains(c)) { listaFiltrada.Add(c); }
                                    }
                                }
                            }
                            ViewBag.Pesquisa = "Banho e tosa";
                            return View(listaFiltrada);
                        case "saude":
                            foreach (var c in estabelecimentos)
                            {
                                foreach (var y in c.Categorias)
                                {
                                    if (y == "Veterinária" || y == "Hospital")
                                    {
                                        if (!listaFiltrada.Contains(c)) { listaFiltrada.Add(c); }
                                    }
                                }
                            }
                            ViewBag.Pesquisa = " Saúde";
                            return View(listaFiltrada);
                        case "crecheEHotel":
                            foreach (var c in estabelecimentos)
                            {
                                foreach (var y in c.Categorias)
                                {
                                    if (y == "Creche" || y == "Hotel")
                                    {
                                        if (!listaFiltrada.Contains(c)) { listaFiltrada.Add(c); }
                                    }
                                }
                            }
                            ViewBag.Pesquisa = " Creches e hotéis";
                            return View(listaFiltrada);
                    }
                }
            }
            TempData["Info"] = "Não há nenhum estabelecimento cadastrado no sistema. Cadastre o primeiro!";
            return View();
        }

        public IActionResult CadastroPrestador()
        {
            ViewBag.categorias = new List<string>(new string[] { "Creche", "Banho e Tosa", "Hotel", "Parque", "Comércio", "Veterinária", "Hospital" });
            return View();
        }

        [HttpPost]
        public IActionResult CadastroPrestador(Estabelecimento estabelecimento, IList<string> categorias)
        {
            try
            {
                ViewBag.categorias = new List<string>(new string[] { "Creche", "Banho e Tosa", "Hotel", "Parque", "Comércio", "Veterinária", "Hospital" });

                if (ModelState.IsValid)
                {
                    client = new FireSharp.FirebaseClient(config);
                    FirebaseResponse estabs = client.Get("/estabelecimento/");
                    JObject json = JObject.Parse(estabs.Body);
                    foreach (var e in json)
                    {
                        var estabBanco = e.Value.ToObject<Estabelecimento>();
                        if (estabBanco.CNPJ == estabelecimento.CNPJ)
                        {
                            TempData["Info"] = "Já existe um estabelecimento cadastrado com esse CNPJ!";
                            return View();
                        }
                    }
                    PushResponse response = client.Push("estabelecimento/", estabelecimento);
                    estabelecimento.EstabelecimentoId = response.Result.name;
                    foreach (var c in categorias)
                    {
                        estabelecimento.Categorias.Append(c);
                    }
                    TimeSpan hora = TimeSpan.FromHours(1);
                    IList<string> horarios = new List<string>();

                    for (var h = estabelecimento.HoraAbertura; h < estabelecimento.HoraFechamento; h += hora)
                    {
                        horarios.Add(h.ToShortTimeString());
                    }
                    Agenda agenda = new Agenda()
                    {
                        EstabId = estabelecimento.EstabelecimentoId,
                        Horarios = horarios,
                    };
                    estabelecimento.Agenda = agenda;
                    client.Set("estabelecimento/" + estabelecimento.EstabelecimentoId, estabelecimento);
                    TempData["Sucesso"] = "Cadastrado com sucesso";
                    return RedirectToAction("index", "home");

                }
                return View();
            }
            catch (Exception)
            {
                TempData["Erro"] = "Algo deu errado! Tente novamente";
                return View();
            }
        }


        [HttpPost]
        public IActionResult AgendarHorario(string horario, string data)
        {
            Cliente cliente = HttpContext.Session.GetObjectFromJson<Cliente>("UserSession");
            if (cliente == null)
            {
                TempData["Info"] = "Faça login para agendar um horário!";
                return RedirectToAction("login", "home");
            }
            Estabelecimento estab = HttpContext.Session.GetObjectFromJson<Estabelecimento>("EstabSession");
            if (DateTime.Parse(data) < DateTime.Today)
            {
                TempData["Erro"] = "A data escolhida já passou!";
                return RedirectToAction("index", new { id = estab.EstabelecimentoId });
            }
            if (DateTime.Parse(data) == DateTime.Today && TimeSpan.Parse(horario) < DateTime.Now.TimeOfDay)
            {
                TempData["Erro"] = "O horário escolhido já passou!";
                return RedirectToAction("index", new { id = estab.EstabelecimentoId });
            }
            Agendamento agendamento = new Agendamento()
            {
                EstabId = estab.EstabelecimentoId,
                ClienteId = cliente.ClienteId,
                DataAgendamento = data,
                HorarioAgendamento = horario,
            };
            client = new FireSharp.FirebaseClient(config);
            PushResponse response = client.Push("agendamento/", agendamento);
            agendamento.AgendamentoId = response.Result.name;
            client.Set("agendamento/" + agendamento.AgendamentoId, agendamento);
            client.Update("/estabelecimento/" + estab.EstabelecimentoId, estab);
            data = DateTime.Parse(data).ToShortDateString();
            ViewBag.data = data;
            HttpContext.Session.SetObjectAsJson("EstabSession", estab);
            TempData["Info"] = $"Banho e Tosa marcados para {horario}, dia {data}!";
            return RedirectToAction("index", new { id = estab.EstabelecimentoId, dataBusca = data, agendado = true });

        }

    }
}
