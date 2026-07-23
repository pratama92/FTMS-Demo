import { Injectable } from '@angular/core';
import { ApiService } from '../../core/services/api';
import { ApiResponse } from '../../core/models/api-response.model';


@Injectable({
  providedIn: 'root'
})
export class TripService {


  constructor(
    private api: ApiService
  ) { }



  getByBookingId(bookingId: string) {
    return this.api.get<any>(
      `/api/bookings/${bookingId}/trip`
    );
  }

  startTrip(tripId: string, request: any) {
    return this.api.patch<ApiResponse<string>>(
      `/api/trips/${tripId}/start`, request
    );
  }

  finishTrip(tripId: string, request: any) {
    return this.api.patch<ApiResponse<string>>(
      `/api/trips/${tripId}/finish`, request
    );
  }

  cancelTrip(id: string, request: any) {
    return this.api.patch<ApiResponse<string>>(
      `/api/trips/${id}/cancel`, request);
  }

}