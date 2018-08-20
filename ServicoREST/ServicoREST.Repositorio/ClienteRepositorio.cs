using ServicoREST.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ServicoREST.Repositorio
{
    public class ClienteRepositorio :IDisposable
    {

        DatabaseContext _rp = new DatabaseContext();
        
        public IQueryable<Cliente> GetAll(Expression<Func<Cliente, bool>> func)
        {
            List<Cliente> result = _rp.Clientes.Where(func).ToList();
            return result.AsQueryable();
        }

        public IQueryable<Cliente> GetAll()
        {
            List<Cliente> result = _rp.Clientes.ToList();
            return result.AsQueryable();
        }


        public Cliente Get( int id )
        {
            Cliente result = _rp.Clientes.Find( id );
            return result;
        }

        public Cliente Get( string CPF )
        {
            Cliente result = _rp.Clientes.Where( a=>a.CPF == CPF ).FirstOrDefault( );
            return result;
        }


        public Cliente Get(string email, string senha)
        {
            Cliente result = _rp.Clientes.Where(m => m.Email == email && m.Senha == senha).FirstOrDefault();
            return result;
        }

        public Cliente Update(Cliente cliente)
        {

            Cliente dbCliente = _rp.Clientes.Find(cliente.Id);

            _rp.Entry( dbCliente ).CurrentValues.SetValues( cliente );

            _rp.SaveChanges();
            return dbCliente;
            
        }

        public Cliente Add(Cliente cliente)
        {
            Cliente result = _rp.Clientes.Add(cliente);
            _rp.SaveChanges();
            return result;
        }

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