using LMS.Models;

namespace LMS.Repositories.Interfaces
{
    public interface IBorrowedItemTempRepository
    {
        BorrowedItemTemp? GetById(int id);
        BorrowedItemTemp? GetByIdInclude(int id);
        BorrowedItemTemp? GetByField(Func<BorrowedItemTemp, bool> predicate);
        List<BorrowedItemTemp> GetAll();
        void Add(BorrowedItemTemp entity);
        void Update(BorrowedItemTemp entity);
        void Delete(int id);
        void DetachedState(BorrowedItemTemp entity);
    }
}
