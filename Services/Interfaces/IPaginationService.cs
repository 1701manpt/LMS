namespace LMS.Services.Interfaces
{
    public interface IPaginationService<T>
    {
        IQueryable<T> GetPageByNumberPage(IQueryable<T> query, int pageNumber, int pageSize);
        int CountPage(IQueryable<T> items, int pageSize);
    }
}
