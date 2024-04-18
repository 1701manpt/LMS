using LMS.Models;

namespace LMS.Repositories.Interfaces
{
    public interface IBorrowedItemTempRepository : IRepository<BorrowedItemTemp>
    {
        BorrowedItemTemp? GetByIdInclude(int id);
        BorrowedItemTemp? GetByField(Func<BorrowedItemTemp, bool> predicate);
        void DetachedState(BorrowedItemTemp entity);
    }
}
