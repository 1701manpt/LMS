using LMS.Services.Interfaces;
using LMS.Repositories.Interfaces;
using LMS.Models;

namespace LMS.Services
{
    public class CartItemService: ICartItemService
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly ICartItemTempRepository _cartItemTempRepository;

        public CartItemService(ICartItemRepository cartItemRepository, ICartItemTempRepository cartItemTempRepository)
        {
            _cartItemRepository = cartItemRepository;
            _cartItemTempRepository = cartItemTempRepository;
        }

        public List<CartItem> Index()
        {
            try
            {
                return _cartItemRepository.GetAll();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public CartItem Details(int id)
        {
            try
            {
                return _cartItemRepository.GetById(id);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public void CreateCartItems(int cartId)
        {
            try
            {
                var cartItemTemps = _cartItemTempRepository.GetAll();

                foreach (var cartItemTemp in cartItemTemps)
                {
                    var cartItem = new CartItem
                    {
                        ItemId = cartItemTemp.ItemId,
                        Quantity = cartItemTemp.Quantity,
                        Cost = cartItemTemp.Quantity * cartItemTemp.Item.Price,
                        CartId = cartId
                    };

                    _cartItemRepository.Add(cartItem);

                    _cartItemTempRepository.Delete(cartItemTemp.Id);
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public CartItem Edit(CartItem cartItem)
        {
            try
            {
                _cartItemRepository.Update(cartItem);

                return _cartItemRepository.GetById(cartItem.Id);
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
                _cartItemRepository.Delete(id);

                return true;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
