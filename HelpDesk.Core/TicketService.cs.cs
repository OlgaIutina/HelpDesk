using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Core
{
    public class TicketService
    {
        private readonly ITicketRepository _repository;
        private readonly StatusTransitionValidator _validator;

        public TicketService(ITicketRepository repository)
        {
            _repository = repository;
            _validator = new StatusTransitionValidator();
        }

        public async Task<int> CreateTicketAsync(Ticket ticket)
        {
           
            if (string.IsNullOrWhiteSpace(ticket.Title))
                throw new DomainException("Заголовок (Title) не может быть пустым.");

            if (ticket.Description != null && ticket.Description.Length > 2000)
                throw new DomainException("Описание не может превышать 2000 символов.");
            if (ticket.CreatedAt == default)
                ticket.CreatedAt = DateTimeOffset.UtcNow;
            return await _repository.CreateAsync(ticket);
        }

        public async Task ChangeStatusAsync(int ticketId, Status newStatus, string changedBy)
        {
            var ticket = await _repository.GetByIdAsync(ticketId);
            if (ticket == null)
                throw new NotFoundException($"Заявка с ID {ticketId} не найдена.");
            if (!_validator.IsTransitionAllowed(ticket.Status, newStatus))
                throw new DomainException($"Недопустимый переход из {ticket.Status} в {newStatus}.");
            var history = new StatusHistory
            {
                TicketId = ticketId,
                FromStatus = ticket.Status,
                ToStatus = newStatus,
                ChangedAt = DateTimeOffset.UtcNow,
                ChangedBy = changedBy
            };
            ticket.Status = newStatus;
            await _repository.AddStatusHistoryAsync(history);
            await _repository.UpdateAsync(ticket);
        }
    }
}
