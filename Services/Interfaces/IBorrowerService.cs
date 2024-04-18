using LMS.Models;

namespace LMS.Services.Interfaces
{
    public interface IBorrowerService
    {
        List<Borrower> Index();
        IQueryable<Borrower> GetAll();
        Borrower Details(int id);
        Borrower Create(Borrower borrowedItemTemp);
        Borrower Edit(Borrower borrowedItemTemp);
        bool Delete(int id);
        bool Exist(int id);
        IQueryable<Borrower> Search(IQueryable<Borrower> query, string? libraryCardNumber);
        IQueryable<Borrower> Pagination(IQueryable<Borrower> query, int pageNumber, int pageSize);
        int CountPage(IQueryable<Borrower> query, int pageSize);
    }
}
