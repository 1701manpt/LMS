using LMS.Models;
using LMS.Repositories.Interfaces;
using LMS.Services.Interfaces;

namespace LMS.Services
{
    public class BorrowerService : PaginationService<Borrower>, IBorrowerService
    {
        private readonly IBorrowerRepository _borrowerRepository;

        public BorrowerService(IBorrowerRepository borrowerRepository)
        {
            _borrowerRepository = borrowerRepository;
        }

        public IQueryable<Borrower> Index()
        {
            return _borrowerRepository.GetAll();
        }

        public IQueryable<Borrower> GetAll()
        {
            try
            {
                return _borrowerRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IQueryable<Borrower> Search(IQueryable<Borrower> query, string? libraryCardNumber)
        {
            if(libraryCardNumber != null)
            {
                query = query
                    .Where(_ => _.LibraryCardNumber.Contains(libraryCardNumber));
            }

            return query;
        }

        public IQueryable<Borrower> Pagination(IQueryable<Borrower> query, int pageNumber, int pageSize)
        {
            try
            {
                return query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Borrower Details(int id)
        {
            return _borrowerRepository.GetById(id);
        }

        public Borrower Create(Borrower borrower)
        {
            _borrowerRepository.Add(borrower);

            return _borrowerRepository.GetById(borrower.Id);
        }

        public Borrower Edit(Borrower borrower)
        {
            _borrowerRepository.Update(borrower);

            return _borrowerRepository.GetById(borrower.Id);
        }

        public bool Delete(int id)
        {
            var borrower = _borrowerRepository.GetById(id);
            if (borrower?.BorrowedHistories != null && borrower.BorrowedHistories.Any())
            {
                throw new Exception("Cannot delete the borrower because they have active borrowing history.");
            }

            _borrowerRepository.Delete(id);

            return true;
        }

        public bool Exist(int id)
        {
            if (_borrowerRepository.GetById(id) == null)
            {
                return false;
            }
            return true;
        }
    }
}
