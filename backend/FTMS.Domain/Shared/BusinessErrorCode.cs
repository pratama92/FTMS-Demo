namespace FTMS.Domain.Shared
{
    public static class ErrorCodes
    {
        // General
        public const string InvalidData = "INVALID_DATA";
        public const string InvalidStatus = "INVALID_STATUS";
        public const string InvalidCredential = "INVALID_CREDENTIAL";
        public const string InvalidTimeRange = "INVALID_TIME_RANGE";
        public const string ValidationFailed = "VALIDATION_FAILED";
        public const string RequiredField = "REQUIRED_FIELD";

        // User
        public const string UserNameExists = "USER_NAME_EXISTS";

        // Organization
        public const string OrganizationNotFound = "ORG_NOT_FOUND";
        public const string OrganizationExists = "ORG_EXISTS";
        public const string OrganizationRequired = "ORG_REQUIRED";
        public const string OrganizationRequiredAdmin = "ORG_REQUIRED_ADMIN";

        // Person
        public const string PersonNotFound = "PERSON_NOT_FOUND";
        public const string PersonExists = "PERSON_EXISTS";
        public const string PersonEmailExists = "PERSON_EMAIL_EXISTS";
        public const string PersonRequired = "PERSON_REQUIRED";
        public const string PersonDeleted = "PERSON_DELETED";
        public const string PersonNameRequired = "PERSON_NAME_REQUIRED";
        public const string PersonEmailRequired = "PERSON_EMAIL_REQUIRED";
        public const string PersonEmailInvalid = "PERSON_EMAIL_INVALID";
        public const string PersonNotDriver = "PERSON_NOT_DRIVER";

        // Vehicle
        public const string VehicleNotFound = "VEHICLE_NOT_FOUND";
        public const string VehicleExists = "VEHICLE_EXISTS";
        public const string VehicleRequired = "VEHICLE_REQUIRED";
        public const string VehicleAvailable = "VEHICLE_AVAILABLE";
        public const string VehicleUnAvailable = "VEHICLE_UNAVAILABLE";
        public const string VehicleInUse = "VEHICLE_IN_USE";
        public const string VehicleInMaintenance = "VEHICLE_IN_MAINTENANCE";
        public const string VehicleRetired = "VEHICLE_RETIRED";
        public const string VehicleNotOwned = "VEHICLE_NOT_OWNED";
        public const string VehicleTypeInvalid = "VEHICLE_TYPE_INVALID";
        public const string VehicleDeleted = "VEHICLE_DELETED";
        public const string VehicleColorRequired = "VEHICLE_COLOR_REQUIRED";
        public const string VehicleCargoCapacityInvalid = "VEHICLE_CARGO_CAPACITY_INVALID";
        public const string VehicleSeatInvalid = "VEHICLE_SEAT_INVALID";
        public const string VehicleCodeRequired = "VEHICLE_CODE_REQUIRED";
        public const string VehicleLicenseRequired = "VEHICLE_LICENSE_REQUIRED";
        public const string VehicleLicenseDuplicated = "VEHICLE_LICENSE_DUPLICATED";
        public const string VehicleBrandRequired = "VEHICLE_BRANDE_REQUIRED";
        public const string VehicleModelRequired = "VEHICLE_MODEL_REQUIRED";
        public const string VehicleChassisNumberRequired = "VEHICLE_CHASSIS_NUMBER_REQUIRED";
        public const string VehicleChassisNumberDuplicated = "VEHICLE_CHASSIS_NUMBER_DUPLICATED";
        public const string VehicleEngineNumberRequired = "VEHICLE_ENGINE_NUMBER_REQUIRED";
        public const string VehicleEngineNumberDuplicated = "VEHICLE_ENGINE_NUMBER_DUPLICATED";
        public const string VehicleYearInvalid = "VEHICLE_YEAR_INVALID";
        public const string VehicleDrivetrainInvalid = "VEHICLE_DRIVETRAIN_INVALID";
        public const string VehicleFuelTypeInvalid = "VEHICLE_FUEL_TYPE_INVALID";
        public const string VehicleTransmissionInvalid = "VEHICLE_TRANSMISSION_INVALID";

        // Booking
        public const string BookingNotFound = "BOOKING_NOT_FOUND";
        public const string BookingCannotBeCancelled = "BOOKING_CANNOT_BE_CANCELLED";
        public const string BookingCapacityExceeded = "BOOKING_CAPACITY_EXCEEDED";
        public const string BookingNumberRequired = "BOOKING_NUMBER_REQUIRED";
        public const string BookingRequired = "BOOKING_REQUIRED";
        public const string BookingCompleted = "BOOKING_IS_COMPLETED";
        public const string BookingCancelled = "BOOKING_IS_CANCELLED";
        public const string BookingConfirmed = "BOOKING_IS_CONFIRMED";
        public const string BookingNotConfirmed = "BOOKING_NOT_CONFIRMED";
        public const string BookingCreatedByPersonRequired = "BOOKING_PERSON_CREATOR_REQUIRED";
        public const string BookingDestinationRequired = "BOOKING_DESTINATION_REQUIRED";
        public const string BookingNotOwned = "BOOKING_NOT_OWNED";

        // Driver
        public const string DriverRequired = "DRIVER_REQUIRED";
        public const string DriverAlreadyAssigned = "DRIVER_ALREADY_ASSIGNED";
        public const string DriverNotOwned = "DRIVER_NOT_OWNED";

        // Passenger
        public const string PassengerRequired = "PASSENGER_REQUIRED";
        public const string PassengerNotFound = "PASSENGER_NOT_FOUND";
        public const string PassengerExists = "PASSENGER_EXISTS";
        public const string PassengerIsDriver = "PASSENGER_IS_DRIVER";
        public const string PassengerAlreadyAdded = "PASSENGER_ALREADY_ADDED";
        public const string PassengerPickupRequired = "PASSENGER_PICKUPLOCATION_REQUIRED";

        // Trip
        public const string TripNotFound = "TRIP_NOT_FOUND";
        public const string TripNotOwned = "TRIP_NOT_OWNED";
        public const string TripReasonRequired = "TRIP_REASON_REQUIRED";
    }
}
