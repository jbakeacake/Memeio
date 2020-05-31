import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/_services/user.service';
import { User } from 'src/app/_models/user';
import { Comments } from 'src/app/_models/comments';
import { ToasterService } from 'src/app/_services/toaster.service';
import { AuthService } from 'src/app/_services/auth.service';
import { CommentStmt } from '@angular/compiler';

@Component({
  selector: 'app-profile-detail',
  templateUrl: './profile-detail.component.html',
  styleUrls: ['./profile-detail.component.css'],
})
export class ProfileDetailComponent implements OnInit {
  userInput: string;
  editButtonValue: string;
  editMode: boolean;
  user: User;
  model: any = {};

  commentSubscription: any;

  constructor(
    private userService: UserService,
    private authService: AuthService,
    private route: ActivatedRoute,
    private toaster: ToasterService
  ) {}

  ngOnInit() {
    this.route.data.subscribe((data) => {
      // Have our route examine the data of the current user
      this.user = data['user'];
    });
    console.log('This pages user:' + this.user.username);
  }

  addComment() {
    this.userService.updateCommentForUser(this.user.id, this.model).subscribe(
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
        this.user.comments.push(comment);
        this.model.content = '';
      }
    );
  }

  enableEditMode(button) {
    this.editMode = !this.editMode;
    if (!this.editMode) {
      button.textContent = 'Edit Collections';
    } else {
      button.textContent = 'Stop Editing';
    }
  }
}
