using MiniEcommerce.Domain.Enums;

namespace MiniEcommerce.Application.DTOs
{
    public class PedidoDTO
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string NomeUsuario { get; set; }
        public int? CupomId { get; set; }
        public string CodigoCupom { get; set; }
        public string NumeroRastreio { get; set; }
        public decimal ValorSubtotal { get; set; }
        public decimal ValorDesconto { get; set; }
        public decimal ValorTotal { get; set; }
        public StatusPedido Status { get; set; }
        public string StatusDescricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public List<ItemPedidoDTO> Itens { get; set; }

        public PedidoDTO()
        {
            Itens = new List<ItemPedidoDTO>();
        }
    }
}
