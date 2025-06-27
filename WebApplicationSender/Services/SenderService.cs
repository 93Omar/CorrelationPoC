namespace WebApplicationSender.Services
{
    public class SenderService
    {
        private readonly HttpClient _httpClient;

        public SenderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task SendAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("Receiver");
            response.EnsureSuccessStatusCode();
        }
    }
}
