import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subject, debounceTime, distinctUntilChanged } from 'rxjs';
import { ProviderService } from '../../../core/services/provider.service';
import { Provider } from '../../../models/provider.model';
import { ProviderFormComponent } from '../provider-form/provider-form.component';

@Component({
  selector: 'app-provider-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule
  ],
  templateUrl: './provider-list.component.html',
  styleUrl: './provider-list.component.scss'
})
export class ProviderListComponent implements OnInit {
  displayedColumns: string[] = ['name', 'nit', 'email', 'country', 'serviceCount', 'actions'];
  providers: Provider[] = [];
  totalCount = 0;
  
  searchQuery = '';
  private searchSubject = new Subject<string>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private providerService: ProviderService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {
    this.searchSubject.pipe(
      debounceTime(300),
      distinctUntilChanged()
    ).subscribe(query => {
      this.searchQuery = query;
      this.paginator.pageIndex = 0;
      this.loadProviders();
    });
  }

  ngOnInit(): void {
    // initial load will be triggered by AfterViewInit or we can just load
    this.loadProviders();
  }

  ngAfterViewInit(): void {
    this.sort.sortChange.subscribe(() => {
      this.paginator.pageIndex = 0;
      this.loadProviders();
    });
  }

  loadProviders(): void {
    const page = this.paginator ? this.paginator.pageIndex + 1 : 1;
    const pageSize = this.paginator ? this.paginator.pageSize : 10;
    const sortBy = this.sort ? this.sort.active : 'createdAt';
    const ascending = this.sort ? this.sort.direction !== 'desc' : false;

    this.providerService.getProviders(page, pageSize, this.searchQuery, sortBy, ascending)
      .subscribe(res => {
        this.providers = res.items;
        this.totalCount = res.totalCount;
      });
  }

  onSearch(event: Event): void {
    const value = (event.target as HTMLInputElement).value;
    this.searchSubject.next(value);
  }

  onPageChange(): void {
    this.loadProviders();
  }

  openForm(provider?: Provider): void {
    const dialogRef = this.dialog.open(ProviderFormComponent, {
      width: '500px',
      data: { provider }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadProviders();
      }
    });
  }

  deleteProvider(id: string, name: string): void {
    if (confirm(`Are you sure you want to delete ${name}?`)) {
      this.providerService.deleteProvider(id).subscribe({
        next: () => {
          this.snackBar.open('Provider deleted', 'Close', { duration: 3000, panelClass: ['snackbar-success'] });
          this.loadProviders();
        },
        error: (err) => {
          this.snackBar.open('Failed to delete provider', 'Close', { duration: 3000, panelClass: ['snackbar-error'] });
        }
      });
    }
  }
}
