# Housing Gate
System for managing access to residential gates using RFID.

## Features
- RFID Card registration
- RFID Card deletion
- Person data management (Resident & Non Resident)
- RFID authentication
- Access log management
- Access reports
- User management (2 level)

## Security
- Data encryption (name, nik, phone, address)
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

## General Concept
1. Management alamat rumah (Blok dan Nomor rumah)
2. Management penghuni
3. Management Tamu (Buat kartu visitor)
4. Management RFID Card
5. Management RFID Reader
6. Management Camera
7. Management Motion Sensor
8. Management Gate
9. Management Access Log
10. Management User
12. Tamu bukan penghuni rumah akan dimintai Identitas (KTP/SIM/Pasport) dan diberikan Kartu Visitor untuk bisa akses masuk. ketika keluar perumahan, kartu ditukar kembali dengan Identitas (KTP/SIM/Pasport).
13. Satu penghuni bisa memiliki lebih dari satu kartu dan bebas keluar masuk tanpa perlu menunjukkan identitas.


## Backend
### Models
- User
    - id (int)
    - username (string)
    - password (string)
    - salt (string)
    - role (enum) // admin, user
    - status (enum) // active, inactive
    - created_at (datetimeoffset)
    - updated_at (datetimeoffset)
    - deleted_at (datetimeoffset)
- Person
    - id (int)
    - name (string)
    - resident_type (enum) // resident, nonresident
    - gender (enum?) // male, female, other
    - identity_type (enum?) // ktp, sim, passport
    - identity_number (string?)
    - address (string?)
    - phone (string?)
    - house_id (int?)
    - notes (string?)
    - created_at (datetimeoffset)
    - updated_at (datetimeoffset)
    - deleted_at (datetimeoffset)
    - status (enum) // active, inactive, deleted
- Card
    - id (guid)
    - person_id (int?)
    - card_uid (string)
    - card_type (enum) // resident, visitor, service
    - label (string?)
    - sequence_number (int?)
    - vehicle_type_id (int?)
    - vehicle_number (string?)
    - valid_from (datetimeoffset?)
    - valid_to (datetimeoffset?)
    - created_at (datetimeoffset)
    - updated_at (datetimeoffset)
    - deleted_at (datetimeoffset)
    - status (enum) // active, inactive, deleted
- Gate
    - id (int)
    - name (string)
    - code (string)
    - created_at (datetimeoffset)
    - updated_at (datetimeoffset)
    - deleted_at (datetimeoffset)
    - status (enum) // active, inactive
- EntryLog
    - id (int)
    - card_id (guid)
    - entry_time (datetimeoffset)
    - entry_gate_id (int)
    - entry_success (bool)
    - entry_error (string)
    - exit_time (datetimeoffset)
    - exit_gate_id (int)
    - exit_success (bool)
    - exit_error (string)
    - created_at (datetimeoffset)
- Device
    - id (int)
    - name (string)
    - ip_address (string)
    - type (enum) // rfid reader, camera, motion_sensor, gate_crossbar
    - port (int)
    - created_at (datetimeoffset)
    - updated_at (datetimeoffset)
    - deleted_at (datetimeoffset)
    - status (enum) // active, inactive

