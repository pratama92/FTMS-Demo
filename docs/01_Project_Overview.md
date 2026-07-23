# Fleet Transportation Management System (FTMS)

**Project Version:** V1 Foundation
**Version:** 1.5


---

# 1. Introduction

Fleet Transportation Management System (FTMS) is a web-based transportation management platform designed to help organizations manage fleet-based transportation operations through a centralized system.

The platform manages transportation resources, people, vehicles, bookings, and trip execution workflows while providing a maintainable foundation for future business expansion.

FTMS focuses on solving operational challenges commonly faced by organizations that operate structured transportation services.

---

# 2. Background

Many transportation operations still rely on spreadsheets, manual processes, or disconnected applications to manage:

* Vehicles.
* Drivers.
* Passengers.
* Transportation requests.
* Operational assignments.

As transportation operations grow, these approaches can create challenges such as:

* Difficulty tracking fleet resources.
* Manual booking coordination.
* Driver and vehicle assignment conflicts.
* Limited visibility into transportation execution.
* Increasing operational complexity.

FTMS addresses these challenges by providing a centralized platform with structured business workflows and clear domain boundaries.

---

# 3. Project Summary

FTMS manages the transportation lifecycle from planning to execution.

```text
Organization
      |
People
      |
Vehicles
      |
Booking
      |
Trip Execution
```

The system applies Domain-Driven Design principles by representing business responsibilities through clear domain models.

The current V1 foundation includes:

* Organization Management.
* Person Management.
* Vehicle Management.
* Booking Management.
* Trip Execution Foundation.
* User Authentication Foundation.

FTMS is implemented as a Modular Monolith, allowing future expansion without introducing unnecessary architectural complexity.

---

# 4. Demo Overview

The FTMS Demo demonstrates the complete transportation workflow from resource management to operational execution.

```text
Organization
      |
Person Management
      |
Vehicle Management
      |
Booking Creation
      |
Driver Assignment
      |
Passenger Assignment
      |
Trip Execution
      |
Dispatcher Dashboard
```

The demo showcases how business requirements flow through:

```text
Domain Model
      |
Application Workflow
      |
REST API
      |
Angular Application
      |
Operational Dashboard
```

The goal of the demo is to demonstrate a complete business workflow rather than only individual technical components.

---

# 5. Vision

To build a modern, maintainable transportation management platform that supports organizations operating fleet-based transportation services.

Potential business areas include:

* Employee transportation services.
* Mining and industrial transportation operations.
* Corporate shuttle services.
* Intercity transportation providers.
* Tourism transportation providers.
* Fleet service providers.

FTMS also serves as a practical Solution Architecture portfolio project demonstrating enterprise software engineering principles using modern .NET technologies.

---

# 6. Objectives

The objectives of FTMS are:

* Centralize transportation operations.
* Improve vehicle and driver utilization.
* Simplify transportation request management.
* Reduce operational conflicts.
* Provide clear transportation workflows.
* Build a maintainable platform that can evolve with business needs.

---

# 7. Target Users

FTMS is designed for organizations that operate structured transportation services, especially those managing multiple vehicles, drivers, passengers, and transportation schedules.

Potential users include:

* Companies providing employee transportation services.
* Organizations operating internal shuttle transportation.
* Mining and industrial companies with workforce transportation needs.
* Corporate transportation providers.
* Intercity and scheduled transportation operators.
* School transportation providers.
* Tourism and fleet service providers.

FTMS is suitable for organizations that require centralized management of:

* Transportation resources.
* Vehicle and driver assignments.
* Passenger information.
* Transportation bookings.
* Operational execution workflows.

---

# 8. V1 Scope

The current FTMS V1 includes:

## Organization Management

Capabilities:

* Manage transportation organizations.
* Provide organization boundaries for business data.

---

## Person Management

Capabilities:

* Manage transportation participants.
* Support business roles such as passenger and driver.

---

## Vehicle Management

Capabilities:

* Manage fleet assets.
* Maintain vehicle information.
* Provide vehicle availability foundation.

---

## Booking Management

Capabilities:

* Create transportation requests.
* Assign vehicles.
* Assign drivers.
* Manage regular and guest passengers.
* Handle booking lifecycle.

---

## Trip Management

Capabilities:

* Execute confirmed bookings.
* Track transportation execution lifecycle.
* Support trip start, completion, and cancellation workflow.

---

## Authentication

Capabilities:

* User authentication.
* Role-based access foundation.

---

# 9. Future Scope

The following capabilities are planned for future development:

* Transportation scheduling.
* Recurring trips.
* Route management.
* Passenger attendance.
* Notification services.
* Reporting and analytics.
* GPS tracking.
* Fleet optimization.

---

# 10. Architecture Principles

FTMS follows these engineering principles:

## Domain-Driven Design

Business rules are modeled inside the domain layer.

Core concepts include:

* Organization.
* Person.
* Vehicle.
* Booking.
* Trip.

Business behavior and validation remain close to the domain model.

---

## Clean Architecture

The system separates responsibilities into:

```text
API

Application

Domain

Infrastructure
```

Each layer has a clear responsibility and dependency direction.

---

## Pragmatic Engineering

FTMS avoids unnecessary complexity.

Design decisions are driven by:

* Business requirements.
* Maintainability.
* Simplicity.
* Long-term evolution.

Architecture exists to support business value.

---

# 11. Technology Stack

## Backend

* .NET 10.
* ASP.NET Core Web API.
* C#.
* Entity Framework Core.

## Frontend

* Angular 22.
* Angular Signals.
* Angular Material.

## Database

* SQL Server.

## Testing

* xUnit.
* FluentAssertions.
* Moq.

## Development Tools

* Git.
* GitHub.
* Docker.
* Docker Compose.

---

# 12. Development Status

Current FTMS V1 status:

```text
Backend

✅ Domain Model
✅ Application Workflows
✅ REST API
✅ Authentication Foundation


Frontend

✅ Angular Application
✅ Core Modules
✅ Booking Workflow
✅ Driver and Passenger Assignment
✅ Trip Workflow Foundation
✅ Dispatcher Dashboard


Quality

✅ Domain Unit Tests
```

---

# 13. Long-Term Roadmap

## Version 1 - Core Transportation Management

Completed:

* Organization Management.
* Person Management.
* Vehicle Management.
* Booking Management.
* Trip Management Foundation.
* Authentication Foundation.

---

## Version 2 - Operational Expansion

Planned:

* Transportation Schedule.
* Recurring Trips.
* Route Management.
* Passenger Attendance.
* Notification Services.

---

## Version 3 - Business Intelligence

Planned:

* Dashboard Improvements.
* Operational Reports.
* Fleet Utilization Analytics.
* Maintenance Management.

---

## Version 4 - Enterprise Scaling

Potential future improvements:

* Event-driven architecture.
* Message broker integration.
* CQRS where business complexity requires it.
* Distributed processing.

---

## Version 5 - Cloud & Platform Evolution

Potential future improvements:

* Cloud deployment.
* Container orchestration.
* Advanced monitoring.
* High availability architecture.

---

# Document Information

| Item         | Value            |
| ------------ | ---------------- |
| Document     | Project Overview |
| Version      | 1.5              |
| Status       | Active           |
| Author       | Indra Pratama    |
| Last Updated | July 2026        |
