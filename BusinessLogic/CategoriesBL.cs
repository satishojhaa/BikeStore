using System;
using System.Collections.Generic;
using BusinessLogic.MappingProfiles;
using DAO.Transactions;
using Models;

namespace BusinessLogic
{
    public interface ICategoriesBl
    {
        List<Category> GetAllCategories();
    }
    public class CategoriesBL : ICategoriesBl
    {
        private readonly ICategoriesDal _categoryDal;
        private readonly IMappingService _mappingService;

        public CategoriesBL(ICategoriesDal categoryDal, IMappingService mappingService)
        {
            _categoryDal = categoryDal;
            _mappingService = mappingService;
        }
        public List<Category> GetAllCategories()
        {
            return _mappingService.Map<List<DAO.Models.Category>, List<Category>>(_categoryDal.GetAllCategories());
        }
    }
}
