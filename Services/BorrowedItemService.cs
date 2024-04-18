using LMS.Services.Interfaces;
using LMS.Repositories.Interfaces;
using LMS.Models;
using NuGet.Packaging;
using LMS.Repositories;

namespace LMS.Services
{
    public class BorrowedItemService: PaginationService<BorrowedItem>, IBorrowedItemService
    {
        private readonly IBorrowedItemRepository _borrowedItemRepository;
        private readonly IBorrowedItemTempRepository _borrowedItemTempRepository;
        private readonly IBorrowedHistoryService _borrowedHistoryService;
        private readonly IItemService _itemService;

        public BorrowedItemService(IBorrowedItemRepository borrowedItemRepository, IBorrowedItemTempRepository borrowedItemTempRepository, IBorrowedHistoryService borrowedHistoryService, IItemService itemService)
        {
            _borrowedItemRepository = borrowedItemRepository;
            _borrowedItemTempRepository = borrowedItemTempRepository;
            _borrowedHistoryService = borrowedHistoryService;
            _itemService = itemService;
        }

        public IQueryable<BorrowedItem> Index()
        {
            try
            {
                return _borrowedItemRepository.GetAll();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public BorrowedItem Details(int id)
        {
            try
            {
                return _borrowedItemRepository.GetById(id);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public BorrowedItem Create(BorrowedItem item)
        {
            _borrowedItemRepository.Add(item);

            return _borrowedItemRepository.GetById(item.Id);
        }

        public void CreateBorrowedItems(int borrowedHistoryId)
        {
            try
            {
                var borrowedItemTemps = _borrowedItemTempRepository.GetAll();

                foreach (var borrowedItemTemp in borrowedItemTemps)
                {
                    var borrowedItem = new BorrowedItem
                    {
                        ItemId = borrowedItemTemp.ItemId,
                        Quantity = borrowedItemTemp.Quantity,
                        ReturnedQuantity = 0,
                        Cost = borrowedItemTemp.Quantity * borrowedItemTemp.Item.Price,
                        BorrowedHistoryId = borrowedHistoryId
                    };

                    Create(borrowedItem);

                    _borrowedItemTempRepository.Delete(borrowedItemTemp.Id);
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public void Return(int id)
        {
            try
            {
                var borrowedItem = _borrowedItemRepository.GetById(id);

                borrowedItem.ReturnedQuantity = borrowedItem.Quantity;

                _borrowedItemRepository.Update(borrowedItem);

                // update borrowed state history if all borrowed items have return quantity = quantity
                _borrowedHistoryService.UpdateBorrowedState(borrowedItem.BorrowedHistoryId);

                // update available quantity item
                _itemService.UpdateAvailableQuantity(borrowedItem.ItemId, borrowedItem.Quantity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public BorrowedItem Edit(BorrowedItem borrowedItem)
        {
            try
            {
                _borrowedItemRepository.Update(borrowedItem);

                return _borrowedItemRepository.GetById(borrowedItem.Id);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                _borrowedItemRepository.Delete(id);

                return true;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public bool Exist(int id)
        {
            try
            {
                if (_borrowedItemRepository.GetById(id) == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
