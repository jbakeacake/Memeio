import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(
    private authService: AuthService,
    private router: Router,
    private alertify: AlertifyService
  ) {}

  canActivate(): boolean {
    if (this.authService.loggedIn()) {
      // If the user is logged in, they may pass and view the resource
      return true;
    }

    this.alertify.error('Please login to view this page'); // otherwise send them back to the home page
    this.router.navigate(['/home']);
    return false;
  }
}
