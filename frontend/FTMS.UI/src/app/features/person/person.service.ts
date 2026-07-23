import { Injectable, inject } from '@angular/core';
import { ApiService } from '../../core/services/api';
import { ApiResponse } from '../../core/models/api-response.model';
import { Person } from './person';

@Injectable({
  providedIn: 'root'
})
export class PersonService {

  private api = inject(ApiService);

  getPersons() {
    return this.api.get<ApiResponse<Person[]>>('/api/persons?isDeleted=false');
  }

  createPerson(request: any) {
    return this.api.post<ApiResponse<string>>(
      '/api/persons',
      request
    );
  }

  getPerson(id: string) {
    return this.api.get<ApiResponse<Person>>(
      `/api/persons/${id}`
    );
  }

  updatePerson(id: string, request: any) {
    return this.api.put<ApiResponse<string>>(
      `/api/persons/${id}`,
      request
    );
  }

  deletePerson(id: string) {
    return this.api.delete<ApiResponse<string>>(
      `/api/persons/${id}`
    );
  }

  addDriverRolePerson(id: string) {
    return this.api.patch<ApiResponse<string>>(
      `/api/persons/${id}/adddriverrole`, {}
    );
  }

  removeDriverRolePerson(id: string) {
    return this.api.patch<ApiResponse<string>>(
      `/api/persons/${id}/removedriverrole`, {}
    );
  }

}