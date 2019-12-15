using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models
{
    public class Bike
    {
        public int Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string Brand_Name { get; set; }
        public int Brand_Id { get; set; }
        public string Category_Name { get; set; }
        public int Category_Id { get; set; }
        public int Model_Year { get; set; }
        public int List_Price { get; set; }
        public int Quantity { get; set; }

    }
}
