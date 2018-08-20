using Android.App;
using Android.Widget;
using Android.OS;
using LojaVirtual.ViewModels;
using LojaVirtual.Servicos.Contratos;
using LojaVirtual.Servicos;
using Android.Content;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace LojaVirtual
{
    [Activity( MainLauncher = true )]
    public partial class MainActivity : Activity
    {
        #region [ LayoutControls ]
        private EditText Email { get; set; }
        private EditText Senha { get; set; }
        private TextView Mensagem { get; set; }
        #endregion

        protected override void OnCreate( Bundle bundle )
        {
            base.OnCreate( bundle );

            SetContentView( Resource.Layout.Acessar );

            #region [ Set Layout Controls ]

            Email = FindViewById<EditText>( Resource.IdAcessar.txtEmail );
            Senha = FindViewById<EditText>( Resource.IdAcessar.txtSenha );
            Mensagem = FindViewById<TextView>( Resource.IdAcessar.lblMensagem );

            #region [ Buttons events ]
            Button Enviar = FindViewById<Button>( Resource.IdAcessar.btnAcessar );
            Enviar.Click += new System.EventHandler( LoginAsync );

            Button Cadastrar = FindViewById<Button>( Resource.IdAcessar.btnCadastrar );
            Cadastrar.Click += new System.EventHandler( delegate
            {
                var acCadastrar = new Intent( this, typeof( CadastrarActivity ) );
                StartActivity( acCadastrar );
            } );

            #endregion

            #endregion

        }
    }

    public partial class MainActivity
    {
        IContaWS contaWS = new ContaService( );

        #region [ Metodos ]

        protected async void LoginAsync( object sender, EventArgs e )
        {
            Mensagem.Text = "";

            if ( string.IsNullOrEmpty( Email.Text ) || string.IsNullOrEmpty( Senha.Text ) )
            {
                Mensagem.Text = "Por favor, preencher o e-mail ou senha";
                return;
            }


            AcessarViewModel model = new AcessarViewModel( );
            model.Email = Email.Text;
            model.Senha = Senha.Text;
            try
            {
                ContaViewModel conta = await ObterContaAsync( model );
                if ( conta == null || conta.DataExclusao.HasValue )
                {
                    Mensagem.Text = "Dados inválidos, por favor verifique o e-mail ou senha digitados.";
                }
                else
                {
                    var contaSerializada = JsonConvert.SerializeObject( conta );

                    var acConta = new Intent( this, typeof( ContaActivity ) );
                    acConta.PutExtra( "conta", contaSerializada );
                    Senha.Text = "";
                    StartActivity( acConta );
                }
            }
            catch ( Exception )
            {
                Toast.MakeText( this, "Ocorreu um erro.", ToastLength.Long ).Show( );
            }

            
        }

        protected async Task<ContaViewModel> ObterContaAsync( AcessarViewModel model )
        {
            ContaViewModel conta = await contaWS.ObterContaAsync( model );
            return conta;
        }


        #endregion
    }




}

