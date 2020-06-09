import { Component, OnInit, Input } from '@angular/core';
import { Comments } from 'src/app/_models/comments';
import { UserService } from 'src/app/_services/user.service';
import { AuthService } from 'src/app/_services/auth.service';
import { User } from 'src/app/_models/user';
import { ToasterService } from 'src/app/_services/toaster.service';
import { Photo } from 'src/app/_models/photo';
import { GalleryService } from 'src/app/_services/gallery.service';

@Component({
  selector: 'app-Gallery-comments',
  templateUrl: './Gallery-comments.component.html',
  styleUrls: ['./Gallery-comments.component.css']
})
export class GalleryCommentsComponent implements OnInit {
  @Input() photo: Photo;
  @Input() comment: Comments;
  model: any = {};
  constructor(
    private galleryService: GalleryService,
    private authService: AuthService,
    private toaster: ToasterService
    ) { }

  ngOnInit() {
  }

  addComment() {
    console.log(this.photo);
    this.galleryService.addCommentForPhoto(this.photo.id, this.model).subscribe(
      () => {
        this.toaster.commentAdded('Comment Added!');
        this.model.author = this.authService.decodedToken.unique_name;
      },
      (err) => {
        this.toaster.error(err);
      },
      () => {
        // Create a local copy so we're not editing all comments that we're pushed in a single sitting:
        const comment: any = {};
        comment.author = this.model.author;
        comment.authorId = this.model.authorId;
        comment.content = this.model.content;
        comment.userId = this.model.userId;
        this.photo.comments.push(comment);
        this.model.content = '';
      }
    );
  }

}
