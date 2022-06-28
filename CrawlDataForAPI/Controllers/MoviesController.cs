using CrawlDataForAPI.Repositories;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CrawlDataForAPI.Controllers
{
    public class MoviesController : ApiController
    {
        private IMovieRepository _movieRepository;
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
                var movies = _movieRepository.GetMovieByName(name);

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

    }

}
