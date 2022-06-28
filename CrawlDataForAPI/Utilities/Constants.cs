using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrawlDataForAPI.Utilities
{
    public static class Constants
    {
        public const string BHDSTAR = "BHDSTAR";
        public const string CGV = "CGV";
        public const string KENH14 = "KENH14";
    }

    public static class WebsiteLink
    {
        public const string BHDSTAR = "https://www.bhdstar.vn/";
        public const string KENH14 = "https://kenh14.vn/";
    }

    public static class ExtensionOfFile
    {
        public const string TXT = ".txt";
        public const string CSV = ".csv";
        public const string PDF = ".pdf";
        public const string XLSX = ".xlsx";
    }

    public static class FieldType
    {
        public const string MOVIE = "Movie";
        public const string NEWS = "News";
    }
}