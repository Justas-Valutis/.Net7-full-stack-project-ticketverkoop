
namespace TicketVerkoop.Services.Interfaces
{
    public interface IService<T> where T : class
    {
        Task<IEnumerable<T>?> GetAll();
        Task<T?> FindById(int Id);

        Task<IEnumerable<T>?> GetMatchByStadiumId(int Id);
        Task<IEnumerable<T>?> GetMatchByPloegId(int Id);
        Task<IEnumerable<T>?> GetMatchByPloegIdAndStadiumId(int PlegId, int StadiumId);
        Task Add(T entity);
        Task Delete(T entity);
        Task<T?> Get (int v);
    }
}
