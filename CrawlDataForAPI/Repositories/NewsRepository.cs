using CrawlDataForAPI.Models;
using CrawlDataForAPI.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace CrawlDataForAPI.Repositories
{
    public interface INewsRepository
    {
        List<News> GetAllNews();
        List<News> GetNewsByTitle(string title);
        List<News> GetNewsByTypeOfNews(string typeOfNews);
    }
    public class NewsRepository : INewsRepository
    {
        public List<News> GetNewsByTitle(string title)
        {
            return CrawlData.CrawlDataFromWebsite<News>(WebsiteLink.KENH14, Constants.KENH14).Where(m => m.Title.ToLower().Contains(title.ToLower())).ToList();
        }

        public List<News> GetAllNews()
        {
            return CrawlData.CrawlDataFromWebsite<News>(WebsiteLink.KENH14, Constants.KENH14);
        }

        public List<News> GetNewsByTypeOfNews(string typeOfNews)
        {
            return CrawlData.CrawlDataFromWebsite<News>(WebsiteLink.KENH14, Constants.KENH14).Where(m => m.TypeOfNews.ToLower().Contains(typeOfNews.ToLower())).ToList();
        }
    }
}