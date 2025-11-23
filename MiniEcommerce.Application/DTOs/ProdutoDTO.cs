using System.ComponentModel.DataAnnotations;

namespace MiniEcommerce.Application.DTOs
{
    public class ProdutoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(200, ErrorMessage = "Nome deve ter no máximo 200 caracteres")]
        public string Nome { get; set; }

        [StringLength(1000, ErrorMessage = "Descrição deve ter no máximo 1000 caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Preço é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Preço deve ser maior que zero")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "Quantidade em estoque é obrigatória")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantidade deve ser maior ou igual a zero")]
        public int QuantidadeEstoque { get; set; }

        public string ImagemUrl { get; set; }

        [StringLength(100, ErrorMessage = "Categoria deve ter no máximo 100 caracteres")]
        public string Categoria { get; set; }

        public bool Ativo { get; set; }
    }
}
