using LMS.Models;
using LMS.Repositories;
using LMS.Repositories.Interfaces;
using LMS.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Services
{
    public class ItemService: IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public List<Item> Index()
        {
            return _itemRepository.GetAll().ToList();
        }

        public List<Item> Search(string title)
        {
            return _itemRepository
                .GetAll()
                .Where(_ => _.Title.ToLower().Contains(title))
                .ToList();
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
    }
}
