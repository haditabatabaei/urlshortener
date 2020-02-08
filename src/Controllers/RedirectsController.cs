using Microsoft.AspNetCore.Mvc;
using urlShortener.Models;
using urlShortener.Services;
using urlShortener.Utils;
using System;

namespace urlShortener.Controllers {
    [ApiController]
    [Route ("redirect")]
    public class RedirectsController : ControllerBase {
        private readonly IUrlShortenerService urlShortenerService;

        private UrlUtils urlUtils;

        public RedirectsController (IUrlShortenerService urlShortenerService) {
            this.urlShortenerService = urlShortenerService;
            urlUtils = new UrlUtils ();
        }

        [HttpGet]
        [Route ("{shortUrl}")]
        public IActionResult ShortUrlRedirect (string shortUrl) {
            try {
                if (urlUtils.stringIsInShortenFormat (shortUrl, 8)) {
                    Url urlToReturn = urlShortenerService.GetLongUrlByShort (shortUrl);
                    if (urlToReturn == null) {
                        return NotFound (new Error ("Input URL is not available.", "آدرس ورودی در سیستم موجود نمی باشد."));
                    } else {
                        return Redirect (urlToReturn.LongAbsoluteUri);
                    }
                } else {
                    return BadRequest (new Error ("Short string is malformed.", "فرمت آدرس کوتاه نا معتبر است."));
                }
            } catch (Exception) {
                return BadRequest (new Error ("Short string is malformed.", "فرمت آدرس کوتاه نا معتبر است."));
            }

        }
    }
}