import { Component, OnInit } from '@angular/core';
import { UpdateRole, User } from 'src/app/models/users';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-admin-page',
  templateUrl: './admin-page.component.html',
  styleUrls: ['./admin-page.component.css']
})
export class AdminPageComponent implements OnInit {

  constructor(public userService : UserService) {}

  ngOnInit(): void {
    this.userService.getAll().subscribe()
  }

  delete(id : number)
  {
    this.userService.delete(id).subscribe(() => this.userService.users.find((item) => {
      if (id === item.id) {
        let idx = this.userService.users.findIndex((data) => data.id === id);
        this.userService.users.splice(idx, 1);
      }
    }));
  }

  update(user : User)
  {
    var request : UpdateRole = {id : user.id, role : user.role}
    console.log(user)
    this.userService.update(request).subscribe((data) => {
      this.userService.users =  this.userService.users.map((user) => {
        if (user.id === data.id) return data;
        else return user;
      });
    });
  }
}
