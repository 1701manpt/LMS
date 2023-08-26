using LMS.Models;
using LMS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Repositories
{
    public class BorrowedHistoryRepository : IBorrowedHistoryRepository
    {
        private readonly AppDbContext _context;

        public BorrowedHistoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public BorrowedHistory? GetById(int id)
        {
            try
            {
                return _context.BorrowedHistories
                    .Include(_ => _.Borrower)
                    .Include(_ => _.BorrowedItems)
                    .ThenInclude(_ => _.Item)
                    .FirstOrDefault(_ => _.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<BorrowedHistory> GetAll()
        {
            try
            {
                return _context.BorrowedHistories
                    .Include(_ => _.Borrower)
                    .Include(_ => _.BorrowedItems)
                    .ThenInclude(_ => _.Item);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Add(BorrowedHistory entity)
        {
            try
            {
                _context.BorrowedHistories.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(BorrowedHistory entity)
        {
            try
            {
                _context.BorrowedHistories.Update(entity);
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
                var borrowedHistory = _context.BorrowedHistories.First(_ => _.Id == id);
                _context.BorrowedHistories.Remove(borrowedHistory);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
