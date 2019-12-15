using System;
using System.Collections.Generic;
using System.Web.Http;
using BusinessLogic;
using CommonClasses;
using Models;

namespace BikeStore.Controllers
{
    public class LogController : ApiController
    {
        private readonly ILogBl _logBl;

        public LogController(ILogBl logBl)
        {
            _logBl = logBl;
        }

        public ApiResponse<List<string>> Get()
        {
            decimal timeInSeconds = DateTime.Now.Millisecond / 1000;
            var logs = _logBl.GetLogs();
            return new ApiResponse<List<string>>()
            {
                Data = logs,
                DataFetched = logs.Count,
                ApiJourneyTime =
                    string.Format(Messages.ApiResponseTime,
                        Math.Abs(timeInSeconds - (decimal)DateTime.Now.Millisecond / 1000)),
                IsSuccess = true,
                Message = Messages.GetApiSuccessMessage
            };
        } 
    }
}
