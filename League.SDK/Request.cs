using System.Net.Http.Headers;
using System.Text;

namespace League.SDK;

public static class Request
{
  private static string RSO_ENDPOINT = "https://127.0.0.1";
  private static string STORE_ENDPOINT = "https://br.store.leagueoflegends.com";
  private static HttpClientHandler httpClient;

  public static async Task<string> GetLocal(string Endpoint, string Token, string Port)
  {
    var auth = Convert.ToBase64String(Encoding.ASCII.GetBytes($"riot:{Token}"));

    httpClient = new HttpClientHandler();

    httpClient.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
    { return true; };

    var httpclient = new HttpClient(httpClient);

    httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    httpclient.BaseAddress = new Uri(RSO_ENDPOINT + ":" + Port + "/");
    httpclient.DefaultRequestHeaders.Add("Authorization", "Basic " + auth);

    var response = await httpclient.GetStringAsync(Endpoint);

    return response;
  }

  public static async Task<string> PostLocal(string PostData, string Endpoint, string Token, string Port)
  {
    var auth = Convert.ToBase64String(Encoding.ASCII.GetBytes($"riot:{Token}"));

    httpClient = new HttpClientHandler();

    httpClient.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
    { return true; };

    var httpclient = new HttpClient(httpClient);

    httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    httpclient.BaseAddress = new Uri(RSO_ENDPOINT + ":" + Port + "/");
    httpclient.DefaultRequestHeaders.Add("Authorization", "Basic " + auth);

    var content = new StringContent(PostData, Encoding.UTF8, "application/json");
    var response = await httpclient.PostAsync(Endpoint, content);

    return response.ToString();
  }

  public static async Task<string> PatchLocal(string PatchData, string Endpoint, string Token, string Port)
  {
    var auth = Convert.ToBase64String(Encoding.ASCII.GetBytes($"riot:{Token}"));

    httpClient = new HttpClientHandler();

    httpClient.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
    { return true; };

    var httpclient = new HttpClient(httpClient);

    httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    httpclient.BaseAddress = new Uri(RSO_ENDPOINT + ":" + Port + "/");
    httpclient.DefaultRequestHeaders.Add("Authorization", "Basic " + auth);

    var content = new StringContent(PatchData, Encoding.UTF8, "application/json");
    var response = await httpclient.PatchAsync(Endpoint, content);

    return response.ToString();
  }

  public static async Task<string> GetStore(string Endpoint, string Token)
  {
    httpClient = new HttpClientHandler();

    var httpclient = new HttpClient(httpClient);

    httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    httpclient.BaseAddress = new Uri(STORE_ENDPOINT);
    httpclient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
    httpclient.DefaultRequestHeaders.Add("User-Agent", "LeagueOfLegendsClient/12.8.437.1743 (CEF 91)");
    httpclient.DefaultRequestHeaders.Add("Referer", "https://br.store.leagueoflegends.com/");

    var response = await httpclient.GetStringAsync(Endpoint);

    return response;
  }


  public static async Task<string> PostStore(string PostData, string Endpoint, string Token)
  {
    httpClient = new HttpClientHandler();

    var httpclient = new HttpClient(httpClient);

    httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    httpclient.BaseAddress = new Uri(STORE_ENDPOINT);
    httpclient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
    httpclient.DefaultRequestHeaders.Add("User-Agent", "LeagueOfLegendsClient/12.8.437.1743 (CEF 91)");
    httpclient.DefaultRequestHeaders.Add("Referer", "https://br.store.leagueoflegends.com/");

    var Content = new StringContent(
        PostData,
        Encoding.UTF8,
        "application/json");

    var Response = await httpclient.PostAsync(Endpoint, Content);

    return Response.ToString();
  }
}
