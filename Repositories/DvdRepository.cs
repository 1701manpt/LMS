using LMS.Models;
using LMS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Repositories
{
    public class DvdRepository : IDvdRepository
    {
        private readonly AppDbContext _context;

        public DvdRepository(AppDbContext context)
        {
            _context = context;
        }

        public Dvd? GetById(int id)
        {
            try
            {
                return _context.Dvds
                .Include(_ => _.BorrowedItems)
                .FirstOrDefault(_ => _.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IQueryable<Dvd> GetAll()
        {
            try
            {
                return _context.Dvds
                .Include(_ => _.BorrowedItems);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Add(Dvd entity)
        {
            try
            {
                _context.Dvds.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Dvd entity)
        {
            try
            {
                _context.Dvds.Update(entity);
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
                var item = _context.Dvds.First(_ => _.Id == id);
                _context.Dvds.Remove(item);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
