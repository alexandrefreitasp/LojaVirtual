using LojaVirtual.DTO;
using LojaVirtual.Servicos.Contratos;
using LojaVirtual.ViewModels;
using System.Threading.Tasks;

namespace LojaVirtual.Servicos
{
    public class ContaService : BaseService, IContaWS
    {
        public async Task<ContaViewModel> ObterContaAsync( AcessarViewModel model )
        {
            AcessoDTO acesso = new AcessoDTO( )
            {
                Email = model.Email,
                Senha = model.Senha
            };

            ContaDTO dto = await Post<ContaDTO>( "cliente", acesso );
            ContaViewModel contaVM = ParseToVM( dto );
            return contaVM;
        }

        public async Task<ContaViewModel> ObterContaAsync( string CPF )
        {
            ContaDTO dto = await Post<ContaDTO>( "cliente", CPF );
            ContaViewModel contaVM = ParseToVM(dto);
            return contaVM;
        }

        public async Task<ContaViewModel> AtualizarContaAsync( ContaViewModel model )
        {
            ContaDTO dto = ParseToDTO( model );
            dto = await Post<ContaDTO>( "cliente/update", dto);
            ContaViewModel contaVM = ParseToVM(dto);

            return contaVM;
        }


        public async Task<ContaViewModel> SalvarContaAsync( ContaViewModel model )
        {
            ContaDTO dto = ParseToDTO( model );
            dto = await Post<ContaDTO>( "cliente/new", dto);
            ContaViewModel contaVM = ParseToVM(dto);

            return contaVM;
        }

        public async Task ExcluirContaAsync( int idConta )
        {
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