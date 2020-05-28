import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Photo } from '../_models/photo';

@Injectable({
  providedIn: 'root'
})
export class GalleryService {
  baseUrl = environment.apiUrl + 'gallery';
  constructor(private http: HttpClient) { }

  getPhotoSet(): Observable<Photo[]> {
    return this.http.get<Photo[]>(this.baseUrl);
  }

}
