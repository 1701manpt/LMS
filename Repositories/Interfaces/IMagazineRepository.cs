using LMS.Models;

namespace LMS.Repositories.Interfaces
{
    public interface IMagazineRepository
    {
        Magazine? GetById(int id);
        List<Magazine> GetAll();
        void Add(Magazine entity);
        void Update(Magazine entity);
        void Delete(int id);
        void DetachedState(Magazine entity);
    }
}
