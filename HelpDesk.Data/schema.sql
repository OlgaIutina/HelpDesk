CREATE TABLE employees (
    id SERIAL PRIMARY KEY,
    full_name VARCHAR(200) NOT NULL,
    position VARCHAR(100) NOT NULL,
    department VARCHAR(100) NOT NULL,
    is_deleted BOOLEAN DEFAULT FALSE
);

CREATE TABLE equipment (
    id SERIAL PRIMARY KEY,
    inventory_number VARCHAR(50) UNIQUE NOT NULL,
    name VARCHAR(200) NOT NULL,
    department VARCHAR(100) NOT NULL,
    is_deleted BOOLEAN DEFAULT FALSE
);

CREATE TABLE tickets (
    id SERIAL PRIMARY KEY,
    author_id INT NOT NULL,
    equipment_id INT NOT NULL,
    title VARCHAR(200) NOT NULL CHECK (title <> ''),
    description VARCHAR(2000) CHECK (length(description) <= 2000),
    priority VARCHAR(20) NOT NULL CHECK (priority IN ('Low', 'Normal', 'High', 'Critical')),
    status VARCHAR(20) NOT NULL CHECK (status IN ('New', 'InProgress', 'Resolved', 'Closed', 'Cancelled')),
    created_at TIMESTAMPTZ NOT NULL,
    assignee_id INT
);

CREATE INDEX idx_tickets_status ON tickets(status);
CREATE INDEX idx_tickets_priority ON tickets(priority);
CREATE INDEX idx_tickets_created_at ON tickets(created_at);
CREATE INDEX idx_tickets_assignee_id ON tickets(assignee_id);

CREATE TABLE ticket_status_history (
    id SERIAL PRIMARY KEY,
    ticket_id INT NOT NULL REFERENCES tickets(id),
    from_status VARCHAR(20) NOT NULL,
    to_status VARCHAR(20) NOT NULL,
    changed_at TIMESTAMPTZ NOT NULL,
    changed_by VARCHAR(200) NOT NULL
);