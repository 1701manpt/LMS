using LMS.Models;

namespace LMS.Repositories.Interfaces
{
    public interface ICartItemRepository
    {
        CartItem? GetById(int id);
        List<CartItem> GetAll();
        void Add(CartItem entity);
        void Update(CartItem entity);
        void Delete(int id);
    }
}
