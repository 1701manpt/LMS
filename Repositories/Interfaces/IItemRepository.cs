using LMS.Models;

namespace LMS.Repositories.Interfaces
{
    public interface IItemRepository
    {
        Item? GetById(int id);
        IEnumerable<Item> GetAll();
        void Add(Item entity);
        void Update(Item entity);
        void Delete(int id);
    }
}
