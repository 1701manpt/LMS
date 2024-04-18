using LMS.Models;

namespace LMS.Services.Interfaces
{
    public interface IItemService : IService<Item>
    {
        void UpdateAvailableQuantity(int id, int quantity);
        void CheckAvailableQuantity(int id, int quantity);
    }
}
