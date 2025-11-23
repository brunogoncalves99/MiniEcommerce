namespace MiniEcommerce.Application.DTOs
{
    public class CarrinhoDTO
    {
        public List<ItemCarrinhoDTO> Itens { get; set; }
        public string CodigoCupom { get; set; }
        public decimal ValorSubtotal { get; set; }
        public decimal ValorDesconto { get; set; }
        public decimal ValorTotal { get; set; }

        public CarrinhoDTO()
        {
            Itens = new List<ItemCarrinhoDTO>();
        }
    }

    public class ItemCarrinhoDTO
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public string ImagemUrl { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorTotal => Preco * Quantidade;
    }
}
