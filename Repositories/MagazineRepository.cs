using LMS.Models;
using LMS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Repositories
{
    public class MagazineRepository : IMagazineRepository
    {
        private readonly AppDbContext _context;

        public MagazineRepository(AppDbContext context)
        {
            _context = context;
        }

        public Magazine? GetById(int id)
        {
            try
            {
                return _context.Magazines
                .Include(_ => _.BorrowedItems)
                .FirstOrDefault(_ => _.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IQueryable<Magazine> GetAll()
        {
            try
            {
                return _context.Magazines
                .Include(_ => _.BorrowedItems);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Add(Magazine entity)
        {
            try
            {
                _context.Magazines.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Magazine entity)
        {
            try
            {
                _context.Magazines.Update(entity);
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
                var item = _context.Magazines.First(_ => _.Id == id);
                _context.Magazines.Remove(item);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
