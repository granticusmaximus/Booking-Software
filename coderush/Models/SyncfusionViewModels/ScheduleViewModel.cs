using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models.SyncfusionViewModels
{
    public class ScheduleViewModel<T> where T : class
    {
        public string key { get; set; }
        public string action { get; set; }
        public string antiForgery { get; set; }
        public List<T> added { get; set; }
        public List<T> changed { get; set; }
        public List<T> deleted { get; set; }
        public T value { get; set; }
    }
}
