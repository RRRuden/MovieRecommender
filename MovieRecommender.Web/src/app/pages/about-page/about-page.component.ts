import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { IMovie } from 'src/app/models/movies';
import { MovieRating, Rating } from 'src/app/models/predicts';
import { AuthService } from 'src/app/services/auth.service';
import { RatingService } from 'src/app/services/rating.service';

@Component({
  selector: 'app-about-page',
  templateUrl: './about-page.component.html',
  styleUrls: ['./about-page.component.css']
})
export class AboutPageComponent implements OnInit{

  movie!: IMovie;
  movieSubscribtion!: Subscription;
  rating : number = 1;
  predict : number = 0;
  rated : boolean = true;

  constructor(private route: ActivatedRoute, 
     private authService : AuthService, 
     private ratingService : RatingService){}

  ngOnInit(): void {
    this.movieSubscribtion = this.route.data.subscribe((data)=>
    {
      this.movie = data['data'];
    });
    this.ratingService.HasPredict(this.authService.getId(), this.movie.id).subscribe(x=> {this.rated = x})
  }

  rate()
  {
    const ratingModel : Rating = 
    {
      userId : this.authService.getId(),
      movieId : this.movie.id,
      score : this.rating
    }

    if(this.rated)
      this.ratingService.UpdateRating(ratingModel).subscribe()
    else
      this.ratingService.SetRating(ratingModel).subscribe(x=>{this.rated = !this.rated})
  }

  remove()
  {
    this.ratingService.DeleteRating(this.authService.getId(), this.movie.id).subscribe(x=>{this.rated = !this.rated})
  }

  GetPredict()
  {
    const ratingModel : MovieRating = 
    {
      userId : this.authService.getId(),
      movieId : this.movie.id,
    }
    this.ratingService.GetPredict(ratingModel).subscribe(x=>{this.predict = x})
  }

  changeFn(e : any)
  {
    this.rating = e.target.value;
  }
}
