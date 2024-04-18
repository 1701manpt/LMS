using LMS.Models;

namespace LMS.Services.Interfaces
{
    public interface IBorrowerService : IService<Borrower>
    {
        IQueryable<Borrower> GetAll();
        IQueryable<Borrower> Search(IQueryable<Borrower> query, string? libraryCardNumber);
    }
}
