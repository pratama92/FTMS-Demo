import { Injectable, inject } from '@angular/core';

import { ApiService } from '../../core/services/api';
import { ApiResponse } from '../../core/models/api-response.model';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  private api = inject(ApiService);

  getPersons() {
    return this.api.get<ApiResponse<any[]>>(
      '/api/persons'
    );
  }

  getVehicles() {
    return this.api.get<ApiResponse<any[]>>(
      '/api/vehicles'
    );
  }

  getBookings() {
    return this.api.get<ApiResponse<any[]>>(
      '/api/bookings'
    );
  }

  getBookingsByDate(date: Date) {
    const dateValue = this.getDateString(date);

    return this.api.get<ApiResponse<any[]>>(
      `/api/bookings/day?date=${dateValue}`
    );
  }

  getDateString(date: Date): string {

    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');

    return `${year}-${month}-${day}`;
  }

}