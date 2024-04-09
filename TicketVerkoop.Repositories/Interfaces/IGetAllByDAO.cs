

namespace TicketVerkoop.Repositories.Interfaces
{
    public interface IGetAllByDAO<T> where T : class
    {
        Task<IEnumerable<T>?> GetAllBy(int Id);
    }
}
