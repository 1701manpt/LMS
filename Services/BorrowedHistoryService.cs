using LMS.Models;
using LMS.Repositories.Interfaces;
using LMS.Services.Interfaces;
using NuGet.Versioning;

namespace LMS.Services
{
    public class BorrowedHistoryService: IBorrowedHistoryService
    {
        private readonly IBorrowedHistoryRepository _borrowedHistoryRepository;
        private readonly IBorrowedItemTempRepository _borrowedItemTempRepository;
        private readonly IItemRepository _itemRepository;

        public BorrowedHistoryService(IBorrowedHistoryRepository borrowedHistoryRepository, IBorrowedItemTempRepository borrowedItemTempRepository,IItemRepository itemRepository)
        {
            _borrowedHistoryRepository = borrowedHistoryRepository;
            _borrowedItemTempRepository = borrowedItemTempRepository;
            _itemRepository = itemRepository;
        }

        public List<BorrowedHistory> Index()
        {
            try
            {
                return _borrowedHistoryRepository.GetAll().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<BorrowedHistory> Search(int? borrowerId, int? itemId, DateTime? startDate, DateTime? endDate, BorrowedState? borrowedState)
        {
            try
            {
                var borrowedHistories = _borrowedHistoryRepository.GetAll();
                
                if(borrowerId != null)
                {
                    borrowedHistories = borrowedHistories.Where(_ => _.BorrowerId == borrowerId);   
                }

                if(itemId != null)
                {
                    borrowedHistories = borrowedHistories.Where(_ => _.BorrowedItems.First().ItemId == itemId);
                }

                if (startDate != null)
                {
                    borrowedHistories = borrowedHistories.Where(_ => _.BorrowedDate > startDate);
                }

                if (endDate != null)
                {
                    borrowedHistories = borrowedHistories.Where(_ => _.BorrowedDate < endDate);
                }

                if (borrowedState != null)
                {
                    //borrowedHistories = borrowedHistories.Where(_ => _.borrowedState < endDate);
                }

                return borrowedHistories.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public BorrowedHistory Details(int id)
        {
            try
            {
                return _borrowedHistoryRepository.GetById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public BorrowedHistory Create(BorrowedHistory borrowedHistory)
        {
            try
            {
                borrowedHistory.BorrowedDate = DateTime.Now;
                borrowedHistory.TotalCost = CalcTotalCost();

                _borrowedHistoryRepository.Add(borrowedHistory);

                return _borrowedHistoryRepository.GetById(borrowedHistory.Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public BorrowedHistory Edit(BorrowedHistory borrowedHistory)
        {
            try
            {
                _borrowedHistoryRepository.Update(borrowedHistory);

                return _borrowedHistoryRepository.GetById(borrowedHistory.Id);
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
                var borrowedHistory = _borrowedHistoryRepository.GetById(id);

                foreach (var borrowedItem in borrowedHistory.BorrowedItems)
                {
                    var item = _itemRepository.GetById(borrowedItem.ItemId);

                    // update available quantity of item
                    item.AvailableQuantity += borrowedItem.Quantity;

                    _itemRepository.Update(item);
                }

                _borrowedHistoryRepository.Delete(id);

                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Exist(int id)
        {
            try
            {
                if (_borrowedHistoryRepository.GetById(id) == null)
                {
                    return false;
                }
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public decimal CalcTotalCost()
        {
            try
            {
                decimal totalCost = _borrowedItemTempRepository.GetAll()
                .Sum(_ => _.Quantity * _.Item.Price);

                return totalCost;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
