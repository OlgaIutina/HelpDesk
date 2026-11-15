using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using HelpDesk.Core;

namespace HelpDesk.Data
{
    public class TicketRepository : ITicketRepository
    {
        private readonly string _connectionString;

        // В конструкторе мы передаем строку подключения к нашей БД
        public TicketRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Создаем соединение (Dapper сам его откроет и закроет)
        private IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);

        public async Task<Ticket?> GetByIdAsync(int id)
        {
            using var connection = CreateConnection();
            // Параметризованный запрос (защита от SQL-инъекций)
            var sql = "SELECT id, title, description, priority, status, created_at AS CreatedAt, assignee_id AS AssigneeId FROM tickets WHERE id = @Id";

            var ticket = await connection.QuerySingleOrDefaultAsync<Ticket>(sql, new { Id = id });
            return ticket;
        }

        public async Task<int> CreateAsync(Ticket ticket)
        {
            using var connection = CreateConnection();
            // Приводим enum к строке, чтобы база поняла
            var sql = @"
            INSERT INTO tickets (author_id, equipment_id, title, description, priority, status, created_at, assignee_id) 
            VALUES (@AuthorId, @EquipmentId, @Title, @Description, @Priority, @Status, @CreatedAt, @AssigneeId)
            RETURNING id;";

            // В упрощенном виде ставим author_id = 1 и equipment_id = 1 (для лабы сойдет)
            var parameters = new
            {
                AuthorId = 1,
                EquipmentId = 1,
                ticket.Title,
                ticket.Description,
                Priority = ticket.Priority.ToString(),
                Status = ticket.Status.ToString(),
                CreatedAt = ticket.CreatedAt,
                ticket.AssigneeId
            };

            var newId = await connection.ExecuteScalarAsync<int>(sql, parameters);
            return newId;
        }

        public async Task UpdateAsync(Ticket ticket)
        {
            using var connection = CreateConnection();
            var sql = "UPDATE tickets SET status = @Status, assignee_id = @AssigneeId WHERE id = @Id";

            await connection.ExecuteAsync(sql, new
            {
                Status = ticket.Status.ToString(),
                ticket.AssigneeId,
                ticket.Id
            });
        }

        public async Task AddStatusHistoryAsync(StatusHistory history)
        {
            using var connection = CreateConnection();
            var sql = @"
            INSERT INTO ticket_status_history (ticket_id, from_status, to_status, changed_at, changed_by) 
            VALUES (@TicketId, @FromStatus, @ToStatus, @ChangedAt, @ChangedBy)";

            await connection.ExecuteAsync(sql, new
            {
                history.TicketId,
                FromStatus = history.FromStatus.ToString(),
                ToStatus = history.ToStatus.ToString(),
                history.ChangedAt,
                history.ChangedBy
            });
        }

    }
}
