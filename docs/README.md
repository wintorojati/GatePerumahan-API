# Housing Gate
System for managing access to residential gates using RFID.

## Features
- RFID registration
- RFID deletion
- Member data management
- RFID authentication
- Access log management
- Access reports
- User management (2 level)

## Security
- Data encryption (name, nik, phone, address, license_plate_number)
- Access authorization
- User authentication
- Input validation
- Data backup
- Data restore
- Audit trail

## Technology
- Backend: .NET 9.0 (C#)
    - Vertical Slice Architecture
    - Entity Framework
    - MediatR
    - FluentValidation
    - AutoMapper
    - Serilog
    - JWT
    - Minimal API
- Frontend: 
    - NextJs
    - TailwindCSS
    - Shadcn UI
- Database: PostgreSQL
- RFID Reader: Arduino UNO + W5100 Shield + MFRC522
- RFID Card: Mifare Classic 1K
- Camera: IP Camera
- Motion Sensor: PIR

## Components
- Entry gate 1x
- Exit gate 1x
- RFID Reader 2x
- RFID Card 100pcs
- Camera 1x
- Motion Sensor 1x
- Computer Set 1x
- Switch Hub min 5 ports 1x

## Backend
### Models
- User
    - id (int)
    - username (string)
    - password (string)
    - salt (string)
    - role (enum) // admin, user
    - created_at (datetime)
    - updated_at (datetime)
    - deleted_at (datetime)
    - status (enum) // active, inactive
- Persons
    - id (int)
    - name (string)
    - gender (char)
    - nik (string)
    - address (string)
    - phone (string)
    - license_plate_number (string)
    - created_at (datetime)
    - updated_at (datetime)
    - deleted_at (datetime)
    - status (enum) // active, inactive
- Card
    - id (guid)
    - person_id (int)
    - card_uid (string)
    - access_type (enum) // person, car, motorcycle
    - created_at (datetime)
    - updated_at (datetime)
    - deleted_at (datetime)
    - status (enum) // active, inactive
- Gate
    - id (int)
    - name (string)
    - code (string)
    - created_at (datetime)
    - updated_at (datetime)
    - deleted_at (datetime)
    - status (enum) // active, inactive
- EntryLog
    - id (int)
    - card_id (guid)
    - entry_time (datetime)
    - entry_gate_id (int)
    - entry_success (bool)
    - entry_error (string)
    - exit_time (datetime)
    - exit_gate_id (int)
    - exit_success (bool)
    - exit_error (string)
    - created_at (datetime)
- Device
    - id (int)
    - name (string)
    - ip_address (string)
    - type (enum) // rfid reader, camera, motion_sensor, gate_crossbar
    - port (int)
    - created_at (datetime)
    - updated_at (datetime)
    - deleted_at (datetime)
    - status (enum) // active, inactive
