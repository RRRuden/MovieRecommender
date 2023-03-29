using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;
using MoiveRecommender.Domain.Entities;
using MoiveRecommender.Domain.Interfaces;
using MovieRecommender.API.Models;
using MovieRecommender.ML.Models;

namespace MovieRecommender.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RatingController : ControllerBase
{
    private readonly PredictionEnginePool<MovieRating, MoviePredict> _predictionEnginePool;
    private readonly IUnitOfWork _unitOfWork;

    public RatingController(PredictionEnginePool<MovieRating, MoviePredict> predictionEnginePool,
        IUnitOfWork unitOf)
    {
        _predictionEnginePool = predictionEnginePool;
        _unitOfWork = unitOf;
    }

    [HttpPost("Predict")]
    public ActionResult<float> Predict(MovieRating input)
    {
        if (!ModelState.IsValid) return BadRequest();

        var prediction = _predictionEnginePool.Predict(input);

        var score = prediction.Score;

        return Ok(score);
    }

    [HttpPost("Rate")]
    public async Task<ActionResult<string>> Rate(Rating rating)
    {
        if (!ModelState.IsValid) return BadRequest("Invalid model");

        await _unitOfWork.Rating.Create(rating);
        return Ok("Created!");
    }

    [HttpPut]
    public async Task<ActionResult<string>> UpdateRate(Rating request)
    {
        var rating = await _unitOfWork.Rating.Get(request.UserId, request.MovieId);

        if (rating == null) return NotFound("Not Found");

        rating.Score = request.Score;
        await _unitOfWork.Rating.Update(rating);
        return Ok("Updated!");
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(int userId, int movieId)
    {
        var rating = await _unitOfWork.Rating.Get(userId, movieId);
        if (rating == null)
            return NotFound();

        await _unitOfWork.Rating.Delete(rating);
        return Ok();
    }

    [HttpPost("HasRating")]
    public async Task<ActionResult<bool>> HasRating(PredictRequest request)
    {
        var rating = await _unitOfWork.Rating.Get(request.userId, request.movieId);
        return Ok(rating != null);
    }
}