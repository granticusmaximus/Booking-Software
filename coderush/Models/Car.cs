using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models
{
    public class Car : BaseModel
    {
        public int CarId { get; set; }
        public string CarName { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
    }

    public class CarViewModel
    {
        public int CarId { get; set; }
        public string CarName { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
    }
}
