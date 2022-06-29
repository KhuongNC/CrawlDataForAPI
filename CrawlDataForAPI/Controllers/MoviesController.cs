using CrawlDataForAPI.Repositories;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CrawlDataForAPI.Controllers
{
    public class MoviesController : ApiController
    {
        private readonly IMovieRepository _movieRepository;
        public MoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        /// <summary>
        /// Get movies (12) from website https://www.bhdstar.vn/
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage GetMoviesFromBhdStar()
        {
            try
            {
                var movies = _movieRepository.GetAllMovies();
                return Request.CreateResponse(HttpStatusCode.OK, movies);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.ToString());
            }
        }

        /// <summary>
        /// Get movies by name from website https://www.bhdstar.vn/
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public HttpResponseMessage GetMoviesByNameFromBhdStar(string name)
        {
            try
            {
                var movies = _movieRepository.GetMoviesByName(name);

                if (movies == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Movie with name " + name + " not found");
                }

                return Request.CreateResponse(HttpStatusCode.OK, movies);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.ToString());
            }
        }

        /// <summary>
        /// Get movies by director from website https://www.bhdstar.vn/
        /// </summary>
        /// <param name="director"></param>
        /// <returns></returns>
        public HttpResponseMessage GetMoviesByDirectorFromBHdStar(string director)
        {
            try
            {
                var movies = _movieRepository.GetMoviesByDirector(director);

                if (movies == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No movie(s) with " + director + " not found");
                }

                return Request.CreateResponse(HttpStatusCode.OK, movies);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.ToString());
            }
        }

        /// <summary>
        /// Get movies by type of movie from website https://www.bhdstar.vn/
        /// </summary>
        /// <param name="typeOfMovie"></param>
        /// <returns></returns>
        public HttpResponseMessage GetMoviesByTypeOfMovieFromBhdStar(string typeOfMovie)
        {
            try
            {
                var movies = _movieRepository.GetMoviesByTypeOfMovie(typeOfMovie);

                if (movies == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No movie(s) with " + typeOfMovie + " not found");
                }

                return Request.CreateResponse(HttpStatusCode.OK, movies);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.ToString());
            }
        }
    }

}
