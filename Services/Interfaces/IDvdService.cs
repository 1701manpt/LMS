using LMS.Models;

namespace LMS.Services.Interfaces
{
    public interface IDvdService
    {
        List<Dvd> Index();
        Dvd Details(int id);
        Dvd Create(Dvd borrowedItemTemp);
        Dvd Edit(Dvd borrowedItemTemp);
        bool Delete(int id);
        bool Exist(int id);
    }
}
