import { Component, Input, OnInit } from '@angular/core';
import { MovieService } from 'src/app/services/movie.service';

@Component({
  selector: 'app-movie-page',
  templateUrl: './movie-page.component.html',
  styleUrls: ['./movie-page.component.css']
})
export class MoviePageComponent implements OnInit {

  pageNumber : number = 1
  movieCount : number = 18

  constructor(public movieService : MovieService){}

  ngOnInit(): void {
    this.movieService.getRange( (this.pageNumber * this.movieCount) - this.movieCount, this.pageNumber * this.movieCount).subscribe()
  }

  next()
  {
    this.pageNumber+=1;
    this.movieService.getRange( (this.pageNumber * this.movieCount) - this.movieCount, this.pageNumber * this.movieCount).subscribe()
  }


  prev()
  {
    this.pageNumber-=1;
    this.movieService.getRange( (this.pageNumber * this.movieCount) - this.movieCount, this.pageNumber * this.movieCount).subscribe()
  }
}
