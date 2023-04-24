using System.Text;
using Newtonsoft.Json.Linq;

namespace simple_api_core.tests;

public class BooksTests
{
    const string ENDPOINT = "https://localhost:7010/Books";
    static readonly HttpClient client = new HttpClient();

    [Fact]
    public void Test_Take_All_Books()
    {
        HttpResponseMessage response = client.GetAsync(ENDPOINT).Result;
        Assert.True(response.IsSuccessStatusCode);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Test_Calc_Shipping(int value)
    {
        HttpResponseMessage response = client.GetAsync(ENDPOINT + "/" + value + "/shipping").Result;
        Assert.True(response.IsSuccessStatusCode);

        string responseBody = response.Content.ReadAsStringAsync().Result;

        JObject json = JObject.Parse(responseBody);

        var total_price = (double)json["total_price"];
        var price = (double)json["price"];
        var book_price = (double)json["book_price"];

        Assert.Equal(total_price, (price + book_price));

        double newPrice = 0.2 * book_price;

        Assert.Equal(newPrice, price);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Test_Take_Book_by_Id(int value)
    {
        HttpResponseMessage response = client.GetAsync(ENDPOINT + "/" + value).Result;
        Assert.True(response.IsSuccessStatusCode);

        string responseBody = response.Content.ReadAsStringAsync().Result;

        JObject json = JObject.Parse(responseBody);

        Assert.Equal(value, (int)json["id"]);
    }

    [Theory]
    [InlineData("Jules Verne")]
    [InlineData("J. R. R. Tolkien")]
    [InlineData("J. K. Rowling")]
    public void Test_Take_All_Books_From_Author(string value)
    {  
        var filter = new
        {
           author = value
        };

        var dataJson = Newtonsoft.Json.JsonConvert.SerializeObject(filter);
        var data = new System.Net.Http.StringContent(dataJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = client.PostAsync(ENDPOINT, data).Result;
        Assert.True(response.IsSuccessStatusCode);

        string responseBody = response.Content.ReadAsStringAsync().Result;

        JObject json = (JObject)JArray.Parse(responseBody).First;

        var author = (string)json["specifications"]["author"];

        Assert.Equal(value, author);
    }

    [Theory]
    [InlineData("Adventure Fiction")]
    [InlineData("Science Fiction")]
    [InlineData("Drama")]
    public void Test_Take_All_Books_From_Genres(string value)
    {
        var filter = new
        {
           genres = value
        };

        var dataJson = Newtonsoft.Json.JsonConvert.SerializeObject(filter);
        var data = new System.Net.Http.StringContent(dataJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = client.PostAsync(ENDPOINT, data).Result;
        Assert.True(response.IsSuccessStatusCode);

        string responseBody = response.Content.ReadAsStringAsync().Result;

        JObject json = (JObject)JArray.Parse(responseBody).First;

        var genres = (JArray)json["specifications"]["genres"];

        Assert.Contains(value, genres);
    }

    [Theory]
    [InlineData("The Lord of the Rings")]
    [InlineData("Journey to the Center of the Earth")]
    [InlineData("Harry Potter and the Goblet of Fire")]
    public void Test_Take_All_Books_From_Name(string value)
    {
        var filter = new
        {
           name = value
        };

        var dataJson = Newtonsoft.Json.JsonConvert.SerializeObject(filter);
        var data = new System.Net.Http.StringContent(dataJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = client.PostAsync(ENDPOINT, data).Result;
        Assert.True(response.IsSuccessStatusCode);

        string responseBody = response.Content.ReadAsStringAsync().Result;

        JObject json = (JObject)JArray.Parse(responseBody).First;

        var name = (string)json["name"];

        Assert.Equal(value, name);
    }
}