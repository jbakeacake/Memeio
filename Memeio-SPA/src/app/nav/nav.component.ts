import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { User } from '../_models/user';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent implements OnInit {
  users: User[];

  currentActive: ElementRef;

  constructor(
    public authService: AuthService,
    private alertify: AlertifyService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {}

  logout() {
    // Remove all any items stored locally including the user's token -- this should prevent them from accessing other resources
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.authService.decodedToken = null;
    this.authService.currentUser = null;
    this.alertify.message('Logged out');
  }

  loggedIn() {
    return this.authService.loggedIn();
  }
}
