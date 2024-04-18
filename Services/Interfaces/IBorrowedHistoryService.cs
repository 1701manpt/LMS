using LMS.Models;

namespace LMS.Services.Interfaces
{
    public interface IBorrowedHistoryService : IService<BorrowedHistory>
    {
        decimal CalcTotalCost();
        IQueryable<BorrowedHistory> Search(IQueryable<BorrowedHistory> query, int? borrowerId, int? itemId, DateTime? startDate, DateTime? endDate, BorrowedState? borrowedState);
        IQueryable<BorrowedHistory> GetAll();
        void UpdateBorrowedState(int id);
    }
}
