using CrawlDataForAPI.Models;
using CrawlDataForAPI.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace CrawlDataForAPI.Repositories
{
    public interface IMovieRepository
    {
        List<Movie> GetAllMovies();
        List<Movie> GetMoviesByName(string name);
        List<Movie> GetMoviesByDirector(string director);
        List<Movie> GetMoviesByTypeOfMovie(string typeOfMovie);
    }
    public class MovieRepository : IMovieRepository
    {
        public List<Movie> GetMoviesByName(string name)
        {
            return CrawlData.CrawlDataFromWebsite<Movie>(WebsiteLink.BHDSTAR, Constants.BHDSTAR).Where(m => m.Name.ToLower().Contains(name)).ToList();
        }

        public List<Movie> GetAllMovies()
        {
            return CrawlData.CrawlDataFromWebsite<Movie>(WebsiteLink.BHDSTAR, Constants.BHDSTAR);
        }

        public List<Movie> GetMoviesByDirector(string director)
        {
            return CrawlData.CrawlDataFromWebsite<Movie>(WebsiteLink.BHDSTAR, Constants.BHDSTAR).Where(m => m.Director.ToLower() == director.ToLower()).ToList();
        }

        public List<Movie> GetMoviesByTypeOfMovie(string typeOfMovie)
        {
            return CrawlData.CrawlDataFromWebsite<Movie>(WebsiteLink.BHDSTAR, Constants.BHDSTAR).Where(m => m.TypeOfMovie.ToLower() == typeOfMovie.ToLower()).ToList();
        }
    }
}