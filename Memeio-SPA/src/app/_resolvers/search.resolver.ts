import { Injectable } from '@angular/core';
import { User } from '../_models/user';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { UserService } from '../_services/user.service';
import { ToasterService } from '../_services/toaster.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
/**
 * ProfileSearchResolver: class
 *
 * This resolver is going to inject data into the initial HTTP request to visit the home page. It contains a list of all users
 * in the database. This data will be used when searching for another user via the search bar.
 */
export class SearchResolver implements Resolve<User[]> {
  constructor(
    private userService: UserService,
    private router: Router,
    private toaster: ToasterService
  ) {}

  resolve(route: ActivatedRouteSnapshot): Observable<User[]> {
      return this.userService.getUsers().pipe(
          catchError(err => {
              this.toaster.error('Problem retrieving user data');
              this.router.navigate(['/home']);
              return of(null);
          })
      );
  }
}
