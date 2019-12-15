using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ApiResponse<T>:ApiResponse
    {
        public T Data { get; set; }
        public int DataFetched { get; set; }
        
    }

    public class ApiResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string ApiJourneyTime { get; set; }
    }
}
