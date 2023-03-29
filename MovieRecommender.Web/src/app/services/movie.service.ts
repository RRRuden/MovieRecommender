import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http"
import { IMovie } from '../models/movies';
import { environment } from 'src/environments/environment';
import { Observable, tap } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class MovieService {

  movies : IMovie[] = []
  watched : IMovie[] = []
  constructor(private http : HttpClient) { }

  getRange(start : number, end : number) : Observable<IMovie[]> 
    {
        return this.http.get<IMovie[]>(`${environment.apiUrl}/Movie?start=${start}&end=${end}`)
        .pipe(tap(movies => this.movies = movies))
    }

  getBike(id : number) : Observable<IMovie>
    {
        return this.http.get<IMovie>(`${environment.apiUrl}/Movie/${id}`)
    }

  getWatchedRange(userId:number,start : number, end : number) : Observable<IMovie[]> 
    {
        return this.http.get<IMovie[]>(`${environment.apiUrl}/Movie/rated?userId=${userId}&start=${start}&end=${end}`)
        .pipe(tap(movies => this.watched = movies))
    }

  getRangeByName(term : string ,start : number, end : number) : Observable<IMovie[]> 
    {
        return this.http.get<IMovie[]>(`${environment.apiUrl}/Movie/byName?term=${term}&start=${start}&end=${end}`)
        .pipe(tap(movies => this.watched = movies))
    }
}