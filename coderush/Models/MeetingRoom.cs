using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models
{
    public class MeetingRoom : BaseModel
    {
        public int MeetingRoomId { get; set; }
        public string MeetingRoomName { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
    }

    public class MeetingRoomViewModel
    {
        public int MeetingRoomId { get; set; }
        public string MeetingRoomName { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
    }
}
