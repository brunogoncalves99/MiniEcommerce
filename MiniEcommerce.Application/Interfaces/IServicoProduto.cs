using MiniEcommerce.Application.DTOs;

namespace MiniEcommerce.Application.Interfaces
{
    public interface IServicoProduto
    {
        Task<ProdutoDTO> ObterPorIdAsync(int id);
        Task<IEnumerable<ProdutoDTO>> ObterTodosAsync();
        Task<IEnumerable<ProdutoDTO>> ObterAtivosAsync();
        Task<IEnumerable<ProdutoDTO>> ObterPorCategoriaAsync(string categoria);
        Task<ProdutoDTO> CriarAsync(ProdutoDTO produtoDto);
        Task AtualizarAsync(ProdutoDTO produtoDto);
        Task DeletarAsync(int id);
        Task AtualizarEstoqueAsync(int id, int quantidade);
    }
}
