

namespace TicketVerkoop.Repositories.Interfaces
{
    public interface IMatchDAO<T> where T : class
    {
        Task<IEnumerable<T>?> GetAll();
        Task<IEnumerable<T>?> GetMatchByStadiumId(int Id);

        Task<IEnumerable<T>?> GetMatchByPloegId(int Id);
        Task<IEnumerable<T>?> GetMatchByPloegIdAndStadiumId(int PlegId, int StadiumId);
        Task Add(T entity);
        Task<IEnumerable<T>?> GetAllWithHistory();
    }
}
