using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.MappingProfiles;
using DAO.Transactions;
using Models;

namespace BusinessLogic
{
    public interface IBrandBl
    {
        List<Brand> GetAllBrands();
    }
    public class BrandBL : IBrandBl
    {
        private readonly IBrandDal _brandDal;
        private readonly IMappingService _mappingService;

        public BrandBL(IBrandDal brandDal,IMappingService mappingService)
        {
            _brandDal = brandDal;
            _mappingService = mappingService;
        }
        public List<Brand> GetAllBrands()
        {
            return _mappingService.Map<List<DAO.Models.Brand>,List<Brand>>(_brandDal.GetAllBrands());
        }
    }
}
