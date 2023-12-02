using LMS.Models;

namespace LMS.Services.Interfaces
{
    public interface IBookService
    {
        List<Book> Index();
        List<Book> GetByPage(int pageNumber, int pageSize);
        int CountPage(int pageSize);
        Book Details(int id);
        Book Create(Book borrowedItemTemp);
        Book Edit(Book borrowedItemTemp);
        bool Delete(int id);
        bool Exist(int id);
    }
}
