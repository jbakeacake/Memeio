import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { Comments } from '../_models/comments';
import { Photo } from '../_models/photo';

@Injectable({
  providedIn: 'root',
})
/*
  UserService: Class

  This service is going to communicate with our "UserController" in our API. All HTTP requests will appended with the 'user'
  url location. If a specific user is being queired, the id the user will be appended as well
*/
export class UserService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl + 'user');
  }

  getUser(id: number): Observable<User> {
    return this.http.get<User>(this.baseUrl + 'user/' + id);
  }

  // acutually ADDS a comment to user profile -- updates the receiving user's comment collection
  updateCommentForUser(userId: number, comment: Comments) {
    return this.http.put(this.baseUrl + 'user/' + userId + '/comment', comment);
  }

  deletePhotoForuser(userId: number, photoId: number) {
    return this.http.delete(this.baseUrl + 'user/' + userId + '/photos/' + photoId);
  }
}
