import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Photo } from '../_models/photo';

@Injectable({
  providedIn: 'root',
})
export class ArchiveService {
  baseUrl: string = environment.apiUrl + 'archive/';

  constructor(private http: HttpClient) {}

  getArchivedPhoto(userId: number, id: number) {
    return this.http.get(this.baseUrl + userId + '/' + id);
  }

  getArchivedPhotos(userId: number) {
    return this.http.get(this.baseUrl + userId);
  }

  addToUserArchive(userId: number, photo: Photo) {
    console.log('DATE CREATED ' + photo.dateCreated);
    const archivePhoto: any = {};
    archivePhoto.PhotoId = photo.id;
    archivePhoto.url = photo.url;
    archivePhoto.authorPhotoUrl = photo.authorPhotoUrl;
    archivePhoto.dateCreated = photo.dateCreated;
    archivePhoto.author = photo.author;
    archivePhoto.authorId = photo.authorId;
    archivePhoto.likes = photo.likes;
    archivePhoto.dislikes = photo.dislikes;
    archivePhoto.favorites = photo.favorites;
    archivePhoto.comments = photo.comments;
    return this.http.post(this.baseUrl + userId, archivePhoto);
  }

  deleteFromUserArchive(userId: number, id: number) {
    return this.http.delete(this.baseUrl + userId + '/' + id);
  }
}
