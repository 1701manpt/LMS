using LMS.Models;
using LMS.Repositories.Interfaces;
using LMS.Services.Interfaces;

namespace LMS.Services
{
    public class CartItemTempService: ICartItemTempService
    {
        private readonly ICartItemTempRepository _cartItemTempRepository;
        private readonly IItemRepository _itemRepository;

        public CartItemTempService(ICartItemTempRepository cartItemTempRepository, IItemRepository itemRepository)
        {
            _cartItemTempRepository = cartItemTempRepository;
            _itemRepository = itemRepository;
        }

        public List<CartItemTemp> Index()
        {
            try
            {
                return _cartItemTempRepository.GetAll();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public CartItemTemp Details(int id)
        {
            return _cartItemTempRepository.GetById(id);
        }

        public CartItemTemp Create(CartItemTemp cartItemTemp)
        {
            var checkItemExistInTempList = _cartItemTempRepository
                .GetAll()
                .Where(_ => _.BorrowerId == cartItemTemp.BorrowerId)
                .Where(_ => _.ItemId == cartItemTemp.ItemId);
            if (checkItemExistInTempList != null && checkItemExistInTempList.Any())
            {
                throw new Exception("Item already added to the borrowing list.");
            }

            var item = _itemRepository.GetById(cartItemTemp.ItemId);
            if (cartItemTemp.Quantity > item.AvailableQuantity)
            {
                throw new Exception("Quantity exceeds available quantity.");
            }

            _cartItemTempRepository.Add(cartItemTemp);

            // update available quantity of item
            item.AvailableQuantity -= cartItemTemp.Quantity;
            _itemRepository.Update(item);

            return _cartItemTempRepository.GetById(cartItemTemp.Id);
        }

        public CartItemTemp Edit(CartItemTemp cartItemTemp)
        {
            try
            {
                var item = _itemRepository.GetById(cartItemTemp.ItemId);
                var cartItemTempOld = _cartItemTempRepository.GetById(cartItemTemp.Id);

                if (cartItemTemp.Quantity > item.AvailableQuantity + cartItemTempOld.Quantity)
                {
                    throw new Exception("Quantity exceeds available quantity.");
                }

                cartItemTemp.BorrowerId = cartItemTempOld.BorrowerId;

                cartItemTemp.ItemId = cartItemTempOld.ItemId;

                _cartItemTempRepository.DetachedState(cartItemTempOld);

                _cartItemTempRepository.Update(cartItemTemp);

                // update available quantity of item after edit quantity borrow
                item.AvailableQuantity += cartItemTempOld.Quantity;
                item.AvailableQuantity -= cartItemTemp.Quantity;

                _itemRepository.Update(item);

                return _cartItemTempRepository.GetById(cartItemTemp.Id);
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
                var cartItemTemp = _cartItemTempRepository.GetById(id);
                var item = _itemRepository.GetById(cartItemTemp.ItemId);

                _cartItemTempRepository.Delete(id);

                // update available quantity of item after delete borrow item
                item.AvailableQuantity += cartItemTemp.Quantity;

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
            if(_cartItemTempRepository.GetById(id) == null)
            {
                return false;
            }
            return true;
        }
    }
}
