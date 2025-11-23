using MiniEcommerce.Domain.Enums;

namespace MiniEcommerce.Domain.Entities
{
    public class Pedido : EntidadeBase
    {
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        
        public int? CupomId { get; set; }
        public Cupom Cupom { get; set; }
        
        public string NumeroRastreio { get; set; }
        public decimal ValorSubtotal { get; set; }
        public decimal ValorDesconto { get; set; }
        public decimal ValorTotal { get; set; }
        public StatusPedido Status { get; set; }
        
        // Relacionamentos
        public ICollection<ItemPedido> Itens { get; set; }

        public Pedido()
        {
            Itens = new List<ItemPedido>();
            Status = StatusPedido.Pendente;
            NumeroRastreio = GerarNumeroRastreio();
        }

        private string GerarNumeroRastreio()
        {
            return $"BR{DateTime.Now:yyyyMMddHHmmss}{new Random().Next(1000, 9999)}";
        }

        public void CalcularValores()
        {
            ValorSubtotal = Itens.Sum(i => i.ValorUnitario * i.Quantidade);
            
            if (Cupom != null && Cupom.EstaValido())
            {
                ValorDesconto = Cupom.CalcularDesconto(ValorSubtotal);
            }
            else
            {
                ValorDesconto = 0;
            }

            ValorTotal = ValorSubtotal - ValorDesconto;
            DataAtualizacao = DateTime.Now;
        }

        public void AdicionarItem(ItemPedido item)
        {
            Itens.Add(item);
            CalcularValores();
        }

        public void RemoverItem(int itemId)
        {
            var item = Itens.FirstOrDefault(i => i.Id == itemId);
            if (item != null)
            {
                Itens.Remove(item);
                CalcularValores();
            }
        }

        public void AtualizarStatus(StatusPedido novoStatus)
        {
            Status = novoStatus;
            DataAtualizacao = DateTime.Now;
        }

        public void Processar()
        {
            if (Status != StatusPedido.Pendente)
                throw new InvalidOperationException("Pedido já foi processado");

            Status = StatusPedido.Processando;
            DataAtualizacao = DateTime.Now;
        }

        public void MarcarComoEntregue()
        {
            if (Status != StatusPedido.EmTransito)
                throw new InvalidOperationException("Pedido precisa estar em trânsito");

            Status = StatusPedido.Entregue;
            DataAtualizacao = DateTime.Now;
        }

        public void Finalizar()
        {
            if (Status != StatusPedido.Entregue)
                throw new InvalidOperationException("Pedido precisa estar entregue");

            Status = StatusPedido.Finalizado;
            DataAtualizacao = DateTime.Now;
        }
    }
}
