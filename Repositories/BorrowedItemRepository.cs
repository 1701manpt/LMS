using LMS.Models;
using LMS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Repositories
{
    public class BorrowedItemRepository : IBorrowedItemRepository
    {
        private readonly AppDbContext _context;

        public BorrowedItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public BorrowedItem? GetById(int id)
        {
            try
            {
                return _context.BorrowedItems
                    .Include(_ => _.BorrowedHistory)
                    .Include(_ => _.Item)
                    .FirstOrDefault(_ => _.Id == id);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<BorrowedItem> GetAll()
        {
            try
            {
                return _context.BorrowedItems
                    .Include(_ => _.BorrowedHistory)
                    .Include(_ => _.Item)
                    .ToList();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Add(BorrowedItem entity)
        {
            try
            {
                _context.BorrowedItems.Add(entity);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(BorrowedItem entity)
        {
            try
            {
                _context.BorrowedItems.Update(entity);
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
                var borrowedItem = _context.BorrowedItems.First(_ => _.Id == id);
                _context.BorrowedItems.Remove(borrowedItem);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
