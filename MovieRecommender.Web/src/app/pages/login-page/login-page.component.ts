import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthRequest } from 'src/app/models/users';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {

  loginForm!: FormGroup;

  constructor( private formBuilder: FormBuilder,
    private router: Router,
    private authenticationService: AuthService)
    {
      if(authenticationService.isLoggedIn())
        this.router.navigate(['/']);
    }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group(
      {
        login : ['', Validators.required],
        password : ['', Validators.required]
      }
    )
  }

    submit()
    {
      if (this.loginForm.invalid) {
        return;
      }

      var user : AuthRequest = {
        login : this.loginForm.value.login,
        password : this.loginForm.value.password
      }

      this.authenticationService.login(user).subscribe(()=> this.router.navigate(['']))
    }
}
