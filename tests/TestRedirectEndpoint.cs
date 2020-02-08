using System;
using Xunit;
using RA;

namespace tests
{
    public class TestRedirectEndpoint
    {
        public const string API = "http://localhost:5000/redirect";

        [Fact]
        public void TestGetRedirectEndpoint()
        {
            new RestAssured()
              .Given()
                .Name("Test Redirect Empty")
                .Header("Content-Type", "application/json")
              .When()
                .Get(API)
              .Then()
                .TestStatus("test status", status => status == 404 )
                .Assert("test status");

            new RestAssured()
              .Given()
                .Name("Test Redirect Empty slash")
                .Header("Content-Type", "application/json")
              .When()
                .Get(API + "/")
              .Then()
                .TestStatus("test status", status => status == 404 )
                .Assert("test status");

            new RestAssured()
              .Given()
                .Name("Test Redirect Empty slash sharp")
                .Header("Content-Type", "application/json")
              .When()
                .Get(API + "/#")
              .Then()
                .TestStatus("test status", status => status == 404 )
                .Assert("test status");
            
            new RestAssured()
              .Given()
                .Name("Test Redirect Empty slash sharp sharp")
                .Header("Content-Type", "application/json")
              .When()
                .Get(API + "/!##")
              .Then()
                .TestStatus("test status", status => status == 400 )
                .Assert("test status")
                .TestBody("test body", body => body.message == "Short string is malformed.")
                .Assert("test body");
    
            new RestAssured()
              .Given()
                .Name("Test Redirect comples route")
                .Header("Content-Type", "application/json")
              .When()
                .Get(API + "/ali/mohammad")
              .Then()
                .TestStatus("test status", status => status == 404 )
                .Assert("test status");

            new RestAssured()
              .Given()
                .Name("Test Redirect malformed long")
                .Header("Content-Type", "application/json")
              .When()
                .Get(API + "/alibaba1234")
              .Then()
                .TestStatus("test status", status => status == 400 )
                .Assert("test status")
                .TestBody("test body", body => body.message == "Short string is malformed.")
                .Assert("test body");
            
            new RestAssured()
              .Given()
                .Name("Test Redirect malformed space")
                .Header("Content-Type", "application/json")
              .When()
                .Get(API + "/ab c d")
              .Then()
                .TestStatus("test status", status => status == 400 )
                .Assert("test status")
                .TestBody("test body", body => body.message == "Short string is malformed.")
                .Assert("test body");

            new RestAssured()
              .Given()
                .Name("Test Redirect not found")
                .Header("Content-Type", "application/json")
              .When()
                .Get(API + "/abcdefgh")
              .Then()
                .TestStatus("test status", status => status == 404 )
                .Assert("test status")
                .TestBody("test body", body => body.message == "Input URL is not available.")
                .Assert("test body");
            
            new RestAssured()
              .Given()
                .Name("Test Redirect Good")
                .Header("Content-Type", "application/json")
              .When()
                .Get(API + "/DlQVEwZO")
              .Then()
                .TestStatus("test status", status => status == 302 )
                .Assert("test status")
                .TestHeader("test loc header","Location" , value => {Console.WriteLine(value); return value == "http://%D8%B9%D9%84%DB%8C@asghar.xn--mgba3a4f16a:8080/naghi?q=%D8%AD%D8%B3%DB%8C%D9%86#%D9%85%D8%AD%D9%85%D8%AF";})
                .Assert("test loc header");

            new RestAssured()
              .Given()
                .Name("Test Redirect Too Much Slash")
              .When()
                .Get(API + "///DlQVEwZO")
              .Then()
                .TestStatus("test status", status => status == 404 )
                .Assert("test status");

            new RestAssured()
              .Given()
                .Name("Test Redirect Good With Sharp")
              .When()
                .Get(API + "/DlQVEwZO#O")
              .Then()
                .TestStatus("test status", status => status == 302 )
                .Assert("test status")
                .TestHeader("test loc header","Location", value => value == "http://%D8%B9%D9%84%DB%8C@asghar.xn--mgba3a4f16a:8080/naghi?q=%D8%AD%D8%B3%DB%8C%D9%86#%D9%85%D8%AD%D9%85%D8%AF")
                .Assert("test loc header");

            new RestAssured()
              .Given()
                .Name("Test Redirect Good long")
              .When()
                .Get(API + "/PrUsFCVM")
              .Then()
                .TestStatus("test status", status => status == 302 )
                .Assert("test status")
                .TestHeader("test loc header","Location", value => value == "https://www.google.com/search?sxsrf=ACYBGNQ6sPtlwOqgken-pxNGGvrOH2NRVA:1581119305668&q=Hamid+%20+%20Mohammadi&stick=H4sIAAAAAAAAAOPgE-LVT9c3NEyqrEoxKDNOVoJyzdIL8syqyrTUs5Ot9JNKizPzUouL4Yz4_ILUosSSzPw8q7T80ryU1KJFrPweibmZKQq--RmJubmJKZk7WBkBal9o3F4AAAA&sa=X&ved=2ahUKEwjqhr-00MDnAhXbwsQBHZixCy4QmxMoATAgegQICxAV&sxsrf=ACYBGNQ6sPtlwOqgken-pxNGGvrOH2NRVA:1581119305668&biw=1853&bih=981F-8")
                .Assert("test loc header");
        }
    }
}
