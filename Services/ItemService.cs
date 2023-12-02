using LMS.Models;
using LMS.Repositories.Interfaces;
using LMS.Services.Interfaces;

namespace LMS.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IBorrowedItemRepository _borrowedItemRepository;

        public ItemService(IItemRepository itemRepository, IBorrowedItemRepository borrowedItemRepository)
        {
            _itemRepository = itemRepository;
            _borrowedItemRepository = borrowedItemRepository;
        }

        public List<Item> Index()
        {
            return _itemRepository.GetAll().ToList();
        }

        public List<Item> GetByPage(int pageNumber, int pageSize, string? title)
        {
            try
            {
                var items = _itemRepository.GetAll();

                if (title != null)
                {
                    items = items.Where(_ => _.Title.ToLower().Contains(title));
                }

                return items
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int CountPage(int pageSize, string? title)
        {
            try
            {
                var items = _itemRepository.GetAll();

                if (title != null)
                {
                    items = items.Where(_ => _.Title.ToLower().Contains(title));
                }

                int totalItems = items.Count();
                int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
                return totalPages;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Item Details(int id)
        {
            return _itemRepository.GetById(id);
        }

        public Item Create(Item item)
        {
            _itemRepository.Add(item);

            return _itemRepository.GetById(item.Id);
        }

        public Item Edit(Item item)
        {
            _itemRepository.Update(item);

            return _itemRepository.GetById(item.Id);
        }

        public bool Delete(int id)
        {
            var item = _itemRepository.GetById(id);
            if (item?.BorrowedItems != null && item.BorrowedItems.Any())
            {
                throw new Exception("Cannot delete the item because it has been borrowed before.");
            }

            _itemRepository.Delete(id);

            return true;
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
            catch(Exception ex)
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
