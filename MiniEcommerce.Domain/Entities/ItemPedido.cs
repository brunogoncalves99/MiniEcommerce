namespace MiniEcommerce.Domain.Entities
{
    public class ItemPedido : EntidadeBase
    {
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }
        
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
