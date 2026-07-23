# Business Requirements

**Project:** Fleet Transportation Management System (FTMS)
**Project Version:** V1 Foundation
**Version:** 1.5

---

# 1. Purpose

This document defines the business requirements for the Fleet Transportation Management System (FTMS).

The purpose of this document is to describe the transportation business context, operational workflows, domain rules, and functional capabilities supported by the system.

This document serves as a foundation for:

* Domain modeling.
* Application workflows.
* System architecture decisions.
* Future business expansion.

---

# 2. Business Context

Organizations operating transportation services need to coordinate vehicles, drivers, passengers, and transportation requests efficiently.

As transportation operations grow, manual processes or disconnected systems can create challenges such as:

* Difficulty tracking fleet resources.
* Manual transportation coordination.
* Driver and vehicle assignment conflicts.
* Limited visibility into transportation execution.
* Increasing operational complexity.

FTMS provides a centralized platform to manage transportation planning and execution through structured business workflows.

---

# 3. Stakeholders

| Stakeholder   | Responsibility                                                                                                 |
| ------------- | -------------------------------------------------------------------------------------------------------------- |
| Organization  | Owns transportation resources, operational data, and business activities.                                      |
| Dispatcher    | Manages transportation requests, bookings, vehicle assignments, driver assignments, and operational readiness. |
| Driver        | Operates assigned transportation vehicles and performs transportation services.                                |
| Passenger     | Uses transportation services provided by the organization.                                                     |
| Administrator | Manages application access, configuration, and system administration.                                          |

---

# 4. Business Objectives

FTMS aims to:

* Centralize transportation operations.
* Improve fleet resource visibility.
* Simplify transportation request and booking processes.
* Improve vehicle and driver utilization.
* Maintain accurate passenger information.
* Provide clear transportation lifecycle tracking.
* Establish a foundation for future transportation capabilities.

---

# 5. Business Capabilities

## Organization Management

Ability to manage transportation organizations.

Responsibilities:

* Define organization boundaries.
* Own operational data.
* Provide tenant separation for business information.

---

## Person Management

Ability to manage transportation participants.

A Person represents a real-world individual within the organization.

A Person may participate as:

* Passenger.
* Driver.

Business roles belong to the Person domain.

---

## Vehicle Management

Ability to manage transportation fleet assets.

Responsibilities:

* Maintain vehicle information.
* Track vehicle availability.
* Manage capacity information.
* Support vehicle assignment.

---

## Booking Management

Ability to plan transportation services.

A Booking represents a planned transportation request.

Responsibilities:

* Create transportation requests.
* Assign vehicles.
* Assign drivers.
* Manage passengers.
* Manage booking lifecycle.

---

## Trip Management

Ability to execute transportation services.

A Trip represents the execution of a confirmed Booking.

Responsibilities:

* Create trips from confirmed bookings.
* Track transportation execution.
* Record trip completion.
* Maintain execution history.

---

# 6. Transportation Workflow

The FTMS transportation workflow:

```text
Transportation Request

        ↓

Dispatcher Creates Booking

        ↓

Passenger Assignment

        ↓

Vehicle Assignment

        ↓

Driver Assignment

        ↓

Booking Confirmation

        ↓

Trip Creation

        ↓

Trip Execution

        ↓

Trip Completion
```

Cancellation may occur before trip completion.

---

# 7. Business Rules

| ID     | Rule                                                               |
| ------ | ------------------------------------------------------------------ |
| BR-001 | A Booking belongs to exactly one Organization.                     |
| BR-002 | A Vehicle belongs to one Organization.                             |
| BR-003 | A Person belongs to one Organization.                              |
| BR-004 | A Booking requires a Vehicle before confirmation.                  |
| BR-005 | A Booking may contain multiple Passengers.                         |
| BR-006 | A Booking requires at least one Passenger before confirmation.     |
| BR-007 | A Booking requires an assigned Driver before confirmation.         |
| BR-008 | An assigned Driver cannot also be a passenger in the same Booking. |
| BR-009 | Passengers cannot be modified after Booking confirmation.          |
| BR-010 | Driver assignment cannot be changed after Booking confirmation.    |
| BR-011 | A Confirmed Booking cannot return to Pending state.                |
| BR-012 | A Completed Booking cannot be modified.                            |
| BR-013 | A Cancelled Booking cannot proceed to Trip execution.              |
| BR-014 | A Trip is created only from a Confirmed Booking.                   |
| BR-015 | Trip execution data originates from the confirmed Booking.         |

---

# 8. Operational Policies

## Booking

A Booking represents a planned transportation service.

A Booking contains:

* Organization.
* Vehicle.
* Driver.
* Passengers.
* Destination.
* Estimated departure time.
* Estimated arrival time.

Booking lifecycle:

```text
Created

↓

Pending

↓

Confirmed

↓

Completed
```

Alternative flow:

```text
Created / Pending

↓

Cancelled
```

---

## Driver

Drivers are registered Persons.

Responsibilities:

* Operate assigned vehicles.
* Perform transportation services.

Driver assignment is managed by the Dispatcher.

---

## Passenger

Passengers can be:

* Registered Persons.
* Guest passengers.

Passengers may:

* Join a Booking.
* Be removed before confirmation.
* Have pickup locations.

---

## Dispatcher

Dispatcher is responsible for transportation coordination.

Responsibilities:

* Create bookings.
* Assign vehicles.
* Assign drivers.
* Manage passengers.
* Confirm transportation readiness.
* Monitor operational status.

---

## User Access

Application access is separated from business identity.

A User represents system access.

Examples:

* Administrator.
* Dispatcher.

A Person represents the real-world individual.

This separation allows business roles and security roles to evolve independently.

---

# 9. Future Business Capabilities

Future versions may introduce:

* Transportation scheduling.
* Recurring transportation services.
* Waiting list management.
* Passenger attendance.
* Route management.
* Notification services.
* Operational reporting.
* Maintenance management.
* GPS tracking.
* Automated dispatching.

---

# 10. Assumptions

The system assumes:

* Organizations operate their own transportation resources.
* Vehicles belong to organizations.
* Drivers are registered Persons.
* Passengers can be registered Persons or Guests.
* Transportation operations are managed by authorized Users.
* Historical operational records should be preserved.

---

# 11. Out of Scope (V1)

The following features are excluded from V1:

* Online payment.
* Mobile applications.
* GPS tracking.
* Route optimization.
* Maintenance management.
* AI dispatching.
* External system integrations.
* Advanced analytics.

---

# 12. Related Documents

* Project Overview.
* Domain Model.
* System Architecture.
* Database Design.
* API Design.
* Architecture Decision Records (ADR).
