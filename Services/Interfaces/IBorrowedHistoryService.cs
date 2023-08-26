using LMS.Models;

namespace LMS.Services.Interfaces
{
    public interface IBorrowedHistoryService
    {
        List<BorrowedHistory> Index();
        BorrowedHistory Details(int id);
        BorrowedHistory Create(BorrowedHistory borrowedItemTemp);
        BorrowedHistory Edit(BorrowedHistory borrowedItemTemp);
        bool Delete(int id);
        bool Exist(int id);
        decimal CalcTotalCost();
        List<BorrowedHistory> Search(int? borrowerId, int? itemId, DateTime? startDate, DateTime? endDate, BorrowedState? borrowedState);
    }
}
