using System;
using System.Text;
using urlShortener.Models;

namespace urlShortener.Utils
{
    public class UrlUtils
    {
        private string[] SCHEMA_PREFIXES = new string[]{"http://", "https://", "ftp://" , "gopher://", "mailto://", "net.pipe://", "net.pcp://", "news://", "nntp://"};
        private string BASE_STRING = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private Random randomGenerator;
        private StringBuilder stringBuilder;

        public UrlUtils() {
            randomGenerator = new Random();
        }

        public string generateRandomStringWithLength(int length) {

            stringBuilder = new StringBuilder ("", length);

            for (int i = 0; i < length; i++) {
                stringBuilder.Append (BASE_STRING[randomGenerator.Next (BASE_STRING.Length)]);
            }

            return stringBuilder.ToString ();
        }

        public bool hasSchemaPrefix(string longUrl) {   
            for(int i = 0; i < SCHEMA_PREFIXES.Length ; i++) {
                if(longUrl.StartsWith(SCHEMA_PREFIXES[i])) {
                    return true;
                }
            }
            return false;
        }   

        public bool isValidUrl(Url url) {
            //if url has no protocol schema, try adding http:// as default
            if (!hasSchemaPrefix (url.LongUrl)) {
                url.LongUrl = "http://" + url.LongUrl;
            }

            try {
                //try creating a uri to check its basic validation
                Uri inputLongUri = new Uri (url.LongUrl);

                if (inputLongUri.Host.Split (".").Length == 1) {
                    //there is no .com .ir etc present in host string so its invalid
                    return false;
                }

                if (inputLongUri.Host.Split(".").Length == 4 && url.LongUrl.Split(".").Length != 4) {
                    //create uri instance falsy generates some ip, this is to prevent that, but thats a stupid descition i made back day
                    return false;
                }

                //url is valid, good to go
                return true;

            } catch (Exception) {
                //there was some exception in creating uri so it was bad uri schema
                return false;
            }
        }

        public bool stringIsInShortenFormat(string inputStrToCheck, int length) {
            if(inputStrToCheck.Length == length) {
                for(int i = 0; i < inputStrToCheck.Length ; i++) {
                    if(!BASE_STRING.Contains(inputStrToCheck[i])) {
                        return false;
                    }
                }
                return true;
            } else {
                return false;
            }
        }
    }
}