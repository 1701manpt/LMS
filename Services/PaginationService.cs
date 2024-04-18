using LMS.Services.Interfaces;

namespace LMS.Services
{
    public class PaginationService<T> : IPaginationService<T>
    {
        public IQueryable<T> GetPageByNumberPage(IQueryable<T> query, int pageNumber, int pageSize)
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

        public int CountPage(IQueryable<T> items, int pageSize)
        {
            try
            {
                int totalItems = items.Count();
                int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
                return totalPages;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
