using LMS.Models;

namespace LMS.Services.Interfaces
{
    public interface ICartItemTempService
    {
        List<CartItemTemp> Index();
        CartItemTemp Details(int id);
        CartItemTemp Create(CartItemTemp borrowedItemTemp);
        CartItemTemp Edit(CartItemTemp borrowedItemTemp);
        bool Delete(int id);
        bool Exist(int id);
    }
}
