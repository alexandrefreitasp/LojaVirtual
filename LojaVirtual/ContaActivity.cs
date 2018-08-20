using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using LojaVirtual.Servicos;
using LojaVirtual.Servicos.Contratos;
using LojaVirtual.ViewModels;

namespace LojaVirtual
{
    [Activity( Label = "ContaActivity" )]
    public partial class ContaActivity : Activity
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

            SetContentView( Resource.Layout.Conta );

            #region [ Set Layout Controls ]
            IdConta = FindViewById<EditText>( Resource.IdConta.hfIdConta );
            Nome = FindViewById<EditText>( Resource.IdConta.txtNome );
            CPF = FindViewById<EditText>( Resource.IdConta.txtCPF );
            CPF.AddTextChangedListener( new Mask( CPF, "###.###.###-##" ) );
            Email = FindViewById<EditText>( Resource.IdConta.txtEmail );
            Senha = FindViewById<EditText>( Resource.IdConta.txtSenha );
            Endereco = FindViewById<EditText>( Resource.IdConta.txtEndereco );
            Estado = FindViewById<EditText>( Resource.IdConta.txtEstado );
            Municipio = FindViewById<EditText>( Resource.IdConta.txtMunicipio );
            Telefone = FindViewById<EditText>( Resource.IdConta.txtTelefone );
            DataCadastro = FindViewById<EditText>( Resource.IdConta.hfDataCadastro );
            Mensagem = FindViewById<TextView>( Resource.IdConta.lblMensagem );

            #region [ Buttons events ]
            Button salvar = FindViewById<Button>( Resource.IdConta.btnSalvar );
            salvar.Click += new EventHandler( SalvarAsync );

            Button excluir = FindViewById<Button>( Resource.IdConta.btnExcluir );
            excluir.Click += new EventHandler( Excluir );
            #endregion

            #endregion

            PopularLayout( );

        }

    }

    public partial class ContaActivity
    {
        IContaWS contaWS = new ContaService( );

        #region [ Metodos ]

        private void PopularLayout( )
        {
            string contraSerializada = Intent.GetStringExtra( "conta" );

            if ( contraSerializada == null && String.IsNullOrEmpty( contraSerializada ) )
                this.Finish( );

            ContaViewModel conta = JsonConvert.DeserializeObject<ContaViewModel>( contraSerializada );
            IdConta.Text = conta.Id.ToString( );
            Nome.Text = conta.Nome;
            CPF.Text = conta.CPF;
            Email.Text = conta.Email;
            Senha.Text = conta.Senha;
            Endereco.Text = conta.Endereco;
            Estado.Text = conta.Estado;
            Municipio.Text = conta.Municipio;
            Telefone.Text = conta.Telefone;
            DataCadastro.Text = conta.DataCadastro.ToString( );

        }

        protected async void SalvarAsync( object sender, EventArgs e )
        {
            Mensagem.Text = "";
            if ( !ValidarForm( ) )
            {
                Mensagem.Text = "Existem campos do formulário sem preencher";
                return;
            }

            ContaViewModel model = new ContaViewModel( );
            model.Id = Convert.ToInt32( IdConta.Text );
            model.Nome = Nome.Text;
            model.CPF = Mask.Unmask(CPF.Text);
            model.Email = Email.Text;
            model.Senha = Senha.Text;
            model.Endereco = Endereco.Text;
            model.Estado = Estado.Text;
            model.Municipio = Municipio.Text;
            model.Telefone = Telefone.Text;
            model.DataCadastro = DateTime.Parse( DataCadastro.Text );

            ContaViewModel conta = await contaWS.AtualizarContaAsync( model );

            Toast.MakeText( this, "Operação Realizada com Sucesso!", ToastLength.Long ).Show( );

        }

        protected void Excluir( object sender, EventArgs e )
        {
            AlertDialog.Builder alert = new AlertDialog.Builder( this );
            alert.SetTitle( "Salvar" );
            alert.SetMessage( "Deseja realmente excluir a conta?" );
            alert.SetPositiveButton( "Sim", delegate
            {
                contaWS.ExcluirContaAsync( Convert.ToInt32( IdConta.Text ) );
                Toast.MakeText( this, "Conta excluída com sucesso!", ToastLength.Long ).Show( );
                alert.Dispose( );
                this.Finish( );
            } );
            alert.SetNegativeButton( "Não", delegate
            {
                alert.Dispose( );
            } );
            alert.Show( );


        }


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