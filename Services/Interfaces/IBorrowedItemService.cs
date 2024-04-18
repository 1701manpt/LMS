using LMS.Models;

namespace LMS.Services.Interfaces
{
    public interface IBorrowedItemService : IService<BorrowedItem>
    {
        void CreateBorrowedItems(int borrowedHistoryId);
        void Return(int id);
    }
}
