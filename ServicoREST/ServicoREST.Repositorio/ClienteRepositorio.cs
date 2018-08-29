using ServicoREST.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ServicoREST.Repositorio
{
    // Repositório de Clientes
    public class ClienteRepositorio :IDisposable
    {
        // Cria uma instancia do DBContext
        DatabaseContext _rp = new DatabaseContext();
        
        // Listar todos os clientes com base no filtro enviado
        public IQueryable<Cliente> GetAll(Expression<Func<Cliente, bool>> func)
        {
            List<Cliente> result = _rp.Clientes.Where(func).ToList();
            return result.AsQueryable();
        }

        // Listar todos os clientes
        public IQueryable<Cliente> GetAll()
        {
            List<Cliente> result = _rp.Clientes.ToList();
            return result.AsQueryable();
        }

        // Obter um cliente por Id
        public Cliente Get( int id )
        {
            Cliente result = _rp.Clientes.Find( id );
            return result;
        }

        // Obter um cliente por CPF
        public Cliente Get( string CPF )
        {
            Cliente result = _rp.Clientes.Where( a=>a.CPF == CPF ).FirstOrDefault( );
            return result;
        }

        // Obter um cliente com base no seu login e senha
        public Cliente Get(string email, string senha)
        {
            Cliente result = _rp.Clientes.Where(m => m.Email == email && m.Senha == senha).FirstOrDefault();
            return result;
        }

        // Atualizar os dados de um cliente
        public Cliente Update(Cliente cliente)
        {

            Cliente dbCliente = _rp.Clientes.Find(cliente.Id);

            _rp.Entry( dbCliente ).CurrentValues.SetValues( cliente );

            _rp.SaveChanges();
            return dbCliente;
            
        }

        // Inserir um novo cliente
        public Cliente Add(Cliente cliente)
        {
            Cliente result = _rp.Clientes.Add(cliente);
            _rp.SaveChanges();
            return result;
        }

        // Excluir um cliente
        public void Delete(int Idcliente)
        {
            var cliente = _rp.Clientes.Find(Idcliente);

            if (cliente != null)
            {
                cliente.DtExclusao = DateTime.Now;
                _rp.SaveChanges();
            }
        }

        public void Dispose()
        {
            
        }
    }
}