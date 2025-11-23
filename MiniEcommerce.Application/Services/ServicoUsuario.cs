using MiniEcommerce.Application.DTOs;
using MiniEcommerce.Application.Interfaces;
using MiniEcommerce.Domain.Entities;
using MiniEcommerce.Domain.Interfaces;

namespace MiniEcommerce.Application.Services
{
    public class ServicoUsuario : IServicoUsuario
    {
        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IServicoAutenticacao _servicoAutenticacao;

        public ServicoUsuario(IRepositorioUsuario repositorioUsuario, IServicoAutenticacao servicoAutenticacao)
        {
            _repositorioUsuario = repositorioUsuario;
            _servicoAutenticacao = servicoAutenticacao;
        }

        public async Task<UsuarioDTO> ObterPorIdAsync(int id)
        {
            var usuario = await _repositorioUsuario.ObterPorIdAsync(id);
            return MapearParaDTO(usuario);
        }

        public async Task<IEnumerable<UsuarioDTO>> ObterTodosAsync()
        {
            var usuarios = await _repositorioUsuario.ObterTodosAsync();
            return usuarios.Select(MapearParaDTO);
        }

        public async Task<UsuarioDTO> CriarAsync(UsuarioDTO usuarioDto)
        {
            if (await _repositorioUsuario.CpfExisteAsync(usuarioDto.Cpf))
                throw new InvalidOperationException("CPF já cadastrado");

            if (await _repositorioUsuario.EmailExisteAsync(usuarioDto.Email))
                throw new InvalidOperationException("Email já cadastrado");

            var usuario = new Usuario
            {
                Nome = usuarioDto.Nome,
                Cpf = usuarioDto.Cpf,
                Email = usuarioDto.Email,
                SenhaHash = _servicoAutenticacao.GerarHashSenha(usuarioDto.Senha),
                Perfil = usuarioDto.Perfil,
                Ativo = true
            };

            var usuarioCriado = await _repositorioUsuario.AdicionarAsync(usuario);
            return MapearParaDTO(usuarioCriado);
        }

        public async Task AtualizarAsync(UsuarioDTO usuarioDto)
        {
            var usuario = await _repositorioUsuario.ObterPorIdAsync(usuarioDto.Id);
            
            if (usuario == null)
                throw new InvalidOperationException("Usuário não encontrado");

            usuario.Nome = usuarioDto.Nome;
            usuario.Email = usuarioDto.Email;
            usuario.Perfil = usuarioDto.Perfil;
            usuario.Ativo = usuarioDto.Ativo;
            usuario.DataAtualizacao = DateTime.Now;

            if (!string.IsNullOrEmpty(usuarioDto.Senha))
            {
                usuario.SenhaHash = _servicoAutenticacao.GerarHashSenha(usuarioDto.Senha);
            }

            await _repositorioUsuario.AtualizarAsync(usuario);
        }

        public async Task DeletarAsync(int id)
        {
            await _repositorioUsuario.DeletarAsync(id);
        }

        public async Task<bool> CpfExisteAsync(string cpf)
        {
            return await _repositorioUsuario.CpfExisteAsync(cpf);
        }

        public async Task<bool> EmailExisteAsync(string email)
        {
            return await _repositorioUsuario.EmailExisteAsync(email);
        }

        private UsuarioDTO MapearParaDTO(Usuario usuario)
        {
            if (usuario == null) return null;

            return new UsuarioDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Cpf = usuario.Cpf,
                Email = usuario.Email,
                Perfil = usuario.Perfil,
                Ativo = usuario.Ativo
            };
        }
    }
}
