using LMS.Models;

namespace LMS.Services.Interfaces
{
    public interface IService<T> : IPaginationService<T>
    {
        IQueryable<T> Index();
        T? Details(int id);
        T Create(T borrowedItemTemp);
        T Edit(T borrowedItemTemp);
        bool Delete(int id);
        bool Exist(int id);
    }
}
