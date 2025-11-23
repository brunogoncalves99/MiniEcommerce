using MiniEcommerce.Domain.Entities;
using System.Linq.Expressions;

namespace MiniEcommerce.Domain.Interfaces
{
    public interface IRepositorioBase<T> where T : EntidadeBase
    {
        Task<T> ObterPorIdAsync(int id);
        Task<IEnumerable<T>> ObterTodosAsync();
        Task<IEnumerable<T>> BuscarAsync(Expression<Func<T, bool>> predicate);
        Task<T> AdicionarAsync(T entidade);
        Task AtualizarAsync(T entidade);
        Task DeletarAsync(int id);
        Task<bool> ExisteAsync(int id);
        Task<int> ContarAsync(Expression<Func<T, bool>> predicate = null);
    }
}
