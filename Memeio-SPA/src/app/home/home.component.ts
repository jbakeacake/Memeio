import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
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
    private authService: AuthService,
    private alertify: AlertifyService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(
      (next) => {
        this.alertify.success('Login Successful!');
      },
      (err) => {
        this.alertify.error(err);
      }
    );
  }

  loggedIn() {
    return this.authService.loggedIn();
  }
}
