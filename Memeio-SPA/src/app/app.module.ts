import { appRoutes } from './routes';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { TabsModule } from 'ngx-tabset';
import { JwtModule } from '@auth0/angular-jwt';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { Ng2IziToastModule } from 'ng2-izitoast';
import { FileUploadModule } from 'ng2-file-upload';


// Component/Services Imports:
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { NavComponent } from './nav/nav.component';
import { GalleryCommentsComponent } from './Gallery/Gallery-main/Gallery-comments/Gallery-Comments.component';
import { GalleryMainComponent } from './Gallery/Gallery-main/Gallery-main.component';
import { RegisterComponent } from './register/register.component';
import { AuthService } from './_services/auth.service';
import { ErrorIntercepterProvider } from './_services/error.interceptor';
import { AuthGuard } from './_guards/auth.guard';
import { UserService } from './_services/user.service';
import { SearchResolver } from './_resolvers/search.resolver';
import { ProfileDetailResolver } from './_resolvers/profile-detail.resolver';
import { ProfileDetailComponent } from './profile/profile-detail/profile-detail.component';
import { ToasterService } from './_services/toaster.service';
import { PhotoUploadComponent } from './profile/photo-upload/photo-upload.component';
import { PostCardComponent } from './profile/post-card/post-card.component';
import { ProfileCommentsComponent } from './profile/profile-comments/profile-comments.component';

// This function gets our token from the user's local storage and returns it for usage in our JwtModule
export function tokenGetter() {
   return localStorage.getItem('token');
}

@NgModule({
   declarations: [
      AppComponent,
      HomeComponent,
      NavComponent,
      GalleryCommentsComponent,
      GalleryMainComponent,
      RegisterComponent,
      ProfileDetailComponent,
      PhotoUploadComponent,
      PostCardComponent,
      ProfileCommentsComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      FileUploadModule,
      BrowserAnimationsModule,
      Ng2IziToastModule,
      BsDropdownModule.forRoot(),
      TabsModule.forRoot(),
      JwtModule.forRoot({
         config: {
            tokenGetter,
            whitelistedDomains: ['localhost:5000'],
            blacklistedRoutes: ['localhost:5000/api/v1/auth']
         }
      }),
      RouterModule.forRoot(appRoutes)
   ],
   providers: [
      AuthService,
      ToasterService,
      UserService,
      SearchResolver,
      ProfileDetailResolver,
      ErrorIntercepterProvider,
      AuthGuard
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
