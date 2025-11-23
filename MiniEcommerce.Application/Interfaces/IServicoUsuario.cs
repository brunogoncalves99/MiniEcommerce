using MiniEcommerce.Application.DTOs;

namespace MiniEcommerce.Application.Interfaces
{
    public interface IServicoUsuario
    {
        Task<UsuarioDTO> ObterPorIdAsync(int id);
        Task<IEnumerable<UsuarioDTO>> ObterTodosAsync();
        Task<UsuarioDTO> CriarAsync(UsuarioDTO usuarioDto);
        Task AtualizarAsync(UsuarioDTO usuarioDto);
        Task DeletarAsync(int id);
        Task<bool> CpfExisteAsync(string cpf);
        Task<bool> EmailExisteAsync(string email);
    }
}
