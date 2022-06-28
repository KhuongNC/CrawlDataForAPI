using CrawlDataForAPI.Models;
using CrawlDataForAPI.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace CrawlDataForAPI.Repositories
{
    public interface IMovieRepository
    {
        List<Movie> GetAllMovies();
        List<Movie> GetMovieByName(string name);
    }
    public class MovieRepository : IMovieRepository
    {
        public List<Movie> GetMovieByName(string name)
        {
            return CrawlData.CrawlDataFromWebsite<Movie>(WebsiteLink.BHDSTAR, Constants.BHDSTAR).Where(m => m.Name.ToLower().Contains(name)).ToList();
        }

        public List<Movie> GetAllMovies()
        {
            return CrawlData.CrawlDataFromWebsite<Movie>(WebsiteLink.BHDSTAR, Constants.BHDSTAR);
        }
    }
}