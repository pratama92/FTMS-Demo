export interface DashboardBooking {

  bookingId: string;

  bookingNumber: string;

  vehicleId: string;

  vehicleCode: string;

  estimatedDepartureTime: string;

  estimatedArrivalTime: string;

  status: string;

  statusTrip: string | null;

}