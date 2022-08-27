using CPT.Helper.Dto.Request.Account;
using CPT.Helper.Dto.Response.Account;
using CPT.WebApp.Configurations;
using CPT.WebApp.Dto.Response;
using CPT.WebApp.ViewModel;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CPT.WebApp.Services.Account
{
    public class LoginService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettingsConfig _accountConfig;
        public LoginService(IOptions<AppSettingsConfig> accountConfig)
        {
            _accountConfig = accountConfig.Value;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_accountConfig.BaseUrl),
            };

            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
        }

        public async Task<UserProfile> AuthenticateUser(LoginRequestDto login)
        {
            try
            {
                var request = await _httpClient.PostAsync(_accountConfig.LoginUrl, new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json"));

                var content = await request.Content.ReadAsStringAsync();

                if (request.IsSuccessStatusCode)
                {
                    var data = JsonConvert.DeserializeObject<UserInfoResponseDto>(content);

                    return new UserProfile
                    {
                        Response = "00",
                        Username = login.Username,
                        CustomerId = data.result.customerId
                    };
                }
                   

                return new UserProfile
                {
                    Response = "01"
                };
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
