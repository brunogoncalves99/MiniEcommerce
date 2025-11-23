using MiniEcommerce.Application.DTOs;

namespace MiniEcommerce.Application.Interfaces
{
    public interface IServicoCupom
    {
        Task<CupomDTO> ObterPorIdAsync(int id);
        Task<CupomDTO> ObterPorCodigoAsync(string codigo);
        Task<IEnumerable<CupomDTO>> ObterTodosAsync();
        Task<IEnumerable<CupomDTO>> ObterValidosAsync();
        Task<CupomDTO> CriarAsync(CupomDTO cupomDto);
        Task AtualizarAsync(CupomDTO cupomDto);
        Task DeletarAsync(int id);
        Task<decimal> ValidarECalcularDescontoAsync(string codigo, decimal valorTotal);
    }
}
