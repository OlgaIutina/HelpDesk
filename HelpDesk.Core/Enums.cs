using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Core
{
    public enum Priority
    {
        Low,
        Normal,
        High,
        Critical
    }

    // Статусы из матрицы переходов
    public enum Status
    {
        New,
        InProgress,
        Resolved,
        Closed,
        Cancelled
    }
}
