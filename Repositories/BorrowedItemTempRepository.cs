using LMS.Models;
using LMS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Repositories
{
    public class BorrowedItemTempRepository : IBorrowedItemTempRepository
    {
        private readonly AppDbContext _context;

        public BorrowedItemTempRepository(AppDbContext context)
        {
            _context = context;
        }

        public BorrowedItemTemp? GetById(int id)
        {
            try
            {
                return _context.BorrowedItemTemps
                    .Include(_ => _.Item)
                    .FirstOrDefault(_ => _.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public BorrowedItemTemp? GetByField(Func<BorrowedItemTemp, bool> predicate)
        {
            try
            {
                return _context.BorrowedItemTemps
                    .Include(_ => _.Item)
                    .FirstOrDefault(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public BorrowedItemTemp? GetByIdInclude(int id)
        {
            try
            {
                return _context.BorrowedItemTemps
                    .Include(_ => _.Item)
                    .FirstOrDefault(_ => _.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IQueryable<BorrowedItemTemp> GetAll()
        {
            try
            {
                return _context.BorrowedItemTemps
                    .Include(_ => _.Item);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Add(BorrowedItemTemp entity)
        {
            try
            {
                _context.BorrowedItemTemps.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(BorrowedItemTemp entity)
        {
            try
            {
                _context.BorrowedItemTemps.Update(entity);
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
                var borrowedItemTemp = _context.BorrowedItemTemps.First(_ => _.Id == id);
                _context.BorrowedItemTemps.Remove(borrowedItemTemp);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DetachedState(BorrowedItemTemp borrowedItemTemp)
        {
            try
            {
                _context.Entry(borrowedItemTemp).State = EntityState.Detached;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
