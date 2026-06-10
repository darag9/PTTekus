import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiService } from './api.service';
import { DashboardData } from '../../models/common.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DashboardService extends ApiService {
  constructor(http: HttpClient) {
    super(http);
  }

  getDashboardData(): Observable<DashboardData> {
    return this.get<DashboardData>('/dashboard');
  }
}
