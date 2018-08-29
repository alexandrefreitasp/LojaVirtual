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
    // Controller que possui os métodos para manutenção de dados de clientes
    public class ClienteController : ApiController
    {
        [HttpGet]
        [Route("loja/cliente/{id:int}")]
        // Obter um cliente pelo seu Id
        public HttpResponseMessage Obter(int id)
        {
            var result = new Cliente();

            // Cria a instancia do repositório e obtém o cliente 
            using (var _rp = new ClienteRepositorio())
            {
                result = _rp.Get(id);
            }
            return Request.CreateResponse(HttpStatusCode.OK, result, "application/json");
        }

        [HttpGet]
        [Route("loja/cliente/{CPF}")]
        // Obter um cliente pelo seu CPF
        public HttpResponseMessage ObterPorCPF(string CPF)
        {
            var result = new Cliente();

            // Cria a instancia do repositório e obtém o cliente 
            using (var _rp = new ClienteRepositorio())
            {
                result = _rp.Get(CPF);
            }
            return Request.CreateResponse(HttpStatusCode.OK, result, "application/json");
        }

        [HttpPost]
        [Route("loja/cliente")]
        // Obter um cliente pelo seu login e senha
        public HttpResponseMessage Obter([FromBody]Login cliente)
        {
            var result = new Cliente();

            // Cria a instancia do repositório e obtém o cliente 
            using (var _rp = new ClienteRepositorio())
            {
                result = _rp.Get(cliente.Email, cliente.Senha);
            }
            return Request.CreateResponse(HttpStatusCode.OK, result, "application/json");
        }


        [HttpPost]
        [Route("loja/cliente/new")]
        // Inserir um novo cliente
        public HttpResponseMessage Inserir([FromBody]Cliente cliente)
        {
            var result = new Cliente();

            // Cria a instancia do repositório e insere o cliente 
            using (var _rp = new ClienteRepositorio())
            {
                cliente.DtCadastro = DateTime.Now;
                result = _rp.Add(cliente);
            }
            return Request.CreateResponse(HttpStatusCode.OK, result, "application/json");
        }

        [HttpPost]
        [Route("loja/cliente/delete")]
        // Excluir um cliente
        public HttpResponseMessage Delete([FromBody]int idCliente)
        {
            bool result = false;

            // Cria a instancia do repositório e exclui o cliente 
            using (var _rp = new ClienteRepositorio())
            {
                _rp.Delete(idCliente);
                result = true;
            }
            return Request.CreateResponse(HttpStatusCode.OK, result, "application/json");
        }

        [HttpPost]
        [Route("loja/cliente/update")]
        // Atualizar um cliente
        public HttpResponseMessage Update([FromBody]Cliente cliente)
        {
            var result = new Cliente();

            // Cria a instancia do repositório e atualiza os dados de um cliente 
            using (var _rp = new ClienteRepositorio())
            {
                result = _rp.Update(cliente);
            }
            return Request.CreateResponse(HttpStatusCode.OK, result, "application/json");
        }

        [HttpGet]
        [Route("loja/cliente/list")]
        // Listar todos os clientes
        public HttpResponseMessage Listar()
        {
            var result = new List<Cliente>();

            // Cria a instancia do repositório e lista os clientes
            using (var _rp = new ClienteRepositorio())
            {
                result = _rp.GetAll().ToList();
            }
            return Request.CreateResponse(HttpStatusCode.OK, result, "application/json");
        }
    }
}
