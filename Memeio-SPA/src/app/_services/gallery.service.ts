import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Photo } from '../_models/photo';
import { Comments } from '../_models/comments';

@Injectable({
  providedIn: 'root'
})
export class GalleryService {
  baseUrl = environment.apiUrl + 'gallery';
  constructor(private http: HttpClient) { }

  getPhotoSet(): Observable<Photo[]> {
    return this.http.get<Photo[]>(this.baseUrl);
  }

  updateCommentForPhoto(photoId: number, comment: Comments) {
    return this.http.put(this.baseUrl + '/' + photoId + '/addcomment', comment);
  }

  updateLikeForPhoto(photoId: number, photo: Photo) {
    return this.http.put(this.baseUrl + '/' + photoId + '/addLike', photo);
  }

  updateDislikeForPhoto(photoId: number, photo: Photo) {
    return this.http.put(this.baseUrl + '/' + photoId + '/addDislike', photo);
  }
}
