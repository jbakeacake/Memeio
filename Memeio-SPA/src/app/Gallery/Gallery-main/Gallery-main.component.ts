import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Photo } from 'src/app/_models/photo';
import { GalleryService } from 'src/app/_services/gallery.service';
import { ToasterService } from 'src/app/_services/toaster.service';
import { ArchiveService } from 'src/app/_services/archive.service';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-Gallery-main',
  templateUrl: './Gallery-main.component.html',
  styleUrls: ['./Gallery-main.component.css'],
})
export class GalleryMainComponent implements OnInit {
  photoSet: Photo[];
  currentPhoto: Photo;

  constructor(
    private galleryService: GalleryService,
    private authService: AuthService,
    private archiveService: ArchiveService,
    private route: ActivatedRoute,
    private toaster: ToasterService
  ) {}

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.photoSet = data['photoSet'];
    });
    this.photoSet.unshift(null);
    console.log(this.photoSet);
    this.pop();
    console.log(this.currentPhoto);
  }

  sendToUrl(url: string) {
    window.open(url);
  }

  pop() {
    if (this.photoSet.length === 0) {
      return;
    }
    this.currentPhoto = this.photoSet.pop();
  }

  ratePhoto(event: any) {
    if (this.photoSet.length === 0) {
      return;
    }

    if (event.key === 'ArrowRight') {
      this.galleryService
        .updateLikeForPhoto(this.currentPhoto.id, this.currentPhoto)
        .subscribe(() => {
          this.pop();
          this.toaster.liked('Liked!');
        });
    } else if (event.key === 'ArrowLeft') {
      this.galleryService
        .updateDislikeForPhoto(this.currentPhoto.id, this.currentPhoto)
        .subscribe(() => {
          this.toaster.disliked('Disliked!');
          this.pop();
        });
    }
  }

  archivePhoto() {
    this.archiveService
      .addToUserArchive(
        parseInt(this.authService.decodedToken.nameid, 10),
        this.currentPhoto
      )
      .subscribe(
        () => {
          this.toaster.success('Photo Archived!');
        },
        (err) => {
          this.toaster.error(err);
        }
      );
  }
}
