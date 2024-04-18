using LMS.Models;
using LMS.Repositories.Interfaces;
using LMS.Services.Interfaces;

namespace LMS.Services
{
    public class BorrowedHistoryService: PaginationService<BorrowedHistory>, IBorrowedHistoryService
    {
        private readonly IBorrowedHistoryRepository _borrowedHistoryRepository;
        private readonly IBorrowedItemTempRepository _borrowedItemTempRepository;
        private readonly IItemService _itemService;

        public BorrowedHistoryService(IBorrowedHistoryRepository borrowedHistoryRepository, IBorrowedItemTempRepository borrowedItemTempRepository,IItemService itemService)
        {
            _borrowedHistoryRepository = borrowedHistoryRepository;
            _borrowedItemTempRepository = borrowedItemTempRepository;
            _itemService = itemService;
        }

        public IQueryable<BorrowedHistory> Index()
        {
            try
            {
                return _borrowedHistoryRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IQueryable<BorrowedHistory> GetAll()
        {
            return _borrowedHistoryRepository.GetAll();
        }

        public IQueryable<BorrowedHistory> Pagination(IQueryable<BorrowedHistory> query,int pageNumber, int pageSize)
        {
            try
            {
                return query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IQueryable<BorrowedHistory> Search(IQueryable<BorrowedHistory> query, int? borrowerId, int? itemId, DateTime? startDate, DateTime? endDate, BorrowedState? borrowedState)
        {
            try
            {
                var borrowedHistories = query;

                if (borrowerId != null)
                {
                    borrowedHistories = borrowedHistories.Where(_ => _.BorrowerId == borrowerId);
                }

                if(itemId != null)
                {
                    borrowedHistories = borrowedHistories
                        .Where(_ => _.BorrowedItems.First().ItemId == itemId);
                }

                if (startDate != null)
                {
                    borrowedHistories = borrowedHistories
                        .Where(_ => _.BorrowedDate.Date >= startDate);
                }

                if (endDate != null)
                {
                    borrowedHistories = borrowedHistories
                        .Where(_ => _.BorrowedDate.Date <= endDate);
                }

                if (borrowedState != null)
                {
                    borrowedHistories = borrowedHistories
                        .Where(_ => _.BorrowedState == borrowedState);
                }

                return borrowedHistories;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public BorrowedHistory? Details(int id)
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
                if(!(CalcTotalCost() > 0))
                {
                    throw new Exception("Total cost must be greater than zero. Please add items to the cart before creating.");
                }

                borrowedHistory.BorrowedDate = DateTime.Now;
                borrowedHistory.TotalCost = CalcTotalCost();
                borrowedHistory.BorrowedState = BorrowedState.Open;

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
                    // update available quantity of item
                    if(borrowedItem.ReturnedQuantity < borrowedItem.Quantity)
                    {
                        int quantity = (int)(borrowedItem.Quantity - borrowedItem.ReturnedQuantity);
                        _itemService.UpdateAvailableQuantity(borrowedItem.ItemId, quantity);
                    }
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

        public void UpdateBorrowedState(int id)
        {
            var borrowedHistory = _borrowedHistoryRepository.GetById(id);
            var borrowedItems = borrowedHistory.BorrowedItems;

            bool isOpen = false;

            foreach (var borrowedItem in borrowedItems)
            {
                if (borrowedItem.ReturnedQuantity < borrowedItem.Quantity)
                {
                    isOpen = true;
                    break;
                }
            }

            if (!isOpen)
            {
                borrowedHistory.BorrowedState = BorrowedState.Closed;
            }

            _borrowedHistoryRepository.Update(borrowedHistory);
        }
    }
}
