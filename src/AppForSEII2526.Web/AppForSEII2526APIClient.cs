
internal class AppForSEII2526APIClient
{
    private string? uRI2API;
    private HttpClient httpClient;

    public AppForSEII2526APIClient(string? uRI2API, HttpClient httpClient)
    {
        this.uRI2API = uRI2API;
        this.httpClient = httpClient;
    }
}