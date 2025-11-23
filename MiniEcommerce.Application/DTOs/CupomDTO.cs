using System.ComponentModel.DataAnnotations;

namespace MiniEcommerce.Application.DTOs
{
    public class CupomDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Código é obrigatório")]
        [StringLength(50, ErrorMessage = "Código deve ter no máximo 50 caracteres")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Percentual de desconto é obrigatório")]
        [Range(0.01, 100, ErrorMessage = "Percentual deve estar entre 0.01 e 100")]
        public decimal PercentualDesconto { get; set; }

        public decimal? ValorMaximoDesconto { get; set; }
        public decimal? ValorMinimoCompra { get; set; }

        [Required(ErrorMessage = "Data de validade é obrigatória")]
        public DateTime DataValidade { get; set; }

        public int? QuantidadeMaximaUso { get; set; }
        public int QuantidadeUsada { get; set; }
        public bool Ativo { get; set; }
    }
}
