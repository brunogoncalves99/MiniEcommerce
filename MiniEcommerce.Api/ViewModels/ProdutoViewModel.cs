using System.ComponentModel.DataAnnotations;

namespace MiniEcommerce.Api.ViewModels
{
    public class ProdutoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [Display(Name = "Nome do Produto")]
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Preço é obrigatório")]
        [Display(Name = "Preço")]
        [DataType(DataType.Currency)]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "Quantidade é obrigatória")]
        [Display(Name = "Quantidade em Estoque")]
        public int QuantidadeEstoque { get; set; }

        [Display(Name = "Imagem")]
        public string ImagemUrl { get; set; }

        [Display(Name = "Categoria")]
        public string Categoria { get; set; }

        public bool Ativo { get; set; }
    }
}
