using LMS.Models;
using LMS.Repositories.Interfaces;
using LMS.Services.Interfaces;

namespace LMS.Services
{
    public class BorrowedItemTempService: IBorrowedItemTempService
    {
        private readonly IBorrowedItemTempRepository _borrowedItemTempRepository;
        private readonly IItemRepository _itemRepository;

        public BorrowedItemTempService(IBorrowedItemTempRepository borrowedItemTempRepository, IItemRepository itemRepository)
        {
            _borrowedItemTempRepository = borrowedItemTempRepository;
            _itemRepository = itemRepository;
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
            if (borrowedItemTemp.Quantity > item.AvailableQuantity)
            {
                throw new Exception("Quantity exceeds available quantity.");
            }

            _borrowedItemTempRepository.Add(borrowedItemTemp);

            // update available quantity of item
            item.AvailableQuantity -= borrowedItemTemp.Quantity;
            _itemRepository.Update(item);

            return _borrowedItemTempRepository.GetById(borrowedItemTemp.Id);
        }

        public BorrowedItemTemp Edit(BorrowedItemTemp borrowedItemTemp)
        {
            try
            {
                var item = _itemRepository.GetById(borrowedItemTemp.ItemId);
                var borrowedItemTempOld = _borrowedItemTempRepository.GetById(borrowedItemTemp.Id);

                if (borrowedItemTemp.Quantity > item.AvailableQuantity + borrowedItemTempOld.Quantity)
                {
                    throw new Exception("Quantity exceeds available quantity.");
                }

                _borrowedItemTempRepository.DetachedState(borrowedItemTempOld);

                _borrowedItemTempRepository.Update(borrowedItemTemp);

                // update available quantity of item after edit quantity borrow
                item.AvailableQuantity += borrowedItemTempOld.Quantity;
                item.AvailableQuantity -= borrowedItemTemp.Quantity;

                _itemRepository.Update(item);

                return _borrowedItemTempRepository.GetById(borrowedItemTemp.Id);
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

                // update available quantity of item after delete borrow item
                item.AvailableQuantity += borrowedItemTemp.Quantity;

                _itemRepository.Update(item);

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
