using LojaVirtual.DTO;
using LojaVirtual.Servicos.Contratos;
using LojaVirtual.ViewModels;
using System.Threading.Tasks;

namespace LojaVirtual.Servicos
{
    // Classe que disponibiliza os métodos de acesso a transações no serviço de Contas
    public class ContaService : BaseService, IContaWS
    {
        // Método para obter uma conta no serviço
        public async Task<ContaViewModel> ObterContaAsync( AcessarViewModel model )
        {
            // Objeto que será enviado como parâmetro ao serviço
            AcessoDTO acesso = new AcessoDTO( )
            {
                Email = model.Email,
                Senha = model.Senha
            };

            // Chamada ao método para realização de um HTTP Post enviando o AcessoDTO como paâmetro
            ContaDTO dto = await Post<ContaDTO>( "cliente", acesso );

            // Transforma o DTO recebido do serviço em ViewModel para uso no app
            ContaViewModel contaVM = ParseToVM( dto );

            return contaVM;
        }

        // Método para obter uma conta no serviço pelo CPF informado
        public async Task<ContaViewModel> ObterContaAsync( string CPF )
        {
            // Chamada ao método para realização de um HTTP Post enviando o CPF como paâmetro
            ContaDTO dto = await Post<ContaDTO>( "cliente", CPF );

            // Transforma o DTO recebido do serviço em ViewModel para uso no app
            ContaViewModel contaVM = ParseToVM(dto);

            return contaVM;
        }

        // Método para atualizar uma conta no serviço
        public async Task<ContaViewModel> AtualizarContaAsync( ContaViewModel model )
        {
            // Transforma o ContaViewModel em ContaDTO para enviar ao serviço
            ContaDTO dto = ParseToDTO( model );

            // Faz a chamada no serviço e recebe o retorno na variável DTO
            dto = await Post<ContaDTO>( "cliente/update", dto);

            // Transforma o DTO recebido do serviço em ViewModel para uso no app
            ContaViewModel contaVM = ParseToVM(dto);

            return contaVM;
        }

        // Método para criar uma conta no serviço
        public async Task<ContaViewModel> SalvarContaAsync( ContaViewModel model )
        {
            // Transforma o ContaViewModel em ContaDTO para enviar ao serviço
            ContaDTO dto = ParseToDTO( model );

            // Faz a chamada no serviço e recebe o retorno na variável DTO
            dto = await Post<ContaDTO>( "cliente/new", dto);

            // Transforma o DTO recebido do serviço em ViewModel para uso no app
            ContaViewModel contaVM = ParseToVM(dto);

            return contaVM;
        }

        // Método para excluir uma conta no serviço
        public async Task ExcluirContaAsync( int idConta )
        {
            // Faz a chamada no serviço e retorna se o registro foi excluído com sucesso
            bool resp = await Post<bool>("cliente/delete", idConta );
        }

        #region [ Parses ]

        private ContaViewModel ParseToVM( ContaDTO contaDTO )
        {
            if ( contaDTO == null )
                return null;

            ContaViewModel contaVM = new ContaViewModel( );
            contaVM.Id = contaDTO.Id;
            contaVM.CPF = contaDTO.CPF;
            contaVM.Email = contaDTO.Email;
            contaVM.Endereco = contaDTO.Endereco;
            contaVM.Estado = contaDTO.Estado;
            contaVM.Municipio = contaDTO.Municipio;
            contaVM.Nome = contaDTO.Nome;
            contaVM.Senha = contaDTO.Senha;
            contaVM.Telefone = contaDTO.Telefone;
            contaVM.DataCadastro = contaDTO.DtCadastro;
            contaVM.DataExclusao = contaDTO.DtExclusao;

            return contaVM;
        }

        private ContaDTO ParseToDTO( ContaViewModel contaVM )
        {
            if ( contaVM == null )
                return null;

            ContaDTO contaDTO = new ContaDTO( );
            contaDTO.Id = contaVM.Id;
            contaDTO.CPF = contaVM.CPF;
            contaDTO.Email = contaVM.Email;
            contaDTO.Endereco = contaVM.Endereco;
            contaDTO.Estado = contaVM.Estado;
            contaDTO.Municipio = contaVM.Municipio;
            contaDTO.Nome = contaVM.Nome;
            contaDTO.Senha = contaVM.Senha;
            contaDTO.Telefone = contaVM.Telefone;
            contaDTO.DtCadastro = contaVM.DataCadastro;
            contaDTO.DtExclusao = contaVM.DataExclusao;

            return contaDTO;
        }

        #endregion
    }
}