import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ActivatedRouteSnapshot, Route } from '@angular/router';
import { Subscription } from 'rxjs';
import { IMovie } from 'src/app/models/movies';
import { MovieService } from 'src/app/services/movie.service';

@Component({
  selector: 'app-search-page',
  templateUrl: './search-page.component.html',
  styleUrls: ['./search-page.component.css']
})
export class SearchPageComponent implements OnInit {

  term : string = ''
  movies : IMovie[] = []
  pageNumber : number = 1
  movieCount : number = 20
  movieSubscribtion!: Subscription;
  constructor(private route: ActivatedRoute, public movieService:MovieService){
  }

  ngOnInit(): void {

    this.movieSubscribtion = this.route.data.subscribe((data)=>
    {
      this.term = data['data'];
    });
    
    this.movieService
    .getRangeByName(this.term, (this.pageNumber * this.movieCount) - this.movieCount, this.pageNumber * this.movieCount)
    .subscribe(x=>this.movies = x)
  }

  next()
  {
    this.pageNumber+=1;
    this.movieService.
    getRangeByName(this.term, (this.pageNumber * this.movieCount) - this.movieCount, this.pageNumber * this.movieCount).
    subscribe(x=>this.movies = x)
  }


  prev()
  {
    this.pageNumber-=1;
    this.movieService.
    getRangeByName(this.term, (this.pageNumber * this.movieCount) - this.movieCount, this.pageNumber * this.movieCount).
    subscribe(x=>this.movies = x)
  }

}
