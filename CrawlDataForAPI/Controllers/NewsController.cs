using CrawlDataForAPI.Repositories;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CrawlDataForAPI.Controllers
{
    public class NewsController : ApiController
    {
        private readonly INewsRepository _newsRepository;
        public NewsController(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        /// <summary>
        /// Get news (30) from website https://kenh14.vn/
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage GetAllNewsFromKenh14()
        {
            try
            {
                var newsList = _newsRepository.GetAllNews();
                return Request.CreateResponse(HttpStatusCode.OK, newsList);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.ToString());
            }
        }

        /// <summary>
        /// Get movie by name from website https://kenh14.vn/
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public HttpResponseMessage GetNewsFromKenh14ByTitle(string title)
        {
            try
            {
                var news = _newsRepository.GetNewsByTitle(title);

                if (news.Count() == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "News with name " + title + " not found");
                }

                return Request.CreateResponse(HttpStatusCode.OK, news);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.ToString());
            }
        }

        /// <summary>
        /// Get movie by type of news from website https://kenh14.vn/
        /// </summary>
        /// <param name="typeOfNews"></param>
        /// <returns></returns>
        public HttpResponseMessage GetNewsFromKenh14ByTypeOfNews(string typeOfNews)
        {
            try
            {
                var news = _newsRepository.GetNewsByTypeOfNews(typeOfNews);

                if (news.Count() == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No news with " + typeOfNews);
                }

                return Request.CreateResponse(HttpStatusCode.OK, news);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.ToString());
            }
        }
    }
}
