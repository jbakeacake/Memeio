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
  editMode: boolean;
  isAllSelected: boolean;
  user: User;
  model: any = {};
  selectedPhotoIds: number[] = [];

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

  selectAll() {
    this.isAllSelected = !this.isAllSelected;
  }

  deselectAll() {
    this.selectedPhotoIds = [];
    this.isAllSelected = false;
  }

  pushPopPhotoIdList(selectionInfo: any) {

    if (selectionInfo.isSelected) {
      this.selectedPhotoIds.push(selectionInfo.id);
    } else {
      const idx = this.selectedPhotoIds.indexOf(selectionInfo.id);
      if (idx !== -1) {
        this.selectedPhotoIds.splice(idx, 1);
      }
    }
    console.log(this.selectedPhotoIds);
  }

  all_pushPopPhotoIdList() {
    if (this.isAllSelected) {
      // Clean our list to ensure no duplicates:
      this.selectedPhotoIds = [];
      this.user.posts.forEach(post => {
        this.selectedPhotoIds.push(post.id);
      });
    } else {
      this.selectedPhotoIds = [];
    }
    console.log(this.selectedPhotoIds);
  }

  deleteSelected() {
    this.toaster.confirm('Are you sure you want to delete ' + this.selectedPhotoIds.length + ' photos?', () => {
      this.selectedPhotoIds.forEach(photoId => {
        console.log('Deleting: ' + photoId);
        this.userService.deletePhotoForuser(this.user.id, photoId).subscribe(() => {
          this.user.posts.splice(this.user.posts.findIndex(p => p.id === photoId), 1);
        }, err => {
          this.toaster.error('Error: Failed to delete photo');
        }, () => {
          this.toaster.success('Photo Deleted!');
        });
      });
    });
  }

  enableEditMode(button) {
    this.editMode = !this.editMode;
    if (!this.editMode) {
      button.textContent = 'Edit Collections';
    } else {
      this.deselectAll();
      button.textContent = 'Stop Editing';
    }
  }
}
