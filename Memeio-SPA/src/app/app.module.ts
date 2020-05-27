import { appRoutes } from './routes';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { JwtModule } from '@auth0/angular-jwt';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';


// Component Imports:
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { NavComponent } from './nav/nav.component';
import { GalleryCommentsComponent } from './Gallery/Gallery-main/Gallery-comments/Gallery-Comments.component';
import { GalleryMainComponent } from './Gallery/Gallery-main/Gallery-main.component';
import { RegisterComponent } from './register/register.component';
import { AuthService } from './_services/auth.service';
import { AlertifyService } from './_services/alertify.service';
import { ErrorIntercepterProvider } from './_services/error.interceptor';

@NgModule({
   declarations: [
      AppComponent,
      HomeComponent,
      NavComponent,
      GalleryCommentsComponent,
      GalleryMainComponent,
      RegisterComponent
   ],
   imports: [
      HttpClientModule,
      FormsModule,
      BrowserAnimationsModule,
      BsDropdownModule.forRoot(),
      JwtModule,
      BrowserModule,
      RouterModule.forRoot(appRoutes)
   ],
   providers: [
      AuthService,
      AlertifyService,
      ErrorIntercepterProvider
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
