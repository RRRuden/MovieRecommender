import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { MovieRating, Rating } from '../models/predicts';

@Injectable({
  providedIn: 'root'
})
export class RatingService {

  constructor(private http : HttpClient) { }

  HasPredict(userId : number, movieId : number) : Observable<boolean>
  {
    let request = {
      userId : userId,
      movieId : movieId
    }
    return this.http.post<boolean>(`${environment.apiUrl}/Rating/HasRating`, request)
  }

  SetRating(rating : Rating) : Observable<string>
  {
    return this.http.post(`${environment.apiUrl}/Rating/Rate`, rating, {responseType : 'text'})
  }

  UpdateRating(rating : Rating) : Observable<string>
  {
    return this.http.put(`${environment.apiUrl}/Rating`, rating, {responseType : 'text'})
  }

  GetPredict(rating : MovieRating) : Observable<number>
  {
    return this.http.post<number>(`${environment.apiUrl}/Rating/Predict`, rating);
  }

  DeleteRating(userId : number, movieId : number ) : Observable<any>
  {
    return this.http.delete(`${environment.apiUrl}/Rating?userId=${userId}&movieId=${movieId}`)
  }
}