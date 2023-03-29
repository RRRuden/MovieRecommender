import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthRequest } from 'src/app/models/users';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.css']
})
export class RegisterPageComponent {
  registerForm!: FormGroup;

  constructor( private formBuilder: FormBuilder,
    private router: Router,
    private authenticationService: AuthService)
    {
      if(authenticationService.isLoggedIn())
        this.router.navigate(['/']);
    }



  ngOnInit(): void {
    this.registerForm = this.formBuilder.group(
      {
        login : ['', Validators.required],
        password : ['', Validators.required]
      }
    )
  }

    submit()
    {
      if (this.registerForm.invalid) {
        return;
      }

      var user : AuthRequest = {
        login : this.registerForm.value.login,
        password : this.registerForm.value.password
      }

      this.authenticationService.register(user).subscribe(()=> this.router.navigate(['/login']))
    }
}
