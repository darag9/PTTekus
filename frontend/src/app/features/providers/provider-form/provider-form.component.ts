import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { ProviderService } from '../../../core/services/provider.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-provider-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './provider-form.component.html',
  styleUrl: './provider-form.component.scss'
})
export class ProviderFormComponent implements OnInit {
  providerForm: FormGroup;
  isEditMode = false;
  loading = false;

  constructor(
    private fb: FormBuilder,
    private providerService: ProviderService,
    public dialogRef: MatDialogRef<ProviderFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { provider?: any },
    private snackBar: MatSnackBar
  ) {
    this.isEditMode = !!data?.provider;
    
    this.providerForm = this.fb.group({
      nit: ['', [Validators.required]],
      name: ['', [Validators.required, Validators.maxLength(100)]],
      websiteUrl: ['', [Validators.required, Validators.pattern('https?://.+')]],
      email: ['', [Validators.required, Validators.email]],
      country: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    if (this.isEditMode && this.data.provider) {
      this.providerForm.patchValue(this.data.provider);
    }
  }

  onSubmit(): void {
    if (this.providerForm.invalid) return;

    this.loading = true;
    const providerData = this.providerForm.value;

    if (this.isEditMode) {
      this.providerService.updateProvider(this.data.provider.id, providerData).subscribe({
        next: () => {
          this.snackBar.open('Provider updated successfully', 'Close', { duration: 3000, panelClass: ['snackbar-success'] });
          this.dialogRef.close(true);
        },
        error: () => {
          this.loading = false;
          this.snackBar.open('Error updating provider', 'Close', { duration: 3000, panelClass: ['snackbar-error'] });
        }
      });
    } else {
      this.providerService.createProvider(providerData).subscribe({
        next: () => {
          this.snackBar.open('Provider created successfully', 'Close', { duration: 3000, panelClass: ['snackbar-success'] });
          this.dialogRef.close(true);
        },
        error: () => {
          this.loading = false;
          this.snackBar.open('Error creating provider', 'Close', { duration: 3000, panelClass: ['snackbar-error'] });
        }
      });
    }
  }
}
