using MiniEcommerce.Application.DTOs;
using MiniEcommerce.Application.Interfaces;
using MiniEcommerce.Domain.Entities;
using MiniEcommerce.Domain.Interfaces;

namespace MiniEcommerce.Application.Services
{
    public class ServicoCupom : IServicoCupom
    {
        private readonly IRepositorioCupom _repositorioCupom;

        public ServicoCupom(IRepositorioCupom repositorioCupom)
        {
            _repositorioCupom = repositorioCupom;
        }

        public async Task<CupomDTO> ObterPorIdAsync(int id)
        {
            var cupom = await _repositorioCupom.ObterPorIdAsync(id);
            return MapearParaDTO(cupom);
        }

        public async Task<CupomDTO> ObterPorCodigoAsync(string codigo)
        {
            var cupom = await _repositorioCupom.ObterPorCodigoAsync(codigo);
            return MapearParaDTO(cupom);
        }

        public async Task<IEnumerable<CupomDTO>> ObterTodosAsync()
        {
            var cupons = await _repositorioCupom.ObterTodosAsync();
            return cupons.Select(MapearParaDTO);
        }

        public async Task<IEnumerable<CupomDTO>> ObterValidosAsync()
        {
            var cupons = await _repositorioCupom.ObterCuponsValidosAsync();
            return cupons.Select(MapearParaDTO);
        }

        public async Task<CupomDTO> CriarAsync(CupomDTO cupomDto)
        {
            if (await _repositorioCupom.CodigoExisteAsync(cupomDto.Codigo))
                throw new InvalidOperationException("Código de cupom já existe");

            var cupom = new Cupom
            {
                Codigo = cupomDto.Codigo.ToUpper(),
                PercentualDesconto = cupomDto.PercentualDesconto,
                ValorMaximoDesconto = cupomDto.ValorMaximoDesconto,
                ValorMinimoCompra = cupomDto.ValorMinimoCompra,
                DataValidade = cupomDto.DataValidade,
                QuantidadeMaximaUso = cupomDto.QuantidadeMaximaUso,
                QuantidadeUsada = 0,
                Ativo = true
            };

            var cupomCriado = await _repositorioCupom.AdicionarAsync(cupom);
            return MapearParaDTO(cupomCriado);
        }

        public async Task AtualizarAsync(CupomDTO cupomDto)
        {
            var cupom = await _repositorioCupom.ObterPorIdAsync(cupomDto.Id);
            
            if (cupom == null)
                throw new InvalidOperationException("Cupom não encontrado");

            cupom.Codigo = cupomDto.Codigo.ToUpper();
            cupom.PercentualDesconto = cupomDto.PercentualDesconto;
            cupom.ValorMaximoDesconto = cupomDto.ValorMaximoDesconto;
            cupom.ValorMinimoCompra = cupomDto.ValorMinimoCompra;
            cupom.DataValidade = cupomDto.DataValidade;
            cupom.QuantidadeMaximaUso = cupomDto.QuantidadeMaximaUso;
            cupom.Ativo = cupomDto.Ativo;
            cupom.DataAtualizacao = DateTime.Now;

            await _repositorioCupom.AtualizarAsync(cupom);
        }

        public async Task DeletarAsync(int id)
        {
            await _repositorioCupom.DeletarAsync(id);
        }

        public async Task<decimal> ValidarECalcularDescontoAsync(string codigo, decimal valorTotal)
        {
            var cupom = await _repositorioCupom.ObterPorCodigoAsync(codigo);

            if (cupom == null)
                throw new InvalidOperationException("Cupom não encontrado");

            if (!cupom.EstaValido())
                throw new InvalidOperationException("Cupom inválido ou expirado");

            return cupom.CalcularDesconto(valorTotal);
        }

        private CupomDTO MapearParaDTO(Cupom cupom)
        {
            if (cupom == null) return null;

            return new CupomDTO
            {
                Id = cupom.Id,
                Codigo = cupom.Codigo,
                PercentualDesconto = cupom.PercentualDesconto,
                ValorMaximoDesconto = cupom.ValorMaximoDesconto,
                ValorMinimoCompra = cupom.ValorMinimoCompra,
                DataValidade = cupom.DataValidade,
                QuantidadeMaximaUso = cupom.QuantidadeMaximaUso,
                QuantidadeUsada = cupom.QuantidadeUsada,
                Ativo = cupom.Ativo
            };
        }
    }
}
