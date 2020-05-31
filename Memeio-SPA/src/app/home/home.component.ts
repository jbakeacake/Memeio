import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { ToasterService } from '../_services/toaster.service';
import { User } from '../_models/user';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  model: any = {};
  users: User[];
  constructor(
    public authService: AuthService,
    private toaster: ToasterService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(
      (next) => {
        this.toaster.loginSuccess('Login Successful!');
      },
      (err) => {
        this.toaster.error(err);
      }
    );
  }

  loggedIn() {
    return this.authService.loggedIn();
  }
}
