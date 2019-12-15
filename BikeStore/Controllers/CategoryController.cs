using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLogic;
using CommonClasses;
using Models;

namespace BikeStore.Controllers
{
    public class CategoryController : ApiController
    {
        private readonly ICategoriesBl _categoriesBl;

        public CategoryController(ICategoriesBl categoriesBl)
        {
            _categoriesBl = categoriesBl;
        }

        public ApiResponse<List<Category>> Get()
        {
            decimal timeInSeconds = DateTime.Now.Millisecond / 1000;
            var categories = _categoriesBl.GetAllCategories();
            return new ApiResponse<List<Category>>()
            {
                Data = categories,
                DataFetched = categories.Count,
                ApiJourneyTime =
                    string.Format(Messages.ApiResponseTime,
                        Math.Abs(timeInSeconds - (decimal)DateTime.Now.Millisecond / 1000)),
                IsSuccess = true,
                Message = Messages.GetApiSuccessMessage
            };
        }
    }
}
