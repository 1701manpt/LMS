using LMS.Models;
using LMS.Repositories.Interfaces;
using LMS.Services.Interfaces;

namespace LMS.Services
{
    public class MagazineService : PaginationService<Magazine>, IMagazineService
    {
        private readonly IMagazineRepository _magazineRepository;

        public MagazineService(IMagazineRepository magazineRepository)
        {
            _magazineRepository = magazineRepository;
        }

        public IQueryable<Magazine> Index()
        {
            return _magazineRepository.GetAll();
        }

        public Magazine Details(int id)
        {
            return _magazineRepository.GetById(id);
        }

        public Magazine Create(Magazine dvd)
        {
            dvd.AvailableQuantity = dvd.Quantity;

            _magazineRepository.Add(dvd);

            return _magazineRepository.GetById(dvd.Id);
        }

        public Magazine Edit(Magazine dvd)
        {
            var dvdOld = _magazineRepository.GetById(dvd.Id);

            dvd.Quantity = dvdOld.Quantity;
            dvd.AvailableQuantity = dvdOld.AvailableQuantity;

            _magazineRepository.Update(dvd);

            return _magazineRepository.GetById(dvd.Id);
        }

        public bool Delete(int id)
        {
            try
            {
                var dvd = _magazineRepository.GetById(id);

                if (dvd?.BorrowedItems != null && dvd.BorrowedItems.Any())
                {
                    throw new Exception("Cannot delete the dvd because it has been borrowed before.");
                }

                _magazineRepository.Delete(id);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Exist(int id)
        {
            if (_magazineRepository.GetById(id) == null)
            {
                return false;
            }
            return true;
        }

        public void UpdateAvailableQuantity(int id, int quantity)
        {
            try
            {
                var item = _magazineRepository.GetById(id);

                item.AvailableQuantity += quantity;

                _magazineRepository.Update(item);
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
                var item = _magazineRepository.GetById(id);

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
