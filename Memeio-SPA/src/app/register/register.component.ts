import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { ToasterService } from '../_services/toaster.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  model: any = {};
  @ViewChild('registerForm') registerForm: ElementRef;
  constructor(
    private authService: AuthService,
    private toaster: ToasterService
  ) {}

  ngOnInit() {}

  register() {
    this.authService.register(this.model).subscribe(
      () => {
        // Register AND Login in our user once they register
        this.toaster.success('Successfully Registered!');
        this.login();
        this.registerForm.nativeElement.reset();
      },
      (err) => {
        this.toaster.error(err);
      }
    );
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
