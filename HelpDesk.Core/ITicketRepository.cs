using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Core
{
    public interface ITicketRepository
    {
        Task<Ticket?> GetByIdAsync(int id);
        Task<int> CreateAsync(Ticket ticket);
        Task UpdateAsync(Ticket ticket);
        Task AddStatusHistoryAsync(StatusHistory history);
    }
}
