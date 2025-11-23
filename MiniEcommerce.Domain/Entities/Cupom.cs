namespace MiniEcommerce.Domain.Entities
{
    public class Cupom : EntidadeBase
    {
        public string Codigo { get; set; }
        public decimal PercentualDesconto { get; set; }
        public decimal? ValorMaximoDesconto { get; set; }
        public decimal? ValorMinimoCompra { get; set; }
        public DateTime DataValidade { get; set; }
        public int? QuantidadeMaximaUso { get; set; }
        public int QuantidadeUsada { get; set; }
        
        // Relacionamentos
        public ICollection<Pedido> Pedidos { get; set; }

        public Cupom()
        {
            Pedidos = new List<Pedido>();
            QuantidadeUsada = 0;
        }

        public bool EstaValido()
        {
            if (!Ativo) return false;
            if (DateTime.Now > DataValidade) return false;
            if (QuantidadeMaximaUso.HasValue && QuantidadeUsada >= QuantidadeMaximaUso.Value) return false;
            
            return true;
        }

        public decimal CalcularDesconto(decimal valorTotal)
        {
            if (!EstaValido())
                return 0;

            if (ValorMinimoCompra.HasValue && valorTotal < ValorMinimoCompra.Value)
                return 0;

            var desconto = valorTotal * (PercentualDesconto / 100);

            if (ValorMaximoDesconto.HasValue && desconto > ValorMaximoDesconto.Value)
                desconto = ValorMaximoDesconto.Value;

            return desconto;
        }

        public void IncrementarUso()
        {
            QuantidadeUsada++;
            DataAtualizacao = DateTime.Now;
        }
    }
}
