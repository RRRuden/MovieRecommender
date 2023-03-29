import { Component, Input, OnInit } from '@angular/core';
import { IMovie } from "src/app/models/movies";

@Component({
  selector: 'app-movie-list-item',
  templateUrl: './movie-list-item.component.html',
  styleUrls: ['./movie-list-item.component.css']
})

export class MovieListItemComponent implements OnInit {


@Input()
  movie!: IMovie;

constructor(){}

ngOnInit(): void {
}

}
