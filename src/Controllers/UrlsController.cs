using System;
using Microsoft.AspNetCore.Mvc;
using urlShortener.Models;
using urlShortener.Services;
using urlShortener.Utils;

namespace urlShortener.Controllers {

    [ApiController]
    [Route ("urls")]
    public class UrlsController : ControllerBase {
        private UrlUtils urlUtils;
        private readonly IUrlShortenerService urlShortenerService;

        public UrlsController (IUrlShortenerService urlShortenerService) {
            this.urlShortenerService = urlShortenerService;
            urlUtils = new UrlUtils ();
        }

        [HttpPost]
        public IActionResult PostLongUrl ([FromBody] Url inputUrl) {
            //if url is valid, then proceed to create short url and the create in the databasee
            try {
                if (urlUtils.isValidUrl (inputUrl)) {
                    Uri uri = new Uri (inputUrl.LongUrl);
                    while (true) {
                        string shortUrl = urlUtils.generateRandomStringWithLength (8);

                        //check if there is no duplication in database
                        if (!urlShortenerService.hasDuplicateShortUrl (shortUrl)) {
                            inputUrl.LongAbsoluteUri = uri.AbsoluteUri.Replace (uri.Host, uri.IdnHost);
                            inputUrl.ShortUrl = shortUrl;
                            if (urlShortenerService.SaveUrl (inputUrl)) {
                                return Created ("", inputUrl);
                            } else {
                                //there was some conflict adding url obj too database
                                return Conflict (new Error ("There was error adding url to database.", "خطایی هنگان اضافه کردن به دیتابیس رخ داد."));
                            }
                        }
                    }
                } else {
                    //url is not valid, send 400 error malformed url
                    return BadRequest (new Error ("Malformed URL.", "فرمت آدرس نا معتبر است"));
                }
            } catch(Exception) {
                return BadRequest (new Error ("Malformed URL.", "فرمت آدرس نا معتبر است"));
            }

        }

        [HttpGet]
        public IActionResult GetAll () {
            return Ok (urlShortenerService.GetAllUrls ());
        }
    }
}