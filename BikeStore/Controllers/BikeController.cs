using System;
using System.Collections.Generic;
using System.Web.Http;
using BusinessLogic;
using CommonClasses;
using Models;

namespace BikeStore.Controllers
{
    public class BikeController : ApiController
    {
        private readonly IBikeBL _bikeBl;

        public BikeController(IBikeBL bikeBl)
        {
            _bikeBl = bikeBl;
        }

        [HttpGet]
        public ApiResponse<List<Bike>> Get()
        {
            decimal timeInSeconds = DateTime.Now.Millisecond/1000;
            var data = _bikeBl.GetAllBikes();
            return new ApiResponse<List<Bike>>()
            {
                Data = data,
                DataFetched = data.Count,
                ApiJourneyTime =
                    string.Format(Messages.ApiResponseTime,
                        Math.Abs(timeInSeconds - (decimal) DateTime.Now.Millisecond/1000)),
                IsSuccess = true,
                Message = Messages.GetApiSuccessMessage
            };
        }

        [HttpGet]
        public ApiResponse<Bike> Get(int id)
        {
            decimal timeInSeconds = DateTime.Now.Millisecond / 1000;
            var bike = _bikeBl.GetBikeById(id);
            var success = bike != null;
            var message = bike != null ? Messages.GetApiSuccessMessage : Messages.GetApiFailureMessage;
            return new ApiResponse<Bike>()
            {
                Data = bike,
                DataFetched = bike !=null ? 1:0,
                ApiJourneyTime =
                    string.Format(Messages.ApiResponseTime,
                        Math.Abs(timeInSeconds - (decimal)DateTime.Now.Millisecond / 1000)),
                IsSuccess = success,
                Message = message
            };
        }

        [HttpGet]
        public ApiResponse<Bike> Get(string bikeName)
        {
            decimal timeInSeconds = DateTime.Now.Millisecond / 1000;
            var bike = _bikeBl.GetBikeByName(bikeName);
            var success = bike!=null;
            var message = bike != null ? Messages.GetApiSuccessMessage : Messages.GetApiFailureMessage;
            return new ApiResponse<Bike>()
            {
                Data = bike,
                DataFetched = bike != null ? 1 : 0,
                ApiJourneyTime =
                    string.Format(Messages.ApiResponseTime,
                        Math.Abs(timeInSeconds - (decimal)DateTime.Now.Millisecond / 1000)),
                IsSuccess = success,
                Message = message
            };
        }

        [HttpPut]
        public ApiResponse Put(Bike newBike)
        {
            decimal timeInSeconds = DateTime.Now.Millisecond / 1000;
            _bikeBl.Update(newBike);
            return new ApiResponse()
            {
                ApiJourneyTime =
                    string.Format(Messages.ApiResponseTime,
                        Math.Abs(timeInSeconds - (decimal)DateTime.Now.Millisecond / 1000)),
                IsSuccess = true,
                Message = Messages.UpdateApiSuccessMessage
            };
        }

        [HttpPost]
        public ApiResponse<Bike> Post(Bike newBike)
        {
            decimal timeInSeconds = DateTime.Now.Millisecond / 1000;
            var newlyInsertedBike = _bikeBl.Insert(newBike);
            return new ApiResponse<Bike>()
            {
                Data = newlyInsertedBike,
                DataFetched = newlyInsertedBike !=null? 1:0,
                ApiJourneyTime =
                    string.Format(Messages.ApiResponseTime,
                        Math.Abs(timeInSeconds - (decimal)DateTime.Now.Millisecond / 1000)),
                IsSuccess = true,
                Message = Messages.InsertApiSuccessMessage
            };
        }

        [HttpDelete]
        public ApiResponse Delete(int bikeId)
        {
            decimal timeInSeconds = DateTime.Now.Millisecond / 1000;
            _bikeBl.Delete(bikeId);
            return new ApiResponse()
            {
                ApiJourneyTime =
                    string.Format(Messages.ApiResponseTime,
                        Math.Abs(timeInSeconds - (decimal)DateTime.Now.Millisecond / 1000)),
                IsSuccess = true,
                Message = Messages.DeleteApiSuccessMessage
            };
        }
    }
}
