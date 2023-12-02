using LMS.Models;
using LMS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Repositories
{
    public class BorrowerRepository : IBorrowerRepository
    {
        private readonly AppDbContext _context;

        public BorrowerRepository(AppDbContext context)
        {
            _context = context;
        }

        public Borrower? GetById(int id)
        {
            try
            {
                return _context.Borrowers
                .Include(_ => _.BorrowedHistories)
                .FirstOrDefault(_ => _.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IQueryable<Borrower> GetAll()
        {
            try
            {
                return _context.Borrowers
                    .Include(_ => _.BorrowedHistories);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Add(Borrower entity)
        {
            try
            {
                _context.Borrowers.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Borrower entity)
        {
            try
            {
                _context.Borrowers.Update(entity);
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
                var borrower = _context.Borrowers.First(_ => _.Id == id);
                _context.Borrowers.Remove(borrower);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
