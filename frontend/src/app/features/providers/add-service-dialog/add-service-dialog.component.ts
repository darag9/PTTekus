import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { ProviderService } from '../../../core/services/provider.service';
import { ServiceService } from '../../../core/services/service.service';
import { Service } from '../../../models/service.model';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-add-service-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule
  ],
  templateUrl: './add-service-dialog.component.html',
  styleUrl: './add-service-dialog.component.scss'
})
export class AddServiceDialogComponent implements OnInit {
  form: FormGroup;
  services: Service[] = [];
  loading = false;
  providerId: string;

  constructor(
    private fb: FormBuilder,
    private providerService: ProviderService,
    private serviceService: ServiceService,
    public dialogRef: MatDialogRef<AddServiceDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { providerId: string },
    private snackBar: MatSnackBar
  ) {
    this.providerId = data.providerId;
    this.form = this.fb.group({
      serviceId: ['', Validators.required],
      customHourlyRate: [null, [Validators.min(0)]]
    });
  }

  ngOnInit(): void {
    // For simplicity, fetching up to 100 services for dropdown. 
    // In real app, might want server-side search in mat-select.
    this.serviceService.getServices(1, 100).subscribe(res => {
      this.services = res.items;
    });
  }

  onSubmit(): void {
    if (this.form.invalid) return;

    this.loading = true;
    this.providerService.addServiceToProvider(this.providerId, this.form.value).subscribe({
      next: () => {
        this.snackBar.open('Service added successfully', 'Close', { duration: 3000, panelClass: ['snackbar-success'] });
        this.dialogRef.close(true);
      },
      error: (err) => {
        this.loading = false;
        this.snackBar.open(err.error?.message || 'Error adding service', 'Close', { duration: 3000, panelClass: ['snackbar-error'] });
      }
    });
  }
}
