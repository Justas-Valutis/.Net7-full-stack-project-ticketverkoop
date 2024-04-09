

namespace TicketVerkoop.Repositories.Interfaces
{
    public interface IDAO<T> where T : class
    {
        Task<IEnumerable<T>?> GetAll();
        Task<IEnumerable<T>?> FindById(int Id);
        Task Add(T entity);
        Task Delete(T entity);
    }
}
