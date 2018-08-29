using ServicoREST.Entidades;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ServicoREST.Repositorio
{
    // Cria as configurações do EntityFramework
    public class DatabaseContext : DbContext
    {
        public DatabaseContext( ) : base( "Loja" )
        {
            Database.SetInitializer<DatabaseContext>( null );
        }

        // Tabela de Clientes mapeada pelo EntityFramework
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}