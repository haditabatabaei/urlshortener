using System.Collections.Generic;
using urlShortener.Models;
using System;

namespace urlShortener.Services {
    public class UrlShortenerService : IUrlShortenerService {
        private readonly UrlShortenerDbContext dbContext;

        public UrlShortenerService (UrlShortenerDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public Url GetLongUrlByShort (string shortUrl) {
            return dbContext.urls.Find (shortUrl);
        }

        public IEnumerable<Url> GetAllUrls () {
            return dbContext.urls;
        }

        public bool SaveUrl (Url url) {
            try {
                dbContext.urls.Add (url);
                dbContext.SaveChanges ();
                return true;
            } catch (Exception) {
                return false;
            }
        }

        public bool hasDuplicateShortUrl (string shortUrl) {
            Url foundUrl = GetLongUrlByShort (shortUrl);
            if (foundUrl == null) {
                return false;
            } else {
                return true;
            }
        }
    }
}