﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace LojaVirtual.DTO
{
    // DTO que representa o objeto de Conta que é utilizado nas comunicações entre o app e o serviço
    public class ContaDTO
    {
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