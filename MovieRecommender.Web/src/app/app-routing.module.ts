import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './guards/auth.guard';
import { AboutPageComponent } from './pages/about-page/about-page.component';
import { AdminPageComponent } from './pages/admin-page/admin-page.component';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { MoviePageComponent } from './pages/movie-page/movie-page.component';
import { RegisterPageComponent } from './pages/register-page/register-page.component';
import { SearchPageComponent } from './pages/search-page/search-page.component';
import { WatchedPageComponent } from './pages/watched-page/watched-page.component';
import { MovieResolver } from './services/movie.resolver';
import { SearchResolver } from './services/search.resolver';

const routes: Routes = [
  {path : '', component: MoviePageComponent},
  {path : 'about/:id', component: AboutPageComponent, resolve :{data : MovieResolver}, canActivate : [AuthGuard]},
  {path : 'watched', component:WatchedPageComponent, canActivate : [AuthGuard]},
  {path : 'login', component:LoginPageComponent},
  {path : 'register', component : RegisterPageComponent},
  {path : 'search/:term', component : SearchPageComponent, resolve : {data : SearchResolver}},
  {path : 'admin', component : AdminPageComponent, canActivate : [AuthGuard], data : {roles : ['Admin']}}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
