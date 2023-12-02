using LMS.Models;

namespace LMS.Repositories.Interfaces
{
    public interface IBorrowedHistoryRepository
    {
        BorrowedHistory? GetById(int id);
        IQueryable<BorrowedHistory> GetAll();
        void Add(BorrowedHistory entity);
        void Update(BorrowedHistory entity);
        void Delete(int id);
    }
}
