export interface Booking {
  bookingId: string;
  bookingNumber: string;
  vehicleId: string;
  vehicleCode: string;
  driverPersonId: string;
  driverName: string;
  destinationLocation: string;
  createdByPersonName: string;
  estimatedDepartureTime: string;
  estimatedArrivalTime: string;
  status: string;
  statusTrip: string | null;
  createdAt: string;
  passengers: BookingPassenger[];
}

export interface BookingPassenger {
  bookingPassengerId: string;
  bookingId: string;
  personId?: string | null;
  personName?: string | null;
  personPhone?: string | null;
  guestName?: string | null;
  guestPhone?: string | null;
  passengerType: string;
  pickupLocation: string;
}