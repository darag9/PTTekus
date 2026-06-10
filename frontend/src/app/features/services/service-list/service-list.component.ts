import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subject, debounceTime, distinctUntilChanged } from 'rxjs';
import { ServiceService } from '../../../core/services/service.service';
import { Service } from '../../../models/service.model';
import { ServiceFormComponent } from '../service-form/service-form.component';

@Component({
  selector: 'app-service-list',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule
  ],
  templateUrl: './service-list.component.html',
  styleUrl: './service-list.component.scss'
})
export class ServiceListComponent implements OnInit {
  displayedColumns: string[] = ['name', 'hourlyRate', 'createdAt', 'providerCount', 'actions'];
  services: Service[] = [];
  totalCount = 0;
  
  searchQuery = '';
  private searchSubject = new Subject<string>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private serviceService: ServiceService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {
    this.searchSubject.pipe(
      debounceTime(300),
      distinctUntilChanged()
    ).subscribe(query => {
      this.searchQuery = query;
      this.paginator.pageIndex = 0;
      this.loadServices();
    });
  }

  ngOnInit(): void {
    this.loadServices();
  }

  ngAfterViewInit(): void {
    this.sort.sortChange.subscribe(() => {
      this.paginator.pageIndex = 0;
      this.loadServices();
    });
  }

  loadServices(): void {
    const page = this.paginator ? this.paginator.pageIndex + 1 : 1;
    const pageSize = this.paginator ? this.paginator.pageSize : 10;
    const sortBy = this.sort ? this.sort.active : 'createdAt';
    const ascending = this.sort ? this.sort.direction !== 'desc' : false;

    this.serviceService.getServices(page, pageSize, this.searchQuery, sortBy, ascending)
      .subscribe(res => {
        this.services = res.items;
        this.totalCount = res.totalCount;
      });
  }

  onSearch(event: Event): void {
    const value = (event.target as HTMLInputElement).value;
    this.searchSubject.next(value);
  }

  onPageChange(): void {
    this.loadServices();
  }

  openForm(service?: Service): void {
    const dialogRef = this.dialog.open(ServiceFormComponent, {
      width: '400px',
      data: { service }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadServices();
      }
    });
  }

  deleteService(id: string, name: string): void {
    if (confirm(`Are you sure you want to delete the service: ${name}?`)) {
      this.serviceService.deleteService(id).subscribe({
        next: () => {
          this.snackBar.open('Service deleted', 'Close', { duration: 3000, panelClass: ['snackbar-success'] });
          this.loadServices();
        },
        error: (err) => {
          this.snackBar.open(err.error?.message || 'Failed to delete service', 'Close', { duration: 3000, panelClass: ['snackbar-error'] });
        }
      });
    }
  }
}
