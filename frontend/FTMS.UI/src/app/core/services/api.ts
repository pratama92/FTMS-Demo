import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private readonly baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  private buildUrl(url: string): string {
    return `${this.baseUrl}${url.replace(/^\/api/, '')}`;
  }

  get<T>(url: string): Observable<T> {
    return this.http.get<T>(this.buildUrl(url));
  }

  post<T>(url: string, body: unknown): Observable<T> {
    return this.http.post<T>(this.buildUrl(url), body);
  }

  put<T>(url: string, body: unknown): Observable<T> {
    return this.http.put<T>(this.buildUrl(url), body);
  }

  delete<T>(url: string): Observable<T> {
    return this.http.delete<T>(this.buildUrl(url));
  }

  patch<T>(url: string, body: unknown): Observable<T> {
    return this.http.patch<T>(
      this.buildUrl(url),
      body
    );
  }
}