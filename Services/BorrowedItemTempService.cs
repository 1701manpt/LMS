using LMS.Models;
using LMS.Repositories.Interfaces;
using LMS.Services.Interfaces;

namespace LMS.Services
{
    public class BorrowedItemTempService: IBorrowedItemTempService
    {
        private readonly IBorrowedItemTempRepository _borrowedItemTempRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IItemService _itemService;

        public BorrowedItemTempService(IBorrowedItemTempRepository borrowedItemTempRepository, IItemRepository itemRepository, IItemService itemService)
        {
            _borrowedItemTempRepository = borrowedItemTempRepository;
            _itemRepository = itemRepository;
            _itemService = itemService;
        }

        public List<BorrowedItemTemp> Index()
        {
            try
            {
                return _borrowedItemTempRepository.GetAll();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public BorrowedItemTemp Details(int id)
        {
            return _borrowedItemTempRepository.GetById(id);
        }

        public BorrowedItemTemp Create(BorrowedItemTemp borrowedItemTemp)
        {
            var checkItemExistInTempList = _borrowedItemTempRepository.GetByField(bit => bit.ItemId == borrowedItemTemp.ItemId);
            
            if (checkItemExistInTempList != null)
            {
                throw new Exception("Item already added to the borrowing list.");
            }

            var item = _itemRepository.GetById(borrowedItemTemp.ItemId);

            // check quantity > available quantity?
            _itemService.CheckAvailableQuantity(item.Id, borrowedItemTemp.Quantity);


            // add borrowedItemTem
            _borrowedItemTempRepository.Add(borrowedItemTemp);

            // difference amount
            int quantity = 0 - borrowedItemTemp.Quantity;

            // update available quantity
            _itemService.UpdateAvailableQuantity(item.Id, quantity);

            return borrowedItemTemp;
        }

        public BorrowedItemTemp Edit(BorrowedItemTemp borrowedItemTemp)
        {
            try
            {
                var item = _itemRepository.GetById(borrowedItemTemp.ItemId);
                var borrowedItemTempCurrent = _borrowedItemTempRepository.GetById(borrowedItemTemp.Id);

                // check quantity > available quantity?
                _itemService.CheckAvailableQuantity(item.Id, borrowedItemTemp.Quantity);

                // difference amount
                int quantity = borrowedItemTempCurrent.Quantity - borrowedItemTemp.Quantity;

                // update borrowedItemTemp
                borrowedItemTempCurrent.Quantity = borrowedItemTemp.Quantity;
                borrowedItemTempCurrent.Cost = borrowedItemTemp.Cost;

                // update available quantity
                _itemService.UpdateAvailableQuantity(item.Id, quantity);

                _borrowedItemTempRepository.Update(borrowedItemTempCurrent);

                return borrowedItemTempCurrent;
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
                var borrowedItemTemp = _borrowedItemTempRepository.GetById(id);
                var item = _itemRepository.GetById(borrowedItemTemp.ItemId);

                _borrowedItemTempRepository.Delete(id);

                // difference amount
                int quantity = borrowedItemTemp.Quantity;

                // update available quantity
                _itemService.UpdateAvailableQuantity(item.Id, quantity);

                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Exist(int id)
        {
            if(_borrowedItemTempRepository.GetById(id) == null)
            {
                return false;
            }
            return true;
        }
    }
}
