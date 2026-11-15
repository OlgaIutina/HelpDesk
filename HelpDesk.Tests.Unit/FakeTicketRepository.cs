using System;
using System.Collections.Generic;
using System.Linq;
using HelpDesk.Core;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Tests.Unit
{
    public class FakeTicketRepository : ITicketRepository
    {
        public List<Ticket> Storage { get; } = new();

        public Task<Ticket?> GetByIdAsync(int id)
        {
            return Task.FromResult(Storage.FirstOrDefault(t => t.Id == id));
        }

        public Task<int> CreateAsync(Ticket ticket)
        {
            ticket.Id = Storage.Count + 1;
            Storage.Add(ticket);
            return Task.FromResult(ticket.Id);
        }

        public Task UpdateAsync(Ticket ticket)
        {       
            return Task.CompletedTask;
        }

        public Task AddStatusHistoryAsync(StatusHistory history)
        {
            return Task.CompletedTask;
        }
    }
}
