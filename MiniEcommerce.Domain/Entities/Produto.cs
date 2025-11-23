namespace MiniEcommerce.Domain.Entities
{
    public class Produto : EntidadeBase
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int QuantidadeEstoque { get; set; }
        public string ImagemUrl { get; set; }
        public string Categoria { get; set; }
        
        // Relacionamentos
        public ICollection<ItemPedido> ItensPedido { get; set; }

        public Produto()
        {
            ItensPedido = new List<ItemPedido>();
        }

        public bool TemEstoqueDisponivel(int quantidade)
        {
            return QuantidadeEstoque >= quantidade;
        }

        public void BaixarEstoque(int quantidade)
        {
            if (!TemEstoqueDisponivel(quantidade))
                throw new InvalidOperationException("Estoque insuficiente");
            
            QuantidadeEstoque -= quantidade;
            DataAtualizacao = DateTime.Now;
        }

        public void AdicionarEstoque(int quantidade)
        {
            QuantidadeEstoque += quantidade;
            DataAtualizacao = DateTime.Now;
        }
    }
}
