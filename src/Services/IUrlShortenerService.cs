using urlShortener.Models;
using System.Collections.Generic;

namespace urlShortener.Services
{
    public interface IUrlShortenerService
    {
        Url GetLongUrlByShort(string shortUrl);

        IEnumerable<Url> GetAllUrls();

        bool SaveUrl(Url url);

        bool hasDuplicateShortUrl(string shortUrl);
    }
}