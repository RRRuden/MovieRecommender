import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { catchError, EMPTY, Observable, of } from 'rxjs';
import { IMovie } from '../models/movies';
import { MovieService } from './movie.service';

@Injectable({
  providedIn: 'root'
})
export class MovieResolver implements Resolve<IMovie> {
  constructor(private movieService : MovieService, private router : Router){}
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<IMovie> {
    return this.movieService.getBike(route.params?.['id']).pipe( catchError(()=> 
    {
      this.router.navigate(['']);
      return EMPTY;
    }))
  }
}
