using HelpDesk.Core;
using HelpDesk.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Tests.Unit
{
    public class DatabaseTests
    {
        private const string ConnectionString = "Host=localhost;Port=5432;Database=helpdesk;Username=postgres;Password=12345";

        [Fact]
        public async Task CreateAndFetchTicket_FromRealDatabase_WorksCorrectly()
        {
            var repository = new TicketRepository(ConnectionString);
            var newTicket = new Ticket
            {
                Title = "Интеграционный тест: Сломался принтер",
                Description = "Не печатает вообще",
                Priority = Priority.High,
                CreatedAt = DateTimeOffset.UtcNow
            };
            var createdId = await repository.CreateAsync(newTicket);
            var fetchedTicket = await repository.GetByIdAsync(createdId);
            Assert.NotNull(fetchedTicket);
            Assert.Equal("Интеграционный тест: Сломался принтер", fetchedTicket.Title);
            Assert.Equal(Priority.High, fetchedTicket.Priority);
            Assert.Equal(Status.New, fetchedTicket.Status);
        }
    }
}
