import { Injectable } from '@angular/core';
import { Ng2IzitoastService } from 'ng2-izitoast';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class ToasterService {
  constructor(public toaster: Ng2IzitoastService, private authService: AuthService) {}

  // confirm(message: string, okCallback: () => any) {
  //   this.toaster.confirm(message, (e: any) => {
  //     if (e) {
  //       okCallback();
  //     } else {
  //     }
  //   });
  // }

  success(msg: string) {
    // alertify.success(message);
    this.toaster.show({
      theme: 'secondary',
      title: msg,
      icon: 'fas fa-user-astronaut',
      message: 'Welcome, ' + this.authService.decodedToken.unique_name + '!',
      position: 'bottomCenter',
      progressBarColor: '#A31621',
      transitionIn: 'fadeInUp',
      transitionOut: 'fadeOut',
      balloon: true
    });
  }

  error(msg: string) {
    this.toaster.error({
      title: 'Error',
      message: msg
    });
  }

  warning(msg: string) {
    this.toaster.warning({
      title: 'Warning',
      message: msg
    })
  }

  message(msg: string) {
    this.toaster.show({
      theme: 'secondary',
      title: msg,
      position: 'bottomCenter',
      progressBarColor: '#A31621',
    });
  }

  logoutMsg(msg: string) {
    this.toaster.show({
      theme: 'secondary',
      title: msg,
      icon: 'far fa-hand-peace',
      position: 'bottomCenter',
      progressBarColor: '#A31621',
      balloon: true
    });
  }

  commentAdded(msg: string) {
    this.toaster.success({
      title: 'OK',
      message: msg,
      transitionIn: 'bounceInLeft'
    });
  }
}
