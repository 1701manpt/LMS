using LMS.Models;

namespace LMS.Repositories.Interfaces
{
    public interface IDvdRepository
    {
        Dvd? GetById(int id);
        List<Dvd> GetAll();
        void Add(Dvd entity);
        void Update(Dvd entity);
        void Delete(int id);
        void DetachedState(Dvd entity);
    }
}
