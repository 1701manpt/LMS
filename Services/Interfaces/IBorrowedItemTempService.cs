using LMS.Models;

namespace LMS.Services.Interfaces
{
    public interface IBorrowedItemTempService
    {
        List<BorrowedItemTemp> Index();
        BorrowedItemTemp Details(int id);
        BorrowedItemTemp Create(BorrowedItemTemp borrowedItemTemp);
        BorrowedItemTemp Edit(BorrowedItemTemp borrowedItemTemp);
        bool Delete(int id);
        bool Exist(int id);
    }
}
