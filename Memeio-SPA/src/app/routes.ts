import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { GalleryMainComponent } from './Gallery/Gallery-main/Gallery-main.component';
import { AuthGuard } from './_guards/auth.guard';
import { ProfileDetailComponent } from './profile/profile-detail/profile-detail.component';
import { ProfileDetailResolver } from './_resolvers/profile-detail.resolver';
import { SearchResolver } from './_resolvers/search.resolver';
import { NavComponent } from './nav/nav.component';
import { GalleryResolver } from './_resolvers/gallery.resolver';


export const appRoutes: Routes = [
  { path: '', component: HomeComponent},
  { path: 'register', component: RegisterComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'gallery', component: GalleryMainComponent, resolve: {photoSet: GalleryResolver}},
      { path: 'profiles/:id', component: ProfileDetailComponent, resolve: {user: ProfileDetailResolver}}
    ]
  },
  { path: '**', redirectTo: '', pathMatch: 'full' }
];
