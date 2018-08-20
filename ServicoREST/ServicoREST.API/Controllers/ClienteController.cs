using ServicoREST.Entidades;
using ServicoREST.Models;
using ServicoREST.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServicoREST.API.Controllers
{
    public class ClienteController : ApiController
    {
        [HttpGet]
        [Route("loja/cliente/{id:int}")]
        public HttpResponseMessage Obter(int id)
        {
            var result = new Cliente();

            using (var _rp = new ClienteRepositorio())
            {
                result = _rp.Get(id);
            }
            return Request.CreateResponse(HttpStatusCode.OK, result, "application/json");
        }

        [HttpGet]
        [Route("loja/cliente/{CPF}")]
        public HttpResponseMessage ObterPorCPF(string CPF)
        {
            var result = new Cliente();

            using (var _rp = new ClienteRepositorio())
            {
                result = _rp.Get(CPF);
            }
            return Request.CreateResponse(HttpStatusCode.OK, result, "application/json");
        }

        [HttpPost]
        [Route("loja/cliente")]
        public HttpResponseMessage Obter([FromBody]Login cliente)
        {
            var result = new Cliente();

            using (var _rp = new ClienteRepositorio())
            {
                result = _rp.Get(cliente.Email, cliente.Senha);
            }
            return Request.CreateResponse(HttpStatusCode.OK, result, "application/json");
        }


        [HttpPost]
        [Route("loja/cliente/new")]
        public HttpResponseMessage Inserir([FromBody]Cliente cliente)
        {
            var result = new Cliente();

            using (var _rp = new ClienteRepositorio())
            {
                cliente.DtCadastro = DateTime.Now;
                result = _rp.Add(cliente);
            }
            return Request.CreateResponse(HttpStatusCode.OK, result, "application/json");
        }

        [HttpPost]
        [Route("loja/cliente/delete")]
        public HttpResponseMessage Delete([FromBody]int idCliente)
        {
            bool result = false;

            using (var _rp = new ClienteRepositorio())
            {
                _rp.Delete(idCliente);
                result = true;
            }
            return Request.CreateResponse(HttpStatusCode.OK, result, "application/json");
        }

        [HttpPost]
        [Route("loja/cliente/update")]
        public HttpResponseMessage Update([FromBody]Cliente cliente)
        {
            var result = new Cliente();

            using (var _rp = new ClienteRepositorio())
            {
                result = _rp.Update(cliente);
            }
            return Request.CreateResponse(HttpStatusCode.OK, result, "application/json");
        }

        [HttpGet]
        [Route("loja/cliente/list")]
        public HttpResponseMessage Listar()
        {
            var result = new List<Cliente>();

            using (var _rp = new ClienteRepositorio())
            {
                result = _rp.GetAll().ToList();
            }
            return Request.CreateResponse(HttpStatusCode.OK, result, "application/json");
        }
    }
}
