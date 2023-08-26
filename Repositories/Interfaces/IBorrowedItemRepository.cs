using LMS.Models;

namespace LMS.Repositories.Interfaces
{
    public interface IBorrowedItemRepository
    {
        BorrowedItem? GetById(int id);
        List<BorrowedItem> GetAll();
        void Add(BorrowedItem entity);
        void Update(BorrowedItem entity);
        void Delete(int id);
    }
}
