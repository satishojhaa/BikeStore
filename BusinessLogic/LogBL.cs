using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Transactions;

namespace BusinessLogic
{
    public interface ILogBl
    {
        List<string> GetLogs();
    }
    public class LogBL:ILogBl
    {
        private readonly ILogDal _logDal;

        public LogBL(ILogDal logDal)
        {
            _logDal = logDal;
        }
        public List<string> GetLogs()
        {
            return _logDal.GetLogs();
        } 
    }
}
