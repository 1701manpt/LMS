using LMS.Services.Interfaces;
using LMS.Repositories.Interfaces;
using LMS.Models;

namespace LMS.Services
{
    public class DvdService : PaginationService<Dvd>, IDvdService
    {
        private readonly IDvdRepository _dvdRepository;

        public DvdService(IDvdRepository dvdRepository)
        {
            _dvdRepository = dvdRepository;
        }

        public IQueryable<Dvd> Index()
        {
            return _dvdRepository.GetAll();
        }

        public Dvd Details(int id)
        {
            return _dvdRepository.GetById(id);
        }

        public Dvd Create(Dvd dvd)
        {
            dvd.AvailableQuantity = dvd.Quantity;

            _dvdRepository.Add(dvd);

            return _dvdRepository.GetById(dvd.Id);
        }

        public Dvd Edit(Dvd dvd)
        {
            var dvdOld = _dvdRepository.GetById(dvd.Id);

            dvd.Quantity = dvdOld.Quantity;
            dvd.AvailableQuantity = dvdOld.AvailableQuantity;

            _dvdRepository.Update(dvd);

            return _dvdRepository.GetById(dvd.Id);
        }

        public bool Delete(int id)
        {
            try
            {
                var dvd = _dvdRepository.GetById(id);

                if (dvd?.BorrowedItems != null && dvd.BorrowedItems.Any())
                {
                    throw new Exception("Cannot delete the dvd because it has been borrowed before.");
                }

                _dvdRepository.Delete(id);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Exist(int id)
        {
            if (_dvdRepository.GetById(id) == null)
            {
                return false;
            }
            return true;
        }

        public void UpdateAvailableQuantity(int id, int quantity)
        {
            try
            {
                var item = _dvdRepository.GetById(id);

                item.AvailableQuantity += quantity;

                _dvdRepository.Update(item);
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
                var item = _dvdRepository.GetById(id);

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
