using LMS.Models;
using LMS.Repositories.Interfaces;
using LMS.Services.Interfaces;

namespace LMS.Services
{
    public class BookService : PaginationService<Book>, IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public IQueryable<Book> Index()
        {
            return _bookRepository.GetAll();
        }

        public Book Details(int id)
        {
            return _bookRepository.GetById(id);
        }

        public Book Create(Book book)
        {
            book.AvailableQuantity = book.Quantity;

            _bookRepository.Add(book);

            return _bookRepository.GetById(book.Id);
        }

        public Book Edit(Book book)
        {
            var bookOld = _bookRepository.GetById(book.Id);

            book.Quantity = bookOld.Quantity;
            book.AvailableQuantity = bookOld.AvailableQuantity;

            _bookRepository.Update(book);

            return _bookRepository.GetById(book.Id);
        }

        public bool Delete(int id)
        {
            try
            {
                var book = _bookRepository.GetById(id);

                if (book?.BorrowedItems != null && book.BorrowedItems.Any())
                {
                    throw new Exception("Cannot delete the book because it has been borrowed before.");
                }

                _bookRepository.Delete(id);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Exist(int id)
        {
            if (_bookRepository.GetById(id) == null)
            {
                return false;
            }
            return true;
        }

        public void UpdateAvailableQuantity(int id, int quantity)
        {
            try
            {
                var item = _bookRepository.GetById(id);

                item.AvailableQuantity += quantity;

                _bookRepository.Update(item);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CheckAvailableQuantity(int id, int quantity)
        {
            try
            {
                var item = _bookRepository.GetById(id);

                if (quantity > item.AvailableQuantity)
                {
                    throw new Exception("Quantity exceeds available quantity.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
