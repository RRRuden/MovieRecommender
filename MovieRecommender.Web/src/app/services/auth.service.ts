import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, map } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { AuthRequest, UserWithToken } from '../models/users';

@Injectable({
  providedIn: 'root'
})
export class AuthService implements OnInit {

  constructor(private router : Router, private http : HttpClient) { }

  ngOnInit(): void {
    localStorage.clear()
  }

  setToken(token : string)
  {
    localStorage.setItem('token', token)
  }

  getToken()
  {
    return localStorage.getItem('token')
  }

  setRole(role : string)
  {
    localStorage.setItem('role', role)
  }

  getRole()
  {
    return localStorage.getItem('role')
  }

  isLoggedIn()
  {
    return this.getToken() !== null
  }

  setUserId(id : number)
  {
    localStorage.setItem('id', id.toString())
  }

  getId()
  {
    return Number(localStorage.getItem('id'))
  }

  login(user : AuthRequest) : Observable<UserWithToken>
  {
    return this.http.post<UserWithToken>(`${environment.apiUrl}/Auth/login`, user).pipe(map(user => {
      this.setToken(user.token);
      this.setRole(user.role);
      this.setUserId(user.id)
      return user;
    }));
  }

  
  register(user : AuthRequest) : Observable<string>
  {
    return this.http.post(`${environment.apiUrl}/Auth/register`, user, {responseType : 'text'});
  }

  logout() {
    localStorage.removeItem('role');
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }

}
