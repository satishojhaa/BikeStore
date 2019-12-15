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
    public class BrandController : ApiController
    {
        private readonly IBrandBl _brandBl;

        public BrandController(IBrandBl brandBl)
        {
            _brandBl = brandBl;
        }

        public ApiResponse<List<Brand>> Get()
        {
            decimal timeInSeconds = DateTime.Now.Millisecond / 1000;
            var brands = _brandBl.GetAllBrands();
            return new ApiResponse<List<Brand>>()
            {
                Data = brands,
                DataFetched = brands.Count,
                ApiJourneyTime =
                    string.Format(Messages.ApiResponseTime,
                        Math.Abs(timeInSeconds - (decimal)DateTime.Now.Millisecond / 1000)),
                IsSuccess = true,
                Message = Messages.GetApiSuccessMessage
            };
        }
    }
}
