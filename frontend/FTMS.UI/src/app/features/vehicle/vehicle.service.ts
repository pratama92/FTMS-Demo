import { Injectable, inject } from '@angular/core';

import { ApiService } from '../../core/services/api';
import { ApiResponse } from '../../core/models/api-response.model';
import { Vehicle } from './vehicle';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {

  private api = inject(ApiService);

  getVehicles() {
    return this.api.get<ApiResponse<Vehicle[]>>('/api/vehicles?isDeleted=false');
  }

  getVehicle(id: string) {
    return this.api.get<ApiResponse<Vehicle>>(`/api/vehicles/${id}`);
  }

  createVehicle(request: any) {
    return this.api.post<ApiResponse<string>>('/api/vehicles', request);
  }

  updateVehicle(id: string, request: any) {
    return this.api.put<ApiResponse<string>>(`/api/vehicles/${id}`, request);
  }

  deleteVehicle(id: string) {
    return this.api.delete<ApiResponse<string>>(`/api/vehicles/${id}`);
  }

  markasmaintenance(id: string) {
    return this.api.patch<ApiResponse<string>>(
      `/api/vehicles/${id}/maintenance`, {}
    );
  }

  markasavailable(id: string) {
    return this.api.patch<ApiResponse<string>>(
      `/api/vehicles/${id}/available`, {}
    );
  }

}