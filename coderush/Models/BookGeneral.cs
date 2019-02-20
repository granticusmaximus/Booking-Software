using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models
{
    public class BookGeneral : BaseModel
    {
        public int BookGeneralId { get; set; }
        public string Subject { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public string StartTimeZone { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public string EndTimeZone { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public bool AllDay { get; set; }
        public bool Recurrence { get; set; }
        public string RecurrenceRule { get; set; }
        public string Categorize { get; set; }
        public string Priority { get; set; }
        public int OwnerId { get; set; }
        public int RecurrenceId { get; set; }
        public DateTimeOffset RecurrenceExDate { get; set; }
        public int GeneralId { get; set; }
    }

    public class BookGeneralViewModel
    {
        public int BookGeneralId { get; set; }
        public string Subject { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public string StartTimeZone { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public string EndTimeZone { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public bool AllDay { get; set; }
        public bool Recurrence { get; set; }
        public string RecurrenceRule { get; set; }
        public string Categorize { get; set; }
        public string Priority { get; set; }
        public int OwnerId { get; set; }
        public int RecurrenceId { get; set; }
        public DateTimeOffset RecurrenceExDate { get; set; }
        public int GeneralId { get; set; }
    }
}
