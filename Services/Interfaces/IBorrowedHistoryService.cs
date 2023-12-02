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
        IQueryable<BorrowedHistory> Search(IQueryable<BorrowedHistory> query, int? borrowerId, int? itemId, DateTime? startDate, DateTime? endDate, BorrowedState? borrowedState);
        IQueryable<BorrowedHistory> Pagination(IQueryable<BorrowedHistory> query, int pageNumber, int pageSize);
        IQueryable<BorrowedHistory> GetAll();
        int CountPage(IQueryable<BorrowedHistory> query, int pageSize);
        void UpdateBorrowedState(int id);
    }
}
