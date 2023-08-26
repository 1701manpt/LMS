using LMS.Models;

namespace LMS.Services.Interfaces
{
    public interface IBorrowedItemService
    {
        List<BorrowedItem> Index();
        BorrowedItem Details(int id);
        void CreateBorrowedItems(int borrowedHistoryId);
        BorrowedItem Edit(BorrowedItem borrowedItemTemp);
        bool Delete(int id);
    }
}
