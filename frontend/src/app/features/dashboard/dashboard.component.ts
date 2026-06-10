import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { DashboardService } from '../../core/services/dashboard.service';
import { DashboardData } from '../../models/common.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatIconModule, MatProgressSpinnerModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent implements OnInit {
  dashboardData$!: Observable<DashboardData>;

  constructor(private dashboardService: DashboardService) {}

  ngOnInit(): void {
    this.dashboardData$ = this.dashboardService.getDashboardData();
  }

  getBarWidth(count: number, max: number): string {
    if (!max) return '0%';
    return `${(count / max) * 100}%`;
  }

  getMaxCount(data: any[]): number {
    if (!data || data.length === 0) return 0;
    return Math.max(...data.map(d => d.count));
  }
}
