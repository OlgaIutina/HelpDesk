using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Core
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Priority Priority { get; set; }
        public Status Status { get; set; } = Status.New; // По умолчанию New
        public DateTimeOffset CreatedAt { get; set; }

        public int? AssigneeId { get; set; }

        // История статусов
        public List<StatusHistory> StatusHistory { get; set; } = new();
    }
    public class StatusHistory
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public Status FromStatus { get; set; }
        public Status ToStatus { get; set; }
        public DateTimeOffset ChangedAt { get; set; }
        public string ChangedBy { get; set; } = string.Empty;
    }
}
