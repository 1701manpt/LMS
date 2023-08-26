using LMS.Models;
namespace LMS.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Book? GetById(int id);
        List<Book> GetAll();
        void Add(Book entity);
        void Update(Book entity);
        void Delete(int id);
        void DetachedState(Book entity);
    }
}
