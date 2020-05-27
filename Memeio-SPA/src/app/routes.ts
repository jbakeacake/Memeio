import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { GalleryMainComponent } from './Gallery/Gallery-main/Gallery-main.component';

export const appRoutes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'gallery', component: GalleryMainComponent }, // TODO: Setup guards
  { path: '**', redirectTo: '', pathMatch: 'full' },
];
