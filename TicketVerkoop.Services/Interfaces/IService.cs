
namespace TicketVerkoop.Services.Interfaces
{
    public interface IService<T> where T : class
    {
        Task<IEnumerable<T>?> GetAll();
        Task<IEnumerable<T>?> FindById(int Id);
        Task Add(T entity);
        Task Delete(T entity);
        Task<T?> Get (int v);
    }
}
