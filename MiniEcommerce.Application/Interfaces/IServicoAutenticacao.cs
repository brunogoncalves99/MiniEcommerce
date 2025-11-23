using MiniEcommerce.Application.DTOs;
using MiniEcommerce.Domain.Entities;

namespace MiniEcommerce.Application.Interfaces
{
    public interface IServicoAutenticacao
    {
        Task<Usuario> AutenticarAsync(LoginDTO loginDto);
        string GerarHashSenha(string senha);
        bool VerificarSenha(string senha, string senhaHash);
    }
}
