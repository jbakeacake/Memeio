import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Photo } from 'src/app/_models/photo';
import { GalleryService } from 'src/app/_services/gallery.service';
import { ToasterService } from 'src/app/_services/toaster.service';

@Component({
  selector: 'app-Gallery-main',
  templateUrl: './Gallery-main.component.html',
  styleUrls: ['./Gallery-main.component.css'],
})
export class GalleryMainComponent implements OnInit {
  photoSet: Photo[];
  currentPhotoUrl: string;

  constructor(
    private galleryService: GalleryService,
    private route: ActivatedRoute,
    private toaster: ToasterService
  ) {}

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.photoSet = data['photoSet'];
    });
    console.log(this.photoSet);
    this.currentPhotoUrl = this.photoSet.pop().url;
  }

  sendToUrl(url: string) {
    window.open(url);
  }

  pop() {
    this.currentPhotoUrl = this.photoSet.pop().url;
  }

  ratePhoto(event: any) {
    if (this.photoSet.length === 0) {
      return;
    }

    if (event.key === 'ArrowRight') {
      this.pop();
      this.toaster.liked('Liked!');
    } else if (event.key === 'ArrowLeft') {
      this.pop();
      this.toaster.disliked('Disliked!');
    }
  }
}
