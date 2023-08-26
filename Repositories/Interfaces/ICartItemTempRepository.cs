using LMS.Models;

namespace LMS.Repositories.Interfaces
{
    public interface ICartItemTempRepository
    {
        CartItemTemp? GetById(int id);
        CartItemTemp? GetByIdInclude(int id);
        List<CartItemTemp> GetAll();
        void Add(CartItemTemp entity);
        void Update(CartItemTemp entity);
        void Delete(int id);
        void DetachedState(CartItemTemp entity);
    }
}
