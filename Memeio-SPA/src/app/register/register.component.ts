import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  model: any = {};
  constructor(private authService: AuthService) {}

  ngOnInit() {}

  register() {
    this.authService.register(this.model).subscribe(() => {
      console.log('Registration success!');
      console.log('Logging in...');
      console.log(this.model);
      this.login();
    }, err => {
      console.log('Unable to register');
    });
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      console.log('logged in successfully');
    }, err => {
      console.log(err);
    });
  }

  loggedIn() {
    return this.authService.loggedIn();
  }
}
