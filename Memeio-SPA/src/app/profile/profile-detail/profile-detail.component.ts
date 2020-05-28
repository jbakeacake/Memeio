import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/_services/user.service';
import { User } from 'src/app/_models/user';
import { Comments } from 'src/app/_models/comments';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-profile-detail',
  templateUrl: './profile-detail.component.html',
  styleUrls: ['./profile-detail.component.css'],
})
export class ProfileDetailComponent implements OnInit {
  user: User;
  model: any = {};

  constructor(
    private userService: UserService,
    private authService: AuthService,
    private route: ActivatedRoute,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    this.route.data.subscribe((data) => {
      // Have our route examine the data of the current user
      this.user = data['user'];
    });
    console.log('This pages user:' + this.user.username);
  }

  addComment() {
    this.userService.updateCommentForUser(this.user.id, this.model).subscribe(() => {
      this.alertify.success('Comment Added!');
      this.model.author = this.authService.decodedToken.unique_name;
      this.user.comments.push(this.model);
    }, err => {
      this.alertify.error(err);
    })
  }
}
