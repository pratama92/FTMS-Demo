import { Injectable, inject } from '@angular/core';

import { ApiService } from '../../core/services/api';
import { ApiResponse } from '../../core/models/api-response.model';
import { Booking } from './booking';
import { DashboardBooking } from '../dashboard/models/dashboard-booking.model';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class BookingService {

  private api = inject(ApiService);
  private router = inject(Router);

  getBookings() {
    return this.api.get<ApiResponse<Booking[]>>(
      '/api/bookings'
    );
  }

  getBooking(id: string) {
    return this.api.get<ApiResponse<Booking>>(
      `/api/bookings/${id}`
    );
  }

  createBooking(request: any) {
    return this.api.post<ApiResponse<string>>(
      '/api/bookings',
      request
    );
  }

  updateBooking(id: string, request: any) {
    return this.api.put<ApiResponse<string>>(
      `/api/bookings/${id}`,
      request
    );
  }

  deleteBooking(id: string) {
    return this.api.delete<ApiResponse<string>>(
      `/api/bookings/${id}`
    );
  }

  confirmBooking(id: string) {
    return this.api.patch<ApiResponse<string>>(
      `/api/bookings/${id}/confirm`,
      {}
    );
  }

  assignDriver(bookingId: string, personId: string) {
    return this.api.patch<ApiResponse<string>>(
      `/api/bookings/${bookingId}/assigndriver`,
      {
        personId
      }
    );
  }

  removeDriver(bookingId: string) {
    return this.api.patch<ApiResponse<string>>(
      `/api/bookings/${bookingId}/removedriver`,
      {}
    );
  }

  addRegularPassenger(bookingId: string, request: any) {
    return this.api.patch<ApiResponse<string>>(
      `/api/bookings/${bookingId}/addregularpassenger`, request

    );
  }

  removePassenger(bookingId: string, bookingPassengerId: string) {
    return this.api.patch<ApiResponse<string>>(
      `/api/bookings/${bookingId}/removepassenger`,
      {
        bookingPassengerId
      }
    );
  }

  changePickupLocationPassenger(bookingId: string, request: any) {
    return this.api.patch<ApiResponse<string>>(
      `/api/bookings/${bookingId}/changepickuplocation`, request
    );
  }

  addGuestPassenger(bookingId: string, request: any) {
    return this.api.patch<ApiResponse<string>>(
      `/api/bookings/${bookingId}/addguestpassenger`,
      request
    );
  }

  cancelBooking(id: string) {
    return this.api.patch<ApiResponse<string>>(
      `/api/bookings/${id}/cancel`,
      {}
    );
  }

  openBookingDetail(booking: DashboardBooking): void {

    this.router.navigate([
      '/bookings',
      booking.bookingId
    ]);

  }

}