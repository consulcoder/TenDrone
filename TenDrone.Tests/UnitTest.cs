using Microsoft.AspNetCore.Http;
using static System.Net.HttpStatusCode;

namespace TenDrone.Tests;

public class UnitTest
{
    public readonly static string URL = "http://localhost:5172/";
    [Fact]
    public async void TestRoutes()
    {
        HttpClient http = new HttpClient();
        var response = await http.GetAsync($"{URL}swagger/index.html");
        Assert.True((int)response.StatusCode == StatusCodes.Status200OK,"");
    }
}