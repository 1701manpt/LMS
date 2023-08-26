using LMS.Models;
using LMS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context)
        {
            _context = context;
        }

        public Cart? GetById(int id)
        {
            try
            {
                return _context.Carts
                    .Include(_ => _.Borrower)
                    .Include(_ => _.CartItems)
                    .ThenInclude(_ => _.Item)
                    .FirstOrDefault(_ => _.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Cart> GetAll()
        {
            try
            {
                return _context.Carts
                    .Include(_ => _.Borrower)
                    .Include(_ => _.CartItems)
                    .ThenInclude(_ => _.Item);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Add(Cart entity)
        {
            try
            {
                _context.Carts.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Cart entity)
        {
            try
            {
                _context.Carts.Update(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var cart = _context.Carts.First(_ => _.Id == id);
                _context.Carts.Remove(cart);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
