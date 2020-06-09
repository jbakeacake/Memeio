import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Photo } from 'src/app/_models/photo';
import { GalleryService } from 'src/app/_services/gallery.service';
import { ToasterService } from 'src/app/_services/toaster.service';
import { ArchiveService } from 'src/app/_services/archive.service';
import { AuthService } from 'src/app/_services/auth.service';
import { state, style, transition, animate, trigger } from '@angular/animations';

@Component({
  selector: 'app-Gallery-main',
  animations: [
    trigger('isRating', [
      state('idle', style({
        opacity: '1'
      })),
      state('liking', style({
        transform: 'matrix(1, 0.3, -0.3, 1, 400, 100)',
        opacity: 0
      })),
      state('disliking', style({
        transform: 'matrix(1, -0.3, 0.3, 1, -400, 100)',
        opacity: 0
      })),
      transition('idle => liking', [
        animate('0.3s')
      ]),
      transition('idle => disliking', [
        animate('0.3s')
      ]),
      transition('liking => idle', [
        animate('0.0s')
      ]),
      transition('disliking => idle', [
        animate('0.0s')
      ])
    ])
  ],
  templateUrl: './Gallery-main.component.html',
  styleUrls: ['./Gallery-main.component.css'],
})
export class GalleryMainComponent implements OnInit {
  photoSet: Photo[];
  currentPhoto: Photo;
  currentAnimState: string = 'idle';

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
      this.toggleLikeState();
      setTimeout(() => this.addLike(), 1000);
    } else if (event.key === 'ArrowLeft') {
      this.toggleDislikeState();
      setTimeout(() => this.addDislike(), 1000);
    }
  }

  addLike() {
    this.toggleLikeState();
    this.galleryService
    .updateLikeForPhoto(this.currentPhoto.id, this.currentPhoto)
    .subscribe(() => {
      this.pop();
      this.toaster.liked('Liked!');
    });
  }

  addDislike() {
    this.toggleDislikeState();
    this.galleryService
    .updateDislikeForPhoto(this.currentPhoto.id, this.currentPhoto)
    .subscribe(() => {
      this.toaster.disliked('Disliked!');
      this.pop();
    });
  }

  toggleLikeState() {
    this.currentAnimState = this.currentAnimState === 'idle' ? 'liking' : 'idle';
  }

  toggleDislikeState() {
    this.currentAnimState = this.currentAnimState === 'idle' ? 'disliking' : 'idle';
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
