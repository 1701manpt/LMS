using LMS.Models;
using LMS.Repositories.Interfaces;
using LMS.Services.Interfaces;

namespace LMS.Services
{
    public class BorrowerService: IBorrowerService
    {
        private readonly IBorrowerRepository _borrowerRepository;

        public BorrowerService(IBorrowerRepository borrowerRepository)
        {
            _borrowerRepository = borrowerRepository;
        }

        public List<Borrower> Index()
        {
            return _borrowerRepository.GetAll().ToList();
        }

        public List<Borrower> Search(string libraryCardNumber)
        {
            return _borrowerRepository
                .GetAll()
                .Where(_ => _.LibraryCardNumber.Contains(libraryCardNumber))
                .ToList();
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
