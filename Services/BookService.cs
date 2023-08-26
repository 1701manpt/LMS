using LMS.Models;
using LMS.Repositories.Interfaces;
using LMS.Services.Interfaces;

namespace LMS.Services
{
    public class BookService: IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public List<Book> Index()
        {
            try
            {
                return _bookRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Book Details(int id)
        {
            try
            {
                return _bookRepository.GetById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Book Create(Book book)
        {
            try
            {
                book.AvailableQuantity = book.Quantity;

                _bookRepository.Add(book);

                return _bookRepository.GetById(book.Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Book Edit(Book book)
        {
            try
            {
                var bookOld = _bookRepository.GetById(book.Id);

                // Preserve data integrity by assigning information from bookOld to book
                book.Quantity = bookOld.Quantity;
                book.AvailableQuantity = bookOld.AvailableQuantity;

                _bookRepository.DetachedState(bookOld);

                _bookRepository.Update(book);

                return _bookRepository.GetById(book.Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
            try
            {
                if (_bookRepository.GetById(id) == null)
                {
                    return false;
                }
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
