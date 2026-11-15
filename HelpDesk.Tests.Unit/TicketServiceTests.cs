using HelpDesk.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HelpDesk.Tests.Unit
{
    public class TicketServiceTests
    {
        [Fact]
        public async Task CreateTicket_EmptyTitle_ThrowsDomainException()
        {
            var fakeRepo = new FakeTicketRepository();
            var service = new TicketService(fakeRepo);
            var invalidTicket = new Ticket { Title = "   " };
            var exception = await Assert.ThrowsAsync<DomainException>(
                () => service.CreateTicketAsync(invalidTicket)
            );
            Assert.Contains("Title", exception.Message);
        }

        [Fact]
        public async Task ChangeStatus_ClosedToNew_ThrowsDomainException()
        {
           
            var fakeRepo = new FakeTicketRepository();
            fakeRepo.Storage.Add(new Ticket { Id = 1, Status = Status.Closed });
            var service = new TicketService(fakeRepo);
            var exception = await Assert.ThrowsAsync<DomainException>(
                () => service.ChangeStatusAsync(1, Status.New, "Тестовый юзер")
            );

            Assert.Contains("Недопустимый переход", exception.Message);
        }
    }
}
