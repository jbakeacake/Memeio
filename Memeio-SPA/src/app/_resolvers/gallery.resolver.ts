import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { ToasterService } from '../_services/toaster.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Photo } from '../_models/photo';
import { GalleryService } from '../_services/gallery.service';

@Injectable()
/**
 * ProfileSearchResolver: class
 *
 * This resolver is going to inject data into the initial HTTP request to visit the home page. It contains a list of all users
 * in the database. This data will be used when searching for another user via the search bar.
 */
export class HomeResolver implements Resolve<Photo[]> {
  constructor(
    private galleryService: GalleryService,
    private router: Router,
    private toaster: ToasterService
  ) {}

  resolve(route: ActivatedRouteSnapshot): Observable<Photo[]> {
      return this.galleryService.getPhotoSet().pipe(
          catchError(err => {
              this.toaster.error('Problem retrieving photo set data');
              this.router.navigate(['/home']);
              return of(null);
          })
      );
  }
}
