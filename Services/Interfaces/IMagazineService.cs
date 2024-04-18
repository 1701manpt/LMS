using LMS.Models;

namespace LMS.Services.Interfaces
{
    public interface IMagazineService
    {
        List<Magazine> Index();
        List <Magazine> GetByPage(int pageNumber, int pageSize);
        int CountPage(int pageSize);
        Magazine Details(int id);
        Magazine Create(Magazine borrowedItemTemp);
        Magazine Edit(Magazine borrowedItemTemp);
        bool Delete(int id);
        bool Exist(int id);
    }
}
