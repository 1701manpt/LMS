using LMS.Models;
using LMS.Repositories.Interfaces;
using LMS.Services.Interfaces;

namespace LMS.Services
{
    public class ItemService : PaginationService<Item>, IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public IQueryable<Item> Index()
        {
            return _itemRepository.GetAll();
        }

        public Item Details(int id)
        {
            return _itemRepository.GetById(id);
        }

        public Item Create(Item item)
        {
            item.AvailableQuantity = item.Quantity;

            _itemRepository.Add(item);

            return _itemRepository.GetById(item.Id);
        }

        public Item Edit(Item item)
        {
            var itemOld = _itemRepository.GetById(item.Id);

            item.Quantity = itemOld.Quantity;
            item.AvailableQuantity = itemOld.AvailableQuantity;

            _itemRepository.Update(item);

            return _itemRepository.GetById(item.Id);
        }

        public bool Delete(int id)
        {
            try
            {
                var item = _itemRepository.GetById(id);

                if (item?.BorrowedItems != null && item.BorrowedItems.Any())
                {
                    throw new Exception("Cannot delete the item because it has been borrowed before.");
                }

                _itemRepository.Delete(id);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Exist(int id)
        {
            if (_itemRepository.GetById(id) == null)
            {
                return false;
            }
            return true;
        }

        public void UpdateAvailableQuantity(int id, int quantity)
        {
            try
            {
                var item = _itemRepository.GetById(id);

                item.AvailableQuantity += quantity;

                _itemRepository.Update(item);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CheckAvailableQuantity(int id, int quantity)
        {
            try
            {
                var item = _itemRepository.GetById(id);

                if (quantity > item.AvailableQuantity)
                {
                    throw new Exception("Quantity exceeds available quantity.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
