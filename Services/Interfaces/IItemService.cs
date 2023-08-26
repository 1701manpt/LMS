using LMS.Models;

namespace LMS.Services.Interfaces
{
    public interface IItemService
    {
        List<Item> Index();
        Item Details(int id);
        Item Create(Item borrowedItemTemp);
        Item Edit(Item borrowedItemTemp);
        bool Delete(int id);
        bool Exist(int id);
        List<Item> Search(string title);
    }
}
