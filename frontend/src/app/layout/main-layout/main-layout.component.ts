import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet, RouterModule } from '@angular/router';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { AuthService } from '../../core/services/auth.service';
import { Observable } from 'rxjs';
import { AuthResponse } from '../../models/auth.model';

@Component({
  selector: 'app-main-layout',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    RouterModule,
    MatSidenavModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatListModule
  ],
  templateUrl: './main-layout.component.html',
  styleUrl: './main-layout.component.scss'
})
export class MainLayoutComponent {
  user$: Observable<AuthResponse | null>;
  isSidenavOpen = true;

  constructor(private authService: AuthService) {
    this.user$ = this.authService.currentUser$;
  }

  toggleSidenav() {
    this.isSidenavOpen = !this.isSidenavOpen;
  }

  logout() {
    this.authService.logout();
  }
}
