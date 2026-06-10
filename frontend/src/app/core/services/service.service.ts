import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ApiService } from './api.service';
import { Service } from '../../models/service.model';
import { PagedResult } from '../../models/common.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ServiceService extends ApiService {
  constructor(http: HttpClient) {
    super(http);
  }

  getServices(page: number = 1, pageSize: number = 10, search: string = '', sortBy: string = '', ascending: boolean = true): Observable<PagedResult<Service>> {
    let params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString())
      .set('ascending', ascending.toString());

    if (search) params = params.set('search', search);
    if (sortBy) params = params.set('sortBy', sortBy);

    return this.get<PagedResult<Service>>('/services', params);
  }

  getService(id: string): Observable<Service> {
    return this.get<Service>(`/services/${id}`);
  }

  createService(service: any): Observable<string> {
    return this.post<string>('/services', service);
  }

  updateService(id: string, service: any): Observable<void> {
    return this.put<void>(`/services/${id}`, service);
  }

  deleteService(id: string): Observable<void> {
    return this.delete<void>(`/services/${id}`);
  }
}
