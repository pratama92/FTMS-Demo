# Fleet Transportation Management System (FTMS)

> A transportation management platform built to demonstrate practical software architecture, domain modeling, and modern .NET application development.

---

# Overview

Fleet Transportation Management System (FTMS) helps organizations manage fleet-based transportation operations through a centralized platform.

The system manages:

- Organizations
- People
- Vehicles
- Transportation bookings
- Passenger assignments
- Trip execution workflow

FTMS is built as a Solution Architecture portfolio project demonstrating how a real business system can be designed, developed, and evolved.

---

# Demo Overview

The FTMS Demo demonstrates the complete transportation workflow:

```
Vehicle Management
        |
        |
Booking Creation
        |
        |
Driver & Passenger Assignment
        |
        |
Booking Confirmation
        |
        |
Trip Execution
        |
        |
Dispatcher Dashboard
```

Available features:

- Vehicle management
- Person management
- Booking workflow
- Driver assignment
- Passenger management
- Trip execution foundation
- Dispatcher timeline dashboard

---

# Architecture

FTMS follows a pragmatic architecture approach:

- Domain-Driven Design (DDD)
- Clean Architecture
- Modular Monolith
- SOLID Principles
- Repository Pattern
- Dependency Injection

The goal is to keep business rules clear, maintainable, and ready to evolve as requirements grow.

Current architecture:

```
Angular Frontend

        |

ASP.NET Core API

        |

Application Layer

        |

Domain Layer

        |

Infrastructure Layer

        |

SQL Server
```

---

# Technology Stack

## Backend

- ASP.NET Core (.NET 10)
- Entity Framework Core
- SQL Server

## Frontend

- Angular
- Angular Material

## Infrastructure

- Docker
- Docker Compose
- Nginx

---

# Running the Demo

Requirements:

- Docker Desktop

Run:

```bash
docker compose up --build
```

The demo environment includes:

- Angular frontend
- ASP.NET Core backend
- SQL Server database
- Nginx hosting

---

# Project Status

FTMS Version 1 foundation is completed.

Implemented:

- Core domain modules
- Booking lifecycle
- Passenger assignment
- Driver assignment
- Trip workflow foundation
- Dispatcher dashboard
- Docker deployment

Future evolution areas:

- Transportation scheduling
- Recurring trips
- Waiting list
- Notifications
- Reporting and analytics

---

# Engineering Philosophy

FTMS follows a simple principle:

> Build systems that solve real business problems, remain understandable, and evolve with confidence.

Architecture is a tool to support business value, not the goal itself.