import { Component, OnInit, Input } from '@angular/core';
import { Comments } from 'src/app/_models/comments';

@Component({
  selector: 'app-profile-comments',
  templateUrl: './profile-comments.component.html',
  styleUrls: ['./profile-comments.component.css']
})
export class ProfileCommentsComponent implements OnInit {
  @Input() comment: Comments;
  constructor() { }

  ngOnInit() {
  }

}
