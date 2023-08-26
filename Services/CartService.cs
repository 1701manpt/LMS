using LMS.Models;
using LMS.Repositories.Interfaces;
using LMS.Services.Interfaces;

namespace LMS.Services
{
    public class CartService: ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemTempRepository _cartItemTempRepository;
        private readonly IItemRepository _itemRepository;

        public CartService(ICartRepository cartRepository, ICartItemTempRepository cartItemTempRepository,IItemRepository itemRepository)
        {
            _cartRepository = cartRepository;
            _cartItemTempRepository = cartItemTempRepository;
            _itemRepository = itemRepository;
        }

        public List<Cart> Index()
        {
            try
            {
                return _cartRepository.GetAll().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Cart Details(int id)
        {
            try
            {
                return _cartRepository.GetById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Cart Create(Cart cart)
        {
            try
            {
                cart.BorrowedDate = DateTime.Now;
                cart.TotalCost = CalcTotalCost();

                _cartRepository.Add(cart);

                return _cartRepository.GetById(cart.Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Cart Edit(Cart cart)
        {
            try
            {
                _cartRepository.Update(cart);

                return _cartRepository.GetById(cart.Id);
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
                var cart = _cartRepository.GetById(id);

                foreach (var cartItem in cart.CartItems)
                {
                    var item = _itemRepository.GetById(cartItem.ItemId);

                    // update available quantity of item
                    item.AvailableQuantity += cartItem.Quantity;

                    _itemRepository.Update(item);
                }

                _cartRepository.Delete(id);

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
                if (_cartRepository.GetById(id) == null)
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
                decimal totalCost = _cartItemTempRepository.GetAll()
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
