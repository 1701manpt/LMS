using LMS.Models;
using LMS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Repositories
{
    public class CartItemTempRepository : ICartItemTempRepository
    {
        private readonly AppDbContext _context;

        public CartItemTempRepository(AppDbContext context)
        {
            _context = context;
        }

        public CartItemTemp? GetById(int id)
        {
            try
            {
                return _context.CartItemTemps
                    .Include(_ => _.Item)
                    .Include(_ => _.Borrower)
                    .FirstOrDefault(_ => _.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public CartItemTemp? GetByIdInclude(int id)
        {
            try
            {
                return _context.CartItemTemps
                    .Include(_ => _.Item)
                    .FirstOrDefault(_ => _.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<CartItemTemp> GetAll()
        {
            try
            {
                return _context.CartItemTemps
                    .Include(_ => _.Item)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Add(CartItemTemp entity)
        {
            try
            {
                _context.CartItemTemps.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(CartItemTemp entity)
        {
            try
            {
                _context.CartItemTemps.Update(entity);
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
                var cartItemTemp = _context.CartItemTemps.First(_ => _.Id == id);
                _context.CartItemTemps.Remove(cartItemTemp);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DetachedState(CartItemTemp cartItemTemp)
        {
            try
            {
                _context.Entry(cartItemTemp).State = EntityState.Detached;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
