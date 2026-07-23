import { Injectable, inject } from '@angular/core';

import { ApiService } from './api';
import { ApiResponse } from '../models/api-response.model';
import { LookupItem } from '../models/lookup-item.model';


@Injectable({
  providedIn: 'root'
})
export class LookupService {

  private api = inject(ApiService);

  getPersons() {
    return this.api.get<ApiResponse<LookupItem[]>>(
      '/api/lookups/persons'
    );
  }

  getDrivers() {
    return this.api.get<ApiResponse<LookupItem[]>>(
      '/api/lookups/drivers'
    );
  }

  getVehicles() {
    return this.api.get<ApiResponse<LookupItem[]>>(
      '/api/lookups/vehicles'
    );
  }


}