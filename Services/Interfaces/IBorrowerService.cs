using LMS.Models;

namespace LMS.Services.Interfaces
{
    public interface IBorrowerService
    {
        List<Borrower> Index();
        Borrower Details(int id);
        Borrower Create(Borrower borrowedItemTemp);
        Borrower Edit(Borrower borrowedItemTemp);
        bool Delete(int id);
        bool Exist(int id);
        List<Borrower> Search(string libraryCardNumber);
    }
}
