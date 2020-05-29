import { Component, OnInit, Input } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AuthService } from 'src/app/_services/auth.service';
import { ToasterService } from 'src/app/_services/toaster.service';
import { FileUploadModule, FileUploader } from 'ng2-file-upload';
import { Photo } from 'src/app/_models/photo';
import { User } from 'src/app/_models/user';

@Component({
  selector: 'app-photo-upload',
  templateUrl: './photo-upload.component.html',
  styleUrls: ['./photo-upload.component.css'],
})
export class PhotoUploadComponent implements OnInit {
  @Input() user: User; // Child component of 'profile-data'

  fileSizeLimit: number = 10 * 1024 * 1024; // measured in bits (not quite sure);
  uploadFile: any;
  hasBaseDropZoneOver: boolean;
  baseUrl: string = environment.apiUrl;
  uploader: FileUploader;
  response: string;

  constructor(
    private authService: AuthService,
    private toaster: ToasterService
  ) {}

  ngOnInit() {
    this.intitializeUploader();
  }

  fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  intitializeUploader() {
    console.log(this.authService.decodedToken.nameid);
    // Set up our uploader options:
    this.uploader = new FileUploader({
      url:
      this.baseUrl
      + 'user/' +
      this.authService.decodedToken.nameid +
      '/photos',
      authToken: 'Bearer ' + localStorage.getItem('token'),
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: this.fileSizeLimit,
    });

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    };

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const res: Photo = JSON.parse(response);
        const photo = {
          id: res.id,
          url: res.url,
          dateCreated: res.dateCreated,
          author: res.author,
          likes: res.likes,
          dislikes: res.likes,
          favorites: res.favorites,
          comments: res.comments,
        };
        this.user.posts.push(photo);
        this.toaster.success('Upload Successful!');
      }
    };
  }
}
