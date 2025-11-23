using MiniEcommerce.Application.DTOs;
using MiniEcommerce.Application.Interfaces;
using MiniEcommerce.Domain.Entities;
using MiniEcommerce.Domain.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace MiniEcommerce.Application.Services
{
    public class ServicoAutenticacao : IServicoAutenticacao
    {
        private readonly IRepositorioUsuario _repositorioUsuario;

        public ServicoAutenticacao(IRepositorioUsuario repositorioUsuario)
        {
            _repositorioUsuario = repositorioUsuario;
        }

        public async Task<Usuario> AutenticarAsync(LoginDTO loginDto)
        {
            var usuario = await _repositorioUsuario.ObterPorCpfAsync(loginDto.Cpf);
            
            if (usuario == null || !usuario.Ativo)
                return null;

            if (!VerificarSenha(loginDto.Senha, usuario.SenhaHash))
                return null;

            return usuario;
        }

        public string GerarHashSenha(string senha)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(senha);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public bool VerificarSenha(string senha, string senhaHash)
        {
            var hashSenhaInformada = GerarHashSenha(senha);
            return hashSenhaInformada == senhaHash;
        }
    }
}
