using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models
{
    public class General : BaseModel
    {
        public int GeneralId { get; set; }
        public string GeneralName { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
    }

    public class GeneralViewModel
    {
        public int GeneralId { get; set; }
        public string GeneralName { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
    }
}
