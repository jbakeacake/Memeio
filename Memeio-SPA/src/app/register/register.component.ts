import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  model: any = {};
  @ViewChild('registerForm') registerForm: ElementRef;
  constructor(private authService: AuthService, private alertify: AlertifyService) {}

  ngOnInit() {}

  register() {
    this.authService.register(this.model).subscribe(() => {
      this.alertify.success('Successfully Registered!');
      this.login();
      this.registerForm.nativeElement.reset();
    }, err => {
      this.alertify.error(err);
    });
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      this.alertify.success('Login Successful!');
    }, err => {
      this.alertify.error(err);
    });
  }

  loggedIn() {
    return this.authService.loggedIn();
  }
}
