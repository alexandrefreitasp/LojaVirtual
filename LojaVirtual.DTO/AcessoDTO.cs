using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LojaVirtual.DTO
{
    // DTO que representa os dados de acesso que é utilizado nas comunicações entre o app e o serviço
    public class AcessoDTO
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
