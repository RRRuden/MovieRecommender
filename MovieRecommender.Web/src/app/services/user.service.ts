import { HttpClient} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { UpdateRole, User } from '../models/users';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  users : User[] = []
  constructor(private http : HttpClient) { }

  getAll() : Observable<User[]>
  {
    return this.http.get<User[]>(`${environment.apiUrl}/User/All`)
        .pipe(tap(u => this.users = u) )
  }

  delete(id : number) : Observable<User>
  {
      return this.http.delete<User>(`${environment.apiUrl}/User/${id}`)
  }

  update(user : UpdateRole) : Observable<User>
  {
      return this.http.put<User>(`${environment.apiUrl}/User/UpdateRole`, user)
  }
}
