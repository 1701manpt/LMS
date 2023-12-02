using LMS.Models;
using LMS.Repositories;
using LMS.Repositories.Interfaces;
using LMS.Services.Interfaces;

namespace LMS.Services
{
    public class MagazineService: IMagazineService
    {
        private readonly IMagazineRepository _magazineRepository;

        public MagazineService(IMagazineRepository magazineRepository)
        {
            _magazineRepository = magazineRepository;
        }

        public List<Magazine> Index()
        {
            return _magazineRepository.GetAll();
        }

        public List<Magazine> GetByPage(int pageNumber, int pageSize)
        {
            try
            {
                return _magazineRepository.GetAll()
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int CountPage(int pageSize)
        {
            try
            {
                int totalItems = _magazineRepository.GetAll().Count();
                int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
                return totalPages;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Magazine Details(int id)
        {
            return _magazineRepository.GetById(id);
        }

        public Magazine Create(Magazine magazine)
        {
            magazine.AvailableQuantity = magazine.Quantity;

            _magazineRepository.Add(magazine);

            return _magazineRepository.GetById(magazine.Id);
        }

        public Magazine Edit(Magazine magazine)
        {
            var magazineOld = _magazineRepository.GetById(magazine.Id);

            magazine.Quantity = magazineOld.Quantity;
            magazine.AvailableQuantity = magazineOld.AvailableQuantity;

            _magazineRepository.Update(magazine);

            return _magazineRepository.GetById(magazine.Id);
        }

        public bool Delete(int id)
        {
            try
            {
                var magazine = _magazineRepository.GetById(id);

                if (magazine?.BorrowedItems != null && magazine.BorrowedItems.Any())
                {
                    throw new Exception("Cannot delete the magazine because it has been borrowed before.");
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
    }
}
