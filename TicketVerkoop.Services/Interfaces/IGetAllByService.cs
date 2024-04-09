

namespace TicketVerkoop.Services.Interfaces
{
    public interface IGetAllByService<T> where T : class
    {
        Task<IEnumerable<T>?> GetAllBy(int Id);
    }
}
