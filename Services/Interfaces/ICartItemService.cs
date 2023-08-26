using LMS.Models;

namespace LMS.Services.Interfaces
{
    public interface ICartItemService
    {
        List<CartItem> Index();
        CartItem Details(int id);
        void CreateCartItems(int borrowedHistoryId);
        CartItem Edit(CartItem borrowedItemTemp);
        bool Delete(int id);
    }
}
