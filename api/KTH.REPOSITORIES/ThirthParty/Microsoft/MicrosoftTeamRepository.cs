using Microsoft.Graph.Models;
using Microsoft.Graph.Models.ODataErrors;
using Microsoft.Graph;
using Azure.Identity;
using KTH.MODELS;
using Microsoft.Extensions.Options;
using System.Net.Http;
using Microsoft.Identity.Client;
using System.Net.Http.Headers;
using System.Text;

namespace KTH.REPOSITORIES.ThirthParty.MicrosoftGraph
{
    public interface IMicrosoftTeamRepository
    {
        public Task ChatGroupCritical();
    }

    public class MicrosoftTeamRepository : IMicrosoftTeamRepository
    {
        private readonly ISysConfigurationRepository _sysConfigurationRepository;

        public MicrosoftTeamRepository(ISysConfigurationRepository sysConfigurationRepository)
        {
            _sysConfigurationRepository = sysConfigurationRepository;
        }

        public async Task ChatGroupCritical()
        {
            try
            {
                var scopes = new[] { "Chat.ReadWrite" };

                FilterModel filterModel = new FilterModel();

                var groupValue = await _sysConfigurationRepository.GetByGroup("Microsoft", filterModel);
                var clientId = groupValue.FirstOrDefault(x => x.Key == "TeamClientId")?.Value;
                var tenantId = groupValue.FirstOrDefault(x => x.Key == "TeamTenantId")?.Value;
                var clientSecret = groupValue.FirstOrDefault(x => x.Key == "TeamSecretId")?.Value;
                var chatId = groupValue.FirstOrDefault(x => x.Key == "TeamIncidentCritical")?.Value;

                var userName = groupValue.FirstOrDefault(x => x.Key == "TeamAccount")?.Value;
                var password = groupValue.FirstOrDefault(x => x.Key == "TeamPassword")?.Value;


                var confidentialClientApp = ConfidentialClientApplicationBuilder
                .Create(clientId)
                .WithClientSecret(clientSecret)
                .WithAuthority(new Uri($"https://login.microsoftonline.com/{tenantId}"))
                .Build();

                var result = await confidentialClientApp.AcquireTokenForClient(new[] { "https://graph.microsoft.com/.default" }).ExecuteAsync();

                var accessToken = result.AccessToken;


                using (var httpClient = new HttpClient())
                {
                    var requestUrl = $"https://graph.microsoft.com/v1.0/chats/{chatId}/messages";
                    var requestBody = new
                    {
                        body = new
                        {
                            contentType = "html",
                            content = "<b style='color:red'> [CRITICAL] </b> </br> klongtun Hospital System </br> </br> "
                        }
                    };

                    var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    var response = await httpClient.PostAsync(requestUrl, content);

                    var responseContent = await response.Content.ReadAsStringAsync();

                    response.EnsureSuccessStatusCode();
                    Console.WriteLine("Message sent successfully: " + responseContent);
                }
            }
            catch (Exception ex)
            {
                // Handle and log detailed error information
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Status Code: {ex.StackTrace}");
                Console.WriteLine($"Request ID: {ex.InnerException}");
            }
        }


    }
}

