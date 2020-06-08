import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { ToasterService } from '../_services/toaster.service';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../_models/user';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent implements OnInit {
  users: User[];

  constructor(
    public authService: AuthService,
    private toaster: ToasterService,
    private router: Router
  ) {}

  ngOnInit() {}

  logout() {
    // Remove all any items stored locally including the user's token -- this should prevent them from accessing other resources
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.authService.decodedToken = null;
    this.authService.currentUser = null;
    this.toaster.logoutMsg('Logged out: See you later!');
  }

  loggedIn() {
    return this.authService.loggedIn();
  }
}
