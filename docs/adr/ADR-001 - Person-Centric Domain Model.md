# ADR-001 - Person Identity and Responsibility Separation

## Status

Accepted

---

## Context

The system manages multiple people involved in transportation operations, including:

* Drivers
* Passengers
* Dispatchers
* Administrators
* Other business contacts

During business analysis, it was identified that the same individual may participate in different transportation contexts over time.

Examples:

* A driver may travel as a passenger on another booking.
* A passenger may later become a driver.
* A person may have multiple responsibilities within an organization.

Creating separate master records for each responsibility would duplicate identity data and make maintenance difficult.

---

## Decision

The system will maintain a single **Person** entity as the identity representation of a human within an **Organization boundary**.

A Person always belongs to one Organization.

Person is responsible for:

* Identity information
* Organization ownership
* Business capabilities through PersonRole

The model separates identity, system access, and transportation participation.

```text
Organization
 |
 +-- Person
       |
       +-- PersonRole
       |      |
       |      +-- Driver
       |      +-- Passenger
       |
       +-- User
              |
              +-- UserRole
                     |
                     +-- PlatformAdmin
                     +-- Admin
                     +-- Dispatcher

Booking
 |
 +-- BookingPassenger
        |
        +-- Person reference OR Guest information
```

---

## Responsibility Separation

### Person

Represents the identity of a human within an organization.

Person stores:

* Name
* Contact information
* Organization ownership
* Business roles through PersonRole

Person does not store:

* Authentication credentials
* System access permissions

---

### PersonRole

Represents transportation-related capabilities or responsibilities.

Examples:

```text
Person
 |
 +-- PersonRole
        |
        +-- Driver
        +-- Passenger
```

A person may have multiple PersonRoles.

Example:

```text
John Doe

Roles:
- Driver
- Passenger
```

PersonRole is a business concept, not an authentication concept.

---

### User

Represents system access.

User is responsible for:

* Login credentials
* Authentication
* Application authorization

Example:

```text
UserRole

- PlatformAdmin
- Admin
- Dispatcher
```

A person does not need to have a User account to exist.

Example:

```text
Driver
 |
 +-- Person
       |
       +-- Driver Role

(No User account required)
```

---

## Passenger Decision

Passengers are managed through **BookingPassenger**.

A BookingPassenger represents participation in a specific transportation service.

A BookingPassenger may reference:

### 1. Existing Person

Example:

```text
Person
 |
BookingPassenger
```

Used for:

* Known passengers
* Employee transportation
* Passenger history

---

### 2. Guest Passenger

Example:

```text
BookingPassenger
 |
 +-- GuestName
 +-- GuestPhone
```

Used for:

* One-time travelers
* Visitors
* Event transportation

A permanent Person record is not required for every passenger.

---

## Consequences

### Advantages

* Eliminates duplicate identity records.
* Keeps Person as the single source of human identity.
* Allows a person to have multiple transportation capabilities.
* Separates application security from business responsibility.
* Supports drivers who do not require system access.
* Keeps Booking focused on transportation operations.

### Trade-offs

* Requires clear separation between UserRole and PersonRole.
* Business rules must validate whether a Person has the required capability.
* Additional relationship management is required compared to a simple role flag.

---

## Principle

```text
Person
=
Human Identity

PersonRole
=
Transportation Capability

User
=
System Access

UserRole
=
Application Permission

BookingPassenger
=
Transportation Participation
```
