using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.MappingProfiles;
using DAO.Transactions;
using Models;

namespace BusinessLogic
{
    public interface IBikeBL
    {
        List<Bike> GetAllBikes();
        Bike GetBikeById(int bikeId);
        Bike GetBikeByName(string bikeName);
        void Update(Bike bike);
        Bike Insert(Bike bike);
        void Delete(int bikeId);
    }
    public class BikeBL:IBikeBL
    {
        private readonly IBikeDal _bikeDal;
        private readonly ICategoriesDal _categoriesDal;
        private readonly IBrandDal _brandDal;
        private readonly IMappingService _mappingService;

        public BikeBL(IBikeDal bikeDal, IMappingService mappingService,ICategoriesDal categoriesDal,IBrandDal brandDal)
        {
            _bikeDal = bikeDal;
            _mappingService = mappingService;
            _categoriesDal = categoriesDal;
            _brandDal = brandDal;
        }

        public List<Bike> GetAllBikes()
        {
            return _mappingService.Map<List<DAO.Models.Bike>, List<Bike>>(_bikeDal.GetAllBikes());
        }

        public Bike GetBikeById(int bikeId)
        {
            return _mappingService.Map<DAO.Models.Bike,Bike>(_bikeDal.GetBikeById(bikeId));
        }

        public Bike GetBikeByName(string bikeName)
        {
            return _mappingService.Map<DAO.Models.Bike, Bike>(_bikeDal.GetBikeByName(bikeName));
        }

        public void Update(Bike bike)
        {
            IDbConnection connection = null;
            IDbTransaction transaction = null;
            _bikeDal.GetConnectionAndTransaction(out connection,out transaction);
            try
            {
                _bikeDal.Update(MapAndProcessCategoryAndBrand(bike), connection, transaction);
                transaction.Commit();
            }
            finally
            {
                connection?.Close();
            }
            
        }

        public Bike Insert(Bike bike)
        {
            IDbConnection connection = null;
            IDbTransaction transaction = null;
            _bikeDal.GetConnectionAndTransaction(out connection, out transaction);
            Bike insertedBike;
            try
            {
                var newBike= _bikeDal.Insert(MapAndProcessCategoryAndBrand(bike), connection, transaction);
                insertedBike = _mappingService.Map<DAO.Models.Bike, Bike>(newBike);
                transaction.Commit();
            }
            finally
            {
                connection?.Close();
            }

            return insertedBike;
        }

        public void Delete(int bikeId)
        {
            _bikeDal.Delete(bikeId);
        }
        private int CheckAndInsertCategory(string categoryName)
        {
            DAO.Models.Category category = _categoriesDal.GetCategory(categoryName);
            if (category == null)
            {
                return _categoriesDal.Insert(categoryName);
            }

            return category.Category_Id;
        }

        private int CheckAndInsertBrand(string brandName)
        {
            DAO.Models.Brand brand = _brandDal.GetBrand(brandName);
            if (brand == null)
            {
                return _brandDal.Insert(brandName);
            }

            return brand.Brand_Id;
        }

        private DAO.Models.Bike MapAndProcessCategoryAndBrand(Bike bike)
        {
            var bikeDaoModel = _mappingService.Map<Bike, DAO.Models.Bike>(bike);
            bikeDaoModel.Category_Id = CheckAndInsertCategory(bikeDaoModel.Category_Name);
            bikeDaoModel.Brand_Id = CheckAndInsertBrand(bikeDaoModel.Brand_Name);

            return bikeDaoModel;
        }
    }
}
