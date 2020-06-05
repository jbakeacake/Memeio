import { Component, OnInit, Input } from '@angular/core';
import { Comments } from 'src/app/_models/comments';

@Component({
  selector: 'app-Gallery-comments',
  templateUrl: './Gallery-comments.component.html',
  styleUrls: ['./Gallery-comments.component.css']
})
export class GalleryCommentsComponent implements OnInit {
  @Input() comment: Comments;
  constructor() { }

  ngOnInit() {
  }

}
