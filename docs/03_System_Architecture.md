# System Architecture

**Project:** Fleet Transportation Management System (FTMS)
**Version:** 1.5

---

# 1. Purpose

This document describes the software architecture of the Fleet Transportation Management System (FTMS).

It defines the architectural style, system layers, project organization, dependency rules, and engineering principles used to build a maintainable and extensible transportation management platform.

The architecture supports current transportation operations while providing a foundation for future capabilities such as scheduling, notifications, reporting, and operational optimization.

---

# 2. Architectural Goals

FTMS architecture is designed with the following goals:

* Maintainability.
* Modularity.
* Testability.
* Business rule isolation.
* Low coupling.
* High cohesion.
* Extensibility.
* Clear separation of responsibilities.
* Incremental evolution.

The architecture prioritizes business value and maintainability over unnecessary technical complexity.

---

# 3. Architectural Style

FTMS uses a **Modular Monolith Architecture**.

The system is deployed as a single application while internally organized into separated business capabilities.

The architecture follows:

* Domain-Driven Design (DDD).
* Clean Architecture.
* SOLID Principles.
* Repository Pattern.
* Dependency Injection.

The Modular Monolith approach provides:

* Simpler deployment.
* Easier development and maintenance.
* Clear business boundaries.
* Strong domain separation.
* Future evolution capability.

Microservices are not treated as a default architecture goal.

Distributed architecture will only be considered when business scale, team structure, or operational requirements justify the additional complexity.

---

# 4. High-Level Architecture

FTMS follows a layered architecture based on Clean Architecture principles.

```text
+-----------------------------+
|          Angular UI         |
|       Presentation Layer    |
+-------------+---------------+
              |
              v
+-----------------------------+
|        FTMS.API             |
|        API Layer            |
+-------------+---------------+
              |
              v
+-----------------------------+
|    FTMS.Application         |
|   Use Cases & Workflows     |
+-------------+---------------+
              |
              v
+-----------------------------+
|      FTMS.Domain            |
| Business Rules & Entities   |
+-------------+---------------+
              |
              ^
+-----------------------------+
| FTMS.Infrastructure         |
| Database & External Services|
+-----------------------------+
```

The dependency direction follows the principle:

```text
Outer Layers
      |
      v
Inner Layers
```

Business logic remains independent from technical implementation details.

---

# 5. Solution Structure

The FTMS solution is organized into the following projects:

```text
FTMS

├── FTMS.Domain

├── FTMS.Application

├── FTMS.Infrastructure

├── FTMS.API

└── FTMS.UI
```

---

# 6. Layer Responsibilities

## FTMS.Domain

The Domain layer contains core business concepts and rules.

Responsibilities:

* Domain entities.
* Value objects.
* Domain validation.
* Business behavior.
* Domain rules.

Core domain concepts:

* Organization.
* Person.
* Vehicle.
* Booking.
* Booking Passenger.
* Trip.

The Domain layer has no dependency on external technologies.

---

## FTMS.Application

The Application layer coordinates business workflows.

Responsibilities:

* Use cases.
* Application services.
* DTO definitions.
* Business workflow orchestration.
* Repository interfaces.
* Authorization rules.

Examples:

* Create Booking.
* Assign Driver.
* Add Passenger.
* Confirm Booking.
* Create Trip.

The Application layer depends on the Domain layer.

---

## FTMS.Infrastructure

The Infrastructure layer provides technical implementations.

Responsibilities:

* Database access.
* Entity Framework Core configuration.
* Repository implementations.
* External service integration.

Examples:

* SQL Server integration.
* EF Core repositories.
* Data persistence.

---

## FTMS.API

The API layer exposes application capabilities.

Responsibilities:

* REST API endpoints.
* Authentication handling.
* Request validation.
* HTTP communication.
* API configuration.

The API layer communicates with Application workflows.

---

## FTMS.UI

The frontend application provides the user interface.

Technology:

* Angular 22.
* Angular Signals.
* Angular Material.

Responsibilities:

* User interaction.
* Data presentation.
* Workflow interaction.
* Dashboard visualization.

---

# 7. Domain Architecture

FTMS follows a business-centric domain model.

The main relationship:

```text
Organization
      |
      |
      +---- Person
      |
      +---- Vehicle
      |
      +---- Booking
               |
               |
               +---- Booking Passenger
               |
               +---- Trip
```

The domain model represents real transportation concepts rather than technical database structures.

---

# 8. Core Business Boundaries

## Organization Boundary

Organization represents the business ownership boundary.

Responsibilities:

* Own transportation resources.
* Isolate operational data.
* Define tenant scope.

---

## Person Boundary

Person represents real-world individuals.

Examples:

* Driver.
* Passenger.

Business roles are modeled within the Person domain.

---

## User Boundary

User represents application access.

Examples:

* Administrator.
* Dispatcher.

Security responsibilities are separated from business identity.

---

## Booking Boundary

Booking represents transportation planning.

Responsibilities:

* Transportation request.
* Vehicle assignment.
* Driver assignment.
* Passenger management.
* Booking lifecycle.

---

## Trip Boundary

Trip represents transportation execution.

Responsibilities:

* Execute confirmed bookings.
* Track operational progress.
* Maintain execution lifecycle.

---

# 9. Dependency Rules

FTMS follows strict dependency direction:

```text
Domain

↑

Application

↑

Infrastructure / API

↑

UI
```

Rules:

* Domain does not depend on other layers.
* Application depends on Domain.
* Infrastructure implements Application contracts.
* API exposes Application capabilities.
* UI communicates through API.

---

# 10. Design Principles

## Business First

Architecture decisions are driven by business needs.

---

## Avoid Premature Complexity

New patterns or technologies are introduced only when they solve real problems.

---

## Clear Responsibility

Each component should have one clear responsibility.

---

## Evolution Over Perfection

The system is designed to evolve safely as requirements grow.

---

# 11. Future Architecture Evolution

Future improvements may include:

* Domain Events.
* CQRS.
* Message brokers.
* Background processing.
* Event-driven architecture.
* Distributed services.

These improvements will be introduced only when business complexity requires them.

---

# 12. Related Documents

* Project Overview.
* Business Requirements.
* Domain Model.
* Database Design.
* API Design.
* Architecture Decision Records (ADR).
