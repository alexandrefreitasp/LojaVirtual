using LojaVirtual.ViewModels;
using System.Threading.Tasks;

namespace LojaVirtual.Servicos.Contratos
{
    // Interfavce que possui os métodos disponibilizados pelo serviço de Contas
    public interface IContaWS
    {
        Task<ContaViewModel> ObterContaAsync( AcessarViewModel model );
        Task<ContaViewModel> ObterContaAsync( string cpf );
        Task<ContaViewModel> AtualizarContaAsync( ContaViewModel model );
        Task ExcluirContaAsync( int idConta );
        Task<ContaViewModel> SalvarContaAsync( ContaViewModel model );
    }
}