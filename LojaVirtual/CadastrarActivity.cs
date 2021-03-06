﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using LojaVirtual.Servicos;
using LojaVirtual.Servicos.Contratos;
using LojaVirtual.ViewModels;

namespace LojaVirtual
{
    [Activity( Label = "CadastrarActivity" )]
    public partial class CadastrarActivity : Activity
    {
        #region [ Layout Controls ]
        private EditText IdConta { get; set; }
        private EditText Nome { get; set; }
        private EditText CPF { get; set; }
        private EditText Email { get; set; }
        private EditText Senha { get; set; }
        private EditText Endereco { get; set; }
        private EditText Estado { get; set; }
        private EditText Municipio { get; set; }
        private EditText Telefone { get; set; }
        private EditText DataCadastro { get; set; }
        private TextView Mensagem { get; set; }
        #endregion

        protected override void OnCreate( Bundle savedInstanceState )
        {
            base.OnCreate( savedInstanceState );
            SetContentView( Resource.Layout.Cadastrar );

            #region [ Set Layout Controls ]
            Nome = FindViewById<EditText>( Resource.IdCadastro.txtNome );
            CPF = FindViewById<EditText>( Resource.IdCadastro.txtCPF );
            CPF.AddTextChangedListener( new Mask( CPF, "###.###.###-##" ) );
            Email = FindViewById<EditText>( Resource.IdCadastro.txtEmail );
            Senha = FindViewById<EditText>( Resource.IdCadastro.txtSenha );
            Endereco = FindViewById<EditText>( Resource.IdCadastro.txtEndereco );
            Estado = FindViewById<EditText>( Resource.IdCadastro.txtEstado );
            Municipio = FindViewById<EditText>( Resource.IdCadastro.txtMunicipio );
            Telefone = FindViewById<EditText>( Resource.IdCadastro.txtTelefone );
            Mensagem = FindViewById<TextView>( Resource.IdCadastro.lblMensagem );

            #region [ Buttons events ]
            Button salvar = FindViewById<Button>( Resource.IdCadastro.btnSalvar );
            salvar.Click += new EventHandler( SalvarAsync );

            #endregion

            #endregion
        }
    }

    public partial class CadastrarActivity : Activity
    {
        // Cria uma instância da classe que acessa os serviços
        IContaWS contaWS = new ContaService( );

        #region [ Metodos ]

        // Salvar os dados de uma conta
        protected async void SalvarAsync( object sender, EventArgs e )
        {
            Mensagem.Text = "";

            // Valida se as informações do formulário são válidas
            if ( !ValidarForm( ) )
            {
                Mensagem.Text = "Existem campos do formulário sem preencher";
                return;
            }

            // Verifica se já existe algum registro cadastrado com o CPF informado
            ContaViewModel conta = await ObterContaAsync( CPF.Text );

            if ( conta == null || conta != null && conta.Id > 0)
            {
                Mensagem.Text = "Usuário já cadastrado";
                return;
            }

            // Cria um ContaViewModel com os dados informados 
            ContaViewModel model = new ContaViewModel( );
            model.Nome = Nome.Text;
            model.CPF = Mask.Unmask(CPF.Text);
            model.Email = Email.Text;
            model.Senha = Senha.Text;
            model.Endereco = Endereco.Text;
            model.Estado = Estado.Text;
            model.Municipio = Municipio.Text;
            model.Telefone = Telefone.Text;

            // Faz a chamada na classe que acessa os serviços para criar um cliente
            conta = await contaWS.SalvarContaAsync( model );

            Toast.MakeText( this, "Operação Realizada com Sucesso!", ToastLength.Long ).Show( );
            this.Finish( );

        }

        // Obtém os dados de uma conta com base no CPF informado
        protected async Task<ContaViewModel> ObterContaAsync( string CPF )
        {
            // Faz a chamada na classe que acessa os serviços para obter os dados do cliente
            ContaViewModel conta = await contaWS.ObterContaAsync( CPF );

            return conta;
        }

        // Valida as informações do formulário
        protected bool ValidarForm( )
        {

            if ( string.IsNullOrEmpty( Nome.Text )
                || string.IsNullOrEmpty( CPF.Text )
                || string.IsNullOrEmpty( Email.Text )
                || string.IsNullOrEmpty( Senha.Text )
                || string.IsNullOrEmpty( Endereco.Text )
                || string.IsNullOrEmpty( Estado.Text )
                || string.IsNullOrEmpty( Municipio.Text )
                || string.IsNullOrEmpty( Telefone.Text )
                )
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}