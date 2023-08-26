using LMS.Models;
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
