using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrawlDataForAPI.Models;

namespace CrawlDataForAPI.Utilities
{
    public static class CrawlData
    {
        public static List<T> CrawlDataFromWebsite<T>(string url, string website)
        {
            List<T> objList = new List<T>();
            object obj = new object();

            HtmlWeb htmlWeb = new HtmlWeb()
            {
                AutoDetectEncoding = false,
                OverrideEncoding = Encoding.UTF8
            };

            // Load web, store data into document
            HtmlAgilityPack.HtmlDocument document = htmlWeb.Load(url);

            // Parent node
            List<HtmlNode> nodeList = new List<HtmlNode>();

            switch (website)
            {
                case Constants.BHDSTAR:
                    nodeList = document.DocumentNode.QuerySelectorAll("li#film-1 div ul.slides > li").ToList();

                    foreach (var item in nodeList)
                    {
                        // Get link to move detail page
                        var movieLinkNode = item.QuerySelector("div.film--item a");
                        string movieLink = movieLinkNode.Attributes["href"].Value.Trim();

                        // Get data from detail page
                        obj = CrawlDataFromDetailPage<Movie>(movieLink, website);

                        if (obj != null)
                        {
                            objList.Add((T)obj);
                        }
                    }
                    break;
                case Constants.KENH14:
                    nodeList = document.DocumentNode.QuerySelectorAll("ul.knsw-list > div > li").ToList();

                    if (nodeList != null)
                    {

                        foreach (var item in nodeList)
                        {
                            // Get link to move detail page
                            var aNode = item.QuerySelector("div.knswli-left a");

                            if (aNode != null)
                            {
                                string newsLink = WebsiteLink.KENH14 + aNode.Attributes["href"].Value.Trim();

                                // Get data from detail page
                                obj = CrawlDataFromDetailPage<News>(newsLink, website, item);

                                if (obj != null)
                                {
                                    objList.Add((T)obj);
                                }
                            }
                        }
                    }
                    break;
            }

            return objList;
        }

        private static T CrawlDataFromDetailPage<T>(string url, string website, HtmlNode parentNodeFromMainPage = null)
        {
            object obj = new object();

            HtmlWeb htmlWeb = new HtmlWeb()
            {
                AutoDetectEncoding = false,
                OverrideEncoding = Encoding.UTF8
            };

            // Load web, store data into document
            HtmlAgilityPack.HtmlDocument document = htmlWeb.Load(url);

            HtmlNode[] detailMovieNodeArr = new HtmlNode[25];

            // Parent node
            HtmlNode nodeList = document.DocumentNode.QuerySelector("div");

            switch (website)
            {
                case Constants.BHDSTAR:
                    // Node stores all infos which relate movie
                    nodeList = document.DocumentNode.QuerySelector("div.film--detail-content-top");

                    // Get node stores info about: Rated, Director, Actors, TypeOfMovie, PremiereDate, Duration, Language
                    detailMovieNodeArr = nodeList.QuerySelectorAll("ul.film--info > li").ToArray();

                    obj = GetMovieDetailFromBhd(nodeList, detailMovieNodeArr);
                    break;
                case Constants.KENH14:
                    // Get information about News
                    nodeList = document.DocumentNode.QuerySelector("div.klw-new-content");

                    if (nodeList != null)
                    {
                        obj = GetDetailFromKenh14(nodeList, parentNodeFromMainPage);
                    }
                    break;
            }

            return (T)obj;
        }

        private static Movie GetMovieDetailFromBhd(HtmlNode nodeList, HtmlNode[] detailMovieNodeArr)
        {
            Movie movie = new Movie()
            {
                ImageLink = nodeList != null ? nodeList.QuerySelector("img.movie-full").Attributes["src"].Value.Trim() : "",
                Name = nodeList != null ? nodeList.QuerySelector("div.product--name h3").InnerText.Trim() : "",
                TrailerLink = nodeList != null ? nodeList.QuerySelector("a.bhd-trailer").Attributes["href"].Value.Trim() : "",
                Content = nodeList != null ? nodeList.QuerySelector("div.film--detail").InnerText.Trim() : "",

                Rated = detailMovieNodeArr.Length != 0 ? detailMovieNodeArr[0].QuerySelector("span.col-right").InnerText.Trim() : "",
                Director = detailMovieNodeArr.Length != 0 ? detailMovieNodeArr[1].QuerySelector("span.col-right").InnerText.Trim() : "",
                Actors = detailMovieNodeArr.Length != 0 ? detailMovieNodeArr[2].QuerySelector("span.col-right").InnerText.Trim() : "",
                TypeOfMovie = detailMovieNodeArr.Length != 0 ? detailMovieNodeArr[3].QuerySelector("span.col-right").InnerText.Trim() : "",
                PremiereDate = detailMovieNodeArr.Length != 0 ? detailMovieNodeArr[4].QuerySelector("span.col-right").InnerText.Trim() : "",
                Duration = detailMovieNodeArr.Length != 0 ? detailMovieNodeArr[5].QuerySelector("span.col-right").InnerText.Trim() : "",
                Language = detailMovieNodeArr.Length != 0 ? detailMovieNodeArr[6].QuerySelector("span.col-right").InnerText.Trim() : ""
            };

            return movie;
        }

        private static News GetDetailFromKenh14(HtmlNode nodeList, HtmlNode parentNodeFromMainPage)
        {
            News news = new News()
            {
                // Get type of news and post date
                TypeOfNews = parentNodeFromMainPage.QuerySelector(".knswli-meta a").InnerText,
                PostDate = parentNodeFromMainPage.QuerySelector(".knswli-meta .knswli-time").Attributes["title"].Value,
                Title = nodeList != null ? nodeList.QuerySelector(".knc-sapo").InnerText.Trim() : ""
            };

            // Get all nodes which store all contents of article
            var contentList = nodeList.QuerySelectorAll(".knc-content > p").ToList();

            foreach (var item in contentList)
            {
                news.Content += System.Net.WebUtility.HtmlDecode(item.InnerText);
            }

            // Get all images of article
            var divNodeList = nodeList.QuerySelectorAll(".knc-content > div.VCSortableInPreviewMode").ToList();

            foreach (var item in divNodeList)
            {
                var imgNode = item.QuerySelector("img");

                if (imgNode != null)
                {
                    news.ImageUrl += imgNode.Attributes["src"].Value + "; ";
                }
            }

            return news;
        }
    }
}