import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { ServiceService } from '../../../core/services/service.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-service-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './service-form.component.html',
  styleUrl: './service-form.component.scss'
})
export class ServiceFormComponent implements OnInit {
  serviceForm: FormGroup;
  isEditMode = false;
  loading = false;

  constructor(
    private fb: FormBuilder,
    private serviceService: ServiceService,
    public dialogRef: MatDialogRef<ServiceFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { service?: any },
    private snackBar: MatSnackBar
  ) {
    this.isEditMode = !!data?.service;
    
    this.serviceForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(100)]],
      hourlyRate: ['', [Validators.required, Validators.min(0)]]
    });
  }

  ngOnInit(): void {
    if (this.isEditMode && this.data.service) {
      this.serviceForm.patchValue({
        name: this.data.service.name,
        hourlyRate: this.data.service.hourlyRate
      });
    }
  }

  onSubmit(): void {
    if (this.serviceForm.invalid) return;

    this.loading = true;
    const serviceData = this.serviceForm.value;

    if (this.isEditMode) {
      this.serviceService.updateService(this.data.service.id, serviceData).subscribe({
        next: () => {
          this.snackBar.open('Service updated successfully', 'Close', { duration: 3000, panelClass: ['snackbar-success'] });
          this.dialogRef.close(true);
        },
        error: () => {
          this.loading = false;
          this.snackBar.open('Error updating service', 'Close', { duration: 3000, panelClass: ['snackbar-error'] });
        }
      });
    } else {
      this.serviceService.createService(serviceData).subscribe({
        next: () => {
          this.snackBar.open('Service created successfully', 'Close', { duration: 3000, panelClass: ['snackbar-success'] });
          this.dialogRef.close(true);
        },
        error: () => {
          this.loading = false;
          this.snackBar.open('Error creating service', 'Close', { duration: 3000, panelClass: ['snackbar-error'] });
        }
      });
    }
  }
}
