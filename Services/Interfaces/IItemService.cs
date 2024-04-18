using LMS.Models;

namespace LMS.Services.Interfaces
{
    public interface IItemService
    {
        List<Item> Index();
        List<Item> GetByPage(int pageNumber, int pageSize, string? title);
        int CountPage(int pageSize, string? title);
        Item Details(int id);
        Item Create(Item borrowedItemTemp);
        Item Edit(Item borrowedItemTemp);
        bool Delete(int id);
        bool Exist(int id);
        void UpdateAvailableQuantity(int id, int quantity);
        void CheckAvailableQuantity(int id, int quantity);
    }
}
