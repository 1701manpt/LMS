using LMS.Models;

namespace LMS.Repositories.Interfaces
{
    public interface IBorrowerRepository
    {
        Borrower? GetById(int id);
        IEnumerable<Borrower> GetAll();
        void Add(Borrower entity);
        void Update(Borrower entity);
        void Delete(int id);
    }
}
