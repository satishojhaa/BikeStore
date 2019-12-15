using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Bike
    {
        public int BikeId { get; set; }
        public string BikeName { get; set; }
        public string BrandMake { get; set; }
        public string CategoryOfBike { get; set; }
        public int YearOfMfg { get; set; }
        public int Price { get; set; }
        public int QuantityInStock { get; set; }
    }
}
