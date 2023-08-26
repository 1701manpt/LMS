using LMS.Services.Interfaces;
using LMS.Repositories.Interfaces;
using LMS.Models;

namespace LMS.Services
{
    public class BorrowedItemService: IBorrowedItemService
    {
        private readonly IBorrowedItemRepository _borrowedItemRepository;
        private readonly IBorrowedItemTempRepository _borrowedItemTempRepository;

        public BorrowedItemService(IBorrowedItemRepository borrowedItemRepository, IBorrowedItemTempRepository borrowedItemTempRepository)
        {
            _borrowedItemRepository = borrowedItemRepository;
            _borrowedItemTempRepository = borrowedItemTempRepository;
        }

        public List<BorrowedItem> Index()
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
                        Cost = borrowedItemTemp.Quantity * borrowedItemTemp.Item.Price,
                        BorrowedHistoryId = borrowedHistoryId
                    };

                    _borrowedItemRepository.Add(borrowedItem);

                    _borrowedItemTempRepository.Delete(borrowedItemTemp.Id);
                }
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
    }
}
