using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicoREST.Entidades
{
    [Table("Clientes")]
    // Mapeia a tabela Clientes na base de dados
    public class Cliente
    {
        [Column("ClienteId")]
        public int Id { get; set; }

        public string Nome { get; set; }

        public string CPF { get; set; }
        
        public string Estado { get; set; }
        
        public string Municipio { get; set; }

        public string Endereco { get; set; }
        
        public string Telefone { get; set; }
        
        public string Email { get; set; }

        public string Senha { get; set; }

        public DateTime DtCadastro { get; set; }

        public DateTime? DtExclusao { get; set; }

    }
}