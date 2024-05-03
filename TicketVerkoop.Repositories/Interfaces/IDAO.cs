

namespace TicketVerkoop.Repositories.Interfaces
{
    public interface IDAO<T> where T : class
    {
        Task<IEnumerable<T>?> GetAll();
        Task<T?> FindById(int Id);
        Task Add(T entity);
        Task Delete(T entity);
        Task<int> AddandGetID(T entity);
        Task<IEnumerable<T>?> GetAllByUserId(string UserId);
    }
}
