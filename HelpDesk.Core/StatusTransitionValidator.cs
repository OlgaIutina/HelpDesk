using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Core
{
    public class StatusTransitionValidator
    {
        public bool IsTransitionAllowed(Status from, Status to)
        {
          
            if (from == to) return false;
            if (from == Status.Closed || from == Status.Cancelled) return false;
            return (from, to) switch
            {
                (Status.New, Status.InProgress) => true,
                (Status.New, Status.Cancelled) => true,
                (Status.InProgress, Status.Resolved) => true,
                (Status.InProgress, Status.Cancelled) => true,
                (Status.Resolved, Status.Closed) => true,
                _ => false
            };
        }
    }
}
