import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Photo } from '../_models/photo';

@Injectable({
  providedIn: 'root',
})
export class ArchiveService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  addPhotoToArchive(userId: number, photo: Photo) {
    return this.http.post(this.baseUrl + userId + '/archive', {
      PhotoId: photo.id,
      UserId: userId,
    });
  }

  getArchivedPhotos(userId: number) {
    return this.http.get(this.baseUrl + userId + '/archive/photos');
  }

  getArchivedIds(userId: number) {
    return this.http.get(this.baseUrl + userId + '/archive/ids');
  }

  deleteArchivedPhoto(userId: number, id: number) {
    return this.http.delete(this.baseUrl + userId + '/archive/' + id);
  }
}
