import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ProviderService } from '../../../core/services/provider.service';
import { ProviderDetail } from '../../../models/provider.model';
import { AddServiceDialogComponent } from '../add-service-dialog/add-service-dialog.component';

@Component({
  selector: 'app-provider-detail',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatDialogModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './provider-detail.component.html',
  styleUrl: './provider-detail.component.scss'
})
export class ProviderDetailComponent implements OnInit {
  providerId!: string;
  provider: ProviderDetail | null = null;
  displayedColumns: string[] = ['name', 'hourlyRate', 'customHourlyRate', 'effectiveHourlyRate', 'actions'];
  loading = true;

  constructor(
    private route: ActivatedRoute,
    private providerService: ProviderService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.providerId = params.get('id')!;
      this.loadProviderDetails();
    });
  }

  loadProviderDetails(): void {
    this.loading = true;
    this.providerService.getProvider(this.providerId).subscribe({
      next: (res) => {
        this.provider = res;
        this.loading = false;
      },
      error: () => {
        this.loading = false;
        this.snackBar.open('Error loading provider details', 'Close', { duration: 3000, panelClass: ['snackbar-error'] });
      }
    });
  }

  openAddServiceDialog(): void {
    const dialogRef = this.dialog.open(AddServiceDialogComponent, {
      width: '400px',
      data: { providerId: this.providerId }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadProviderDetails();
      }
    });
  }

  removeService(serviceId: string): void {
    if (confirm('Are you sure you want to remove this service from the provider?')) {
      this.providerService.removeServiceFromProvider(this.providerId, serviceId).subscribe({
        next: () => {
          this.snackBar.open('Service removed successfully', 'Close', { duration: 3000, panelClass: ['snackbar-success'] });
          this.loadProviderDetails();
        },
        error: () => {
          this.snackBar.open('Error removing service', 'Close', { duration: 3000, panelClass: ['snackbar-error'] });
        }
      });
    }
  }
}
