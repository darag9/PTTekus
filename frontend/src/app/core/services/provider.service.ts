import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ApiService } from './api.service';
import { Provider, ProviderDetail } from '../../models/provider.model';
import { PagedResult } from '../../models/common.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProviderService extends ApiService {
  constructor(http: HttpClient) {
    super(http);
  }

  getProviders(page: number = 1, pageSize: number = 10, search: string = '', sortBy: string = '', ascending: boolean = true): Observable<PagedResult<Provider>> {
    let params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString())
      .set('ascending', ascending.toString());

    if (search) params = params.set('search', search);
    if (sortBy) params = params.set('sortBy', sortBy);

    return this.get<PagedResult<Provider>>('/providers', params);
  }

  getProvider(id: string): Observable<ProviderDetail> {
    return this.get<ProviderDetail>(`/providers/${id}`);
  }

  createProvider(provider: any): Observable<string> {
    return this.post<string>('/providers', provider);
  }

  updateProvider(id: string, provider: any): Observable<void> {
    return this.put<void>(`/providers/${id}`, provider);
  }

  deleteProvider(id: string): Observable<void> {
    return this.delete<void>(`/providers/${id}`);
  }

  addServiceToProvider(providerId: string, payload: { serviceId: string; customHourlyRate?: number }): Observable<void> {
    return this.post<void>(`/providers/${providerId}/services`, payload);
  }

  removeServiceFromProvider(providerId: string, serviceId: string): Observable<void> {
    return this.delete<void>(`/providers/${providerId}/services/${serviceId}`);
  }
}
