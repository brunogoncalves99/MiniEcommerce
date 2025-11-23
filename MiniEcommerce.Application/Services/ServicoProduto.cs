using MiniEcommerce.Application.DTOs;
using MiniEcommerce.Application.Interfaces;
using MiniEcommerce.Domain.Entities;
using MiniEcommerce.Domain.Interfaces;

namespace MiniEcommerce.Application.Services
{
    public class ServicoProduto : IServicoProduto
    {
        private readonly IRepositorioProduto _repositorioProduto;

        public ServicoProduto(IRepositorioProduto repositorioProduto)
        {
            _repositorioProduto = repositorioProduto;
        }

        public async Task<ProdutoDTO> ObterPorIdAsync(int id)
        {
            var produto = await _repositorioProduto.ObterPorIdAsync(id);
            return MapearParaDTO(produto);
        }

        public async Task<IEnumerable<ProdutoDTO>> ObterTodosAsync()
        {
            var produtos = await _repositorioProduto.ObterTodosAsync();
            return produtos.Select(MapearParaDTO);
        }

        public async Task<IEnumerable<ProdutoDTO>> ObterAtivosAsync()
        {
            var produtos = await _repositorioProduto.BuscarAsync(p => p.Ativo);
            return produtos.Select(MapearParaDTO);
        }

        public async Task<IEnumerable<ProdutoDTO>> ObterPorCategoriaAsync(string categoria)
        {
            var produtos = await _repositorioProduto.ObterPorCategoriaAsync(categoria);
            return produtos.Select(MapearParaDTO);
        }

        public async Task<ProdutoDTO> CriarAsync(ProdutoDTO produtoDto)
        {
            var produto = new Produto
            {
                Nome = produtoDto.Nome,
                Descricao = produtoDto.Descricao,
                Preco = produtoDto.Preco,
                QuantidadeEstoque = produtoDto.QuantidadeEstoque,
                ImagemUrl = produtoDto.ImagemUrl,
                Categoria = produtoDto.Categoria,
                Ativo = true
            };

            var produtoCriado = await _repositorioProduto.AdicionarAsync(produto);
            return MapearParaDTO(produtoCriado);
        }

        public async Task AtualizarAsync(ProdutoDTO produtoDto)
        {
            var produto = await _repositorioProduto.ObterPorIdAsync(produtoDto.Id);
            
            if (produto == null)
                throw new InvalidOperationException("Produto não encontrado");

            produto.Nome = produtoDto.Nome;
            produto.Descricao = produtoDto.Descricao;
            produto.Preco = produtoDto.Preco;
            produto.QuantidadeEstoque = produtoDto.QuantidadeEstoque;
            produto.ImagemUrl = produtoDto.ImagemUrl;
            produto.Categoria = produtoDto.Categoria;
            produto.Ativo = produtoDto.Ativo;
            produto.DataAtualizacao = DateTime.Now;

            await _repositorioProduto.AtualizarAsync(produto);
        }

        public async Task DeletarAsync(int id)
        {
            await _repositorioProduto.DeletarAsync(id);
        }

        public async Task AtualizarEstoqueAsync(int id, int quantidade)
        {
            var produto = await _repositorioProduto.ObterPorIdAsync(id);
            
            if (produto == null)
                throw new InvalidOperationException("Produto não encontrado");

            produto.QuantidadeEstoque = quantidade;
            produto.DataAtualizacao = DateTime.Now;

            await _repositorioProduto.AtualizarAsync(produto);
        }

        private ProdutoDTO MapearParaDTO(Produto produto)
        {
            if (produto == null) return null;

            return new ProdutoDTO
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                QuantidadeEstoque = produto.QuantidadeEstoque,
                ImagemUrl = produto.ImagemUrl,
                Categoria = produto.Categoria,
                Ativo = produto.Ativo
            };
        }
    }
}
