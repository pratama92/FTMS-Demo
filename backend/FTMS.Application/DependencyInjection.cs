using FTMS.Application.Common;
using FTMS.Application.Common.Settings;
using FTMS.Application.Interfaces;
using FTMS.Application.UseCases.BookingManagement.AddGuestPassenger;
using FTMS.Application.UseCases.BookingManagement.AddRegularPassenger;
using FTMS.Application.UseCases.BookingManagement.AssignDriver;
using FTMS.Application.UseCases.BookingManagement.CancelBooking;
using FTMS.Application.UseCases.BookingManagement.CompleteBooking;
using FTMS.Application.UseCases.BookingManagement.ConfirmBooking;
using FTMS.Application.UseCases.BookingManagement.CreateBooking;
using FTMS.Application.UseCases.BookingManagement.GetBookingById;
using FTMS.Application.UseCases.BookingManagement.GetBookings;
using FTMS.Application.UseCases.BookingManagement.RemoveDriver;
using FTMS.Application.UseCases.BookingManagement.RemovePassenger;
using FTMS.Application.UseCases.OrganizationManagement.ActivateOrganization;
using FTMS.Application.UseCases.OrganizationManagement.CreateOrganization;
using FTMS.Application.UseCases.OrganizationManagement.DeactivateOrganization;
using FTMS.Application.UseCases.OrganizationManagement.GetOrganizationById;
using FTMS.Application.UseCases.OrganizationManagement.GetOrganizations;
using FTMS.Application.UseCases.OrganizationManagement.RenameOrganization;
using FTMS.Application.UseCases.PersonManagement.AddDriverRole;
using FTMS.Application.UseCases.PersonManagement.CreatePerson;
using FTMS.Application.UseCases.PersonManagement.DeletePerson;
using FTMS.Application.UseCases.PersonManagement.GetPersonById;
using FTMS.Application.UseCases.PersonManagement.GetPersons;
using FTMS.Application.UseCases.PersonManagement.RemoveDriverRole;
using FTMS.Application.UseCases.PersonManagement.UpdatePerson;
using FTMS.Application.UseCases.UserManagement.CreateUser;
using FTMS.Application.UseCases.UserManagement.Login;
using FTMS.Application.UseCases.VehicleManagement.UpdateStatusVehicle;
using FTMS.Application.UseCases.VehicleManagement.CreateVehicle;
using FTMS.Application.UseCases.VehicleManagement.DeleteVehicle;
using FTMS.Application.UseCases.VehicleManagement.GetVehicleById;
using FTMS.Application.UseCases.VehicleManagement.GetVehicles;
using FTMS.Application.UseCases.VehicleManagement.UpdateVehicle;
using FTMS.Infrastructure.Security;
using Microsoft.Extensions.DependencyInjection;
using FTMS.Application.UseCases.TripManagement.CreateTrip;
using FTMS.Application.UseCases.TripManagement.StartTrip;
using FTMS.Application.UseCases.TripManagement.FinishTrip;
using FTMS.Application.UseCases.TripManagement.CancelTrip;
using FTMS.Application.Workflow;
using FTMS.Application.UseCases.BookingManagement.GetTripByBookingId;
using FTMS.Application.UseCases.Lookup;
using FTMS.Application.UseCases.BookingManagement.ChangePickupLocation;

namespace FTMS.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // =====================
        // Person Management
        // =====================
        services.AddScoped<CreatePersonUseCase>();
        services.AddScoped<GetPersonsUseCase>();
        services.AddScoped<GetPersonByIdUseCase>();
        services.AddScoped<UpdatePersonUseCase>();
        services.AddScoped<DeletePersonUseCase>();
        services.AddScoped<AddDriverRoleUseCase>();
        services.AddScoped<RemoveDriverRoleUseCase>();

        // =====================
        // Vehicle Management
        // =====================
        services.AddScoped<CreateVehicleUseCase>();
        services.AddScoped<GetVehiclesUseCase>();
        services.AddScoped<GetVehicleByIdUseCase>();
        services.AddScoped<UpdateVehicleUseCase>();
        services.AddScoped<DeleteVehicleUseCase>();
        services.AddScoped<UpdateStatusVehicleUseCase>();

        // =====================
        // Organization Management
        // =====================
        services.AddScoped<CreateOrganizationUseCase>();
        services.AddScoped<GetOrganizationsUseCase>();
        services.AddScoped<GetOrganizationByIdUseCase>();
        services.AddScoped<RenameOrganizationUseCase>();
        services.AddScoped<ActivateOrganizationUseCase>();
        services.AddScoped<DeactivateOrganizationUseCase>();

        // =====================
        // Booking Management
        // =====================
        services.AddScoped<CreateBookingUseCase>();
        services.AddScoped<AddGuestPassengerUseCase>();
        services.AddScoped<AddRegularPassengerUseCase>();
        services.AddScoped<RemovePassengerUseCase>();
        services.AddScoped<AssignDriverUseCase>();
        services.AddScoped<RemoveDriverUseCase>();
        services.AddScoped<CancelBookingUseCase>();
        services.AddScoped<GetBookingsUseCase>();
        services.AddScoped<GetBookingByIdUseCase>();
        services.AddScoped<ConfirmBookingUseCase>();
        services.AddScoped<CompleteBookingUseCase>();
        services.AddScoped<GetTripByBookingIdUseCase>();
        services.AddScoped<ChangePickupLocationUseCase>();

        // =====================
        // User Management
        // =====================
        services.AddScoped<CreateUserUseCase>();
        services.AddScoped<LoginUseCase>();

        // =====================
        // Trip
        // =====================
        services.AddScoped<CreateTripUseCase>();
        services.AddScoped<StartTripUseCase>();
        services.AddScoped<FinishTripUseCase>();
        services.AddScoped<CancelTripUseCase>();

        // =====================
        // Validator
        // =====================
        services.AddScoped<IOrganizationValidator, OrganizationValidator>();
        services.AddScoped<IVehicleValidator, VehicleValidator>();
        services.AddScoped<IPersonValidator, PersonValidator>();

        // =====================
        // Settings
        // =====================
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddScoped<JwtSettings>();

        // =====================
        // Workflow
        // =====================
        services.AddScoped<ConfirmBookingWorkflow>();
        services.AddScoped<FinishTripWorkflow>();

        // =====================
        // Lookup
        // =====================
        services.AddScoped<GetLookupDriverUseCase>();
        services.AddScoped<GetLookupVehicleUseCase>();
        services.AddScoped<GetLookupPersonUseCase>();

        return services;
    }
}