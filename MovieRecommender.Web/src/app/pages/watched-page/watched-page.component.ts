import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { MovieService } from 'src/app/services/movie.service';

@Component({
  selector: 'app-watched-page',
  templateUrl: './watched-page.component.html',
  styleUrls: ['./watched-page.component.css']
})
export class WatchedPageComponent {
  pageNumber : number = 1
  movieCount : number = 9

  constructor(public movieService : MovieService, private authService : AuthService){}

  ngOnInit(): void {
    this.movieService.
    getWatchedRange(this.authService.getId() ,(this.pageNumber * this.movieCount) - this.movieCount, this.pageNumber * this.movieCount).
    subscribe()
  }

  next()
  {
    this.pageNumber+=1;
    this.movieService.getWatchedRange(this.authService.getId() ,(this.pageNumber * this.movieCount) - this.movieCount, this.pageNumber * this.movieCount).subscribe()
  }


  prev()
  {
    this.pageNumber-=1;
    this.movieService.getWatchedRange( this.authService.getId() ,(this.pageNumber * this.movieCount) - this.movieCount, this.pageNumber * this.movieCount).subscribe()
  }
}
