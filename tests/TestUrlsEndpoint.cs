using System;
using Xunit;
using RA;

namespace tests
{
    public class TestUrlsEndpoint
    {
        public const string API = "http://localhost:5000/urls";

        [Fact]
        public void TestGetUrlsEndpoint()
        {
            new RestAssured()
              .Given()
                .Name("Test Urls List")
                .Header("Content-Type", "application/json")
              .When()
                .Get(API)
                .Then()
                .TestStatus("test status", status => status == 200 )
                .Assert("test status");
        }

        [Fact]
        public void TestPostUrlsEndpoint()
        {
        //Given
            new RestAssured()
              .Given()
                .Name("Test Post google.com")
                .Header("Content-Type", "application/json")
                .Body(new {LongUrl = "http://google.com"})
              .When()
                .Post(API)
              .Then()
                .TestStatus("test status", status => status == 201)
                .Assert("test status")
                .TestBody("test body", body => {return body.longAbsoluteUri == "http://google.com/";})
                .Assert("test body")  
                .TestBody("test body short url", body => {string t = body.shortUrl; return t.Length == 8;})
                .Assert("test body short url");

            new RestAssured()
              .Given()
                .Name("Test Post google.com")
                .Header("Content-Type", "application/json")
                .Body(new {LongUrl = "google.com"})
              .When()
                .Post(API)
              .Then()
                .TestStatus("test status", status => status == 201)
                .Assert("test status")
                .TestBody("test body", body => { return body.longAbsoluteUri == "http://google.com/";})
                .Assert("test body")  
                .TestBody("test body short url", body => {return ((string)body.shortUrl).Length == 8;})
                .Assert("test body short url");

            new RestAssured()
              .Given()
                .Name("Test Post google.com")
                .Header("Content-Type", "application/json")
                .Body(new {LongUrl = "http://"})
              .When()
                .Post(API)
              .Then()
                .TestStatus("test status", status => status == 400)
                .Assert("test status")
                .TestBody("test body", body => { return body.message == "Malformed URL.";})
                .Assert("test body");

            new RestAssured()
              .Given()
                .Name("Test Post google.com")
                .Header("Content-Type", "application/json")
                .Body(new {LongUrl = ""})
              .When()
                .Post(API)
              .Then()
                .TestStatus("test status", status => status == 400)
                .Assert("test status")
                .TestBody("test body", body => {return body.message == "Malformed URL.";})
                .Assert("test body");

            new RestAssured()
              .Given()
                .Name("Test Post google.com")
                .Header("Content-Type", "application/json")
                .Body(new {LongUrl = "#"})
              .When()
                .Post(API)
              .Then()
                .TestStatus("test status", status => status == 400)
                .Assert("test status")
                .TestBody("test body", body => {return body.message == "Malformed URL.";})
                .Assert("test body");

            new RestAssured()
              .Given()
                .Name("Test Post google.com")
                .Header("Content-Type", "application/json")
                .Body(new {LongUrl = "khubayeAlam.ایران"})
              .When()
                .Post(API)
              .Then()
                .TestStatus("test status", status => status == 201)
                .Assert("test status")
                .TestBody("test body", body => { return body.longAbsoluteUri == "http://khubayealam.xn--mgba3a4f16a/";})
                .Assert("test body");
                
                
            new RestAssured()
              .Given()
                .Name("Test Post google.com")
                .Header("Content-Type", "application/json")
                .Body(new {LongUrl = "http://علی@asghar.ایران:8080/naghi?q=حسین#محمد"})
              .When()
                .Post(API)
              .Then()
                .TestStatus("test status", status => status == 201)
                .Assert("test status")
                .TestBody("test body", body => {return body.longAbsoluteUri == "http://%D8%B9%D9%84%DB%8C@asghar.xn--mgba3a4f16a:8080/naghi?q=%D8%AD%D8%B3%DB%8C%D9%86#%D9%85%D8%AD%D9%85%D8%AF";})
                .Assert("test body");

            new RestAssured()
              .Given()
                .Name("Test Post google.com")
                .Header("Content-Type", "application/json")
                .Body(new {LongUrl = "192"})
              .When()
                .Post(API)
              .Then()
                .TestStatus("test status", status => status == 400)
                .Assert("test status");  
                
            new RestAssured()
              .Given()
                .Name("Test Post google.com")
                .Header("Content-Type", "application/json")
                .Body(new {LongUrl = "https://www.google.com/search?sxsrf=ACYBGNQ6sPtlwOqgken-pxNGGvrOH2NRVA:1581119305668&q=Hamid+ +Mohammadi&stick=H4sIAAAAAAAAAOPgE-LVT9c3NEyqrEoxKDNOVoJyzdIL8syqyrTUs5Ot9JNKizPzUouL4Yz4_ILUosSSzPw8q7T80ryU1KJFrPweibmZKQq--RmJubmJKZk7WBkBal9o3F4AAAA&sa=X&ved=2ahUKEwjqhr-00MDnAhXbwsQBHZixCy4QmxMoATAgegQICxAV&sxsrf=ACYBGNQ6sPtlwOqgken-pxNGGvrOH2NRVA:1581119305668&biw=1853&bih=981F-8"})
              .When()
                .Post(API)
              .Then()
                .TestStatus("test status", status => status == 201)
                .Assert("test status");  
                             
        }
    }
}
