using ServicoREST.Entidades;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ServicoREST.Repositorio
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext( ) : base( "Loja" )
        {
            Database.SetInitializer<DatabaseContext>( null );
        }
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}