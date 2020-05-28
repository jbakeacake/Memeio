import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Photo } from '../_models/photo';
import { PhotosService } from '../_services/photos.service';

@Injectable()
/**
 * ProfileSearchResolver: class
 *
 * This resolver is going to inject data into the initial HTTP request to visit the home page. It contains a list of all users
 * in the database. This data will be used when searching for another user via the search bar.
 */
export class HomeResolver implements Resolve<Photo[]> {
  constructor(
    private photosService: PhotosService,
    private router: Router,
    private alertify: AlertifyService
  ) {}

  resolve(route: ActivatedRouteSnapshot): Observable<Photo[]> {
      return this.photosService.getPhotoSet().pipe(
          catchError(err => {
              this.alertify.error('Problem retrieving photo set data');
              this.router.navigate(['/home']);
              return of(null);
          })
      );
  }
}
