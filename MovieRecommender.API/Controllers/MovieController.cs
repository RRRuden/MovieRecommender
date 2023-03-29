using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoiveRecommender.Domain.Entities;
using MoiveRecommender.Domain.Interfaces;

namespace MovieRecommender.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MovieController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public MovieController(IUnitOfWork unitOf)
    {
        _unitOfWork = unitOf;
    }

    [HttpGet]
    public async Task<List<Movie>> GetRange(int start, int end)
    {
        return await _unitOfWork.Movie.GetRange(start, end);
    }

    [HttpGet("rated")]
    [Authorize]
    public async Task<List<Movie>> GetRatedMoviesRange(int userId, int start, int end)
    {
        return await _unitOfWork.Movie.GetRatedRange(userId, start, end);
    }

    [HttpGet("byName")]
    public async Task<IEnumerable<Movie?>> GetRangeByName(string term, int start, int end)
    {
        return await _unitOfWork.Movie.GetRangeByName(term, start, end);
    }

    [HttpGet("{id}")]
    public async Task<Movie?> GetById(int id)
    {
        return await _unitOfWork.Movie.GetById(id);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Delete(int id)
    {
        var movie = await _unitOfWork.Movie.GetById(id);
        if (movie == null)
            return NotFound();

        await _unitOfWork.Movie.Delete(movie);
        return Ok();
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Movie>> Update(Movie request)
    {
        var movie = await _unitOfWork.Movie.GetById(request.Id);
        if (movie == null)
            return NotFound();

        movie.Title = request.Title;
        movie.Genres = request.Genres;
        movie.ImgURL = request.ImgURL;

        await _unitOfWork.Movie.Update(movie);

        return Ok(movie);
    }
}