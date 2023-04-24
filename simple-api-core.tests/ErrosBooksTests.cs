using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace simple_api_core.tests;

public class ErrosBooksTests
{
    const string ENDPOINT = "https://localhost:7010/Books";
    static readonly HttpClient client = new HttpClient();

    [Fact]
    public void Test_Take_Error_Book_Shipping()
    {
        HttpResponseMessage response = client.GetAsync(ENDPOINT + "/7/shipping").Result;
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        string responseBody = response.Content.ReadAsStringAsync().Result;

        Assert.Equal("Nada foi localizado esse ID", responseBody);
    }

    [Fact]
    public void Test_Take_Error_Book_by_Id()
    {
        HttpResponseMessage response = client.GetAsync(ENDPOINT + "/7").Result;
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        string responseBody = response.Content.ReadAsStringAsync().Result;

        Assert.Equal("Nada foi localizado esse ID", responseBody);
    }

    [Fact]
    public void Test_Take_Erro_Book_From_Author()
    {
        var filter = new
        {
            author = "Error"
        };

        var dataJson = Newtonsoft.Json.JsonConvert.SerializeObject(filter);
        var data = new System.Net.Http.StringContent(dataJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = client.PostAsync(ENDPOINT, data).Result;
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        string responseBody = response.Content.ReadAsStringAsync().Result;

        Assert.Equal("Nada foi localizado com esses parametros", responseBody);
    }
}