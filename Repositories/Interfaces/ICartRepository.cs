using LMS.Models;

namespace LMS.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Cart? GetById(int id);
        IEnumerable<Cart> GetAll();
        void Add(Cart entity);
        void Update(Cart entity);
        void Delete(int id);
    }
}
