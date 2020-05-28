import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-Gallery-main',
  templateUrl: './Gallery-main.component.html',
  styleUrls: ['./Gallery-main.component.css']
})
export class GalleryMainComponent implements OnInit {

  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
  }

}
