using LMS.Models;

namespace LMS.Services.Interfaces
{
    public interface ICartService
    {
        List<Cart> Index();
        Cart Details(int id);
        Cart Create(Cart cartItemTemp);
        Cart Edit(Cart cartItemTemp);
        bool Delete(int id);
        bool Exist(int id);
        decimal CalcTotalCost();
    }
}
