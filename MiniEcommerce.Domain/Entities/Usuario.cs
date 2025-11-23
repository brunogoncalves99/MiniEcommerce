using MiniEcommerce.Domain.Enums;

namespace MiniEcommerce.Domain.Entities
{
    public class Usuario : EntidadeBase
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string SenhaHash { get; set; }
        public PerfilUsuario Perfil { get; set; }
        
        // Relacionamentos
        public ICollection<Pedido> Pedidos { get; set; }

        public Usuario()
        {
            Pedidos = new List<Pedido>();
        }
    }
}
