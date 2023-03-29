export interface PredictRequest
{
    movieId : number,
    userId : number
}

export interface Rating
{
    movieId : number,
    userId : number,
    score : number
}

export interface MovieRating
{
    movieId : number,
    userId : number,
}