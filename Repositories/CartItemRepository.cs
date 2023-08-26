using LMS.Models;
using LMS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly AppDbContext _context;

        public CartItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public CartItem? GetById(int id)
        {
            try
            {
                return _context.CartItems
                    .Include(_ => _.Cart)
                    .Include(_ => _.Item)
                    .FirstOrDefault(_ => _.Id == id);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<CartItem> GetAll()
        {
            try
            {
                return _context.CartItems
                    .Include(_ => _.Cart)
                    .Include(_ => _.Item)
                    .ToList();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Add(CartItem entity)
        {
            try
            {
                _context.CartItems.Add(entity);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(CartItem entity)
        {
            try
            {
                _context.CartItems.Update(entity);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var cartItem = _context.CartItems.First(_ => _.Id == id);
                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
