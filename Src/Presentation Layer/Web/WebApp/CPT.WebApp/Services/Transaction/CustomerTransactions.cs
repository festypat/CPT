using CPT.Helper.Dto.Response.Transactions;
using CPT.WebApp.Configurations;
using CPT.WebApp.Dto.Response.TransactionHistory;
using CPT.WebApp.Dto.Response.TransactionSummary;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CPT.WebApp.Services.Transaction
{
    public class CustomerTransactions
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettingsConfig _appSettingsConfig;
        public CustomerTransactions(IOptions<AppSettingsConfig> appSettingsConfig)
        {
            _appSettingsConfig = appSettingsConfig.Value;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_appSettingsConfig.BaseUrl),
            };

            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
        }

        public async Task<TransactionSummaryBaseResponseDto> TransactionSummary(long customerId)
        {
            var response = new TransactionSummaryBaseResponseDto();
            try
            {
                var request = await _httpClient.GetAsync($"{_appSettingsConfig.TransactionSummaryUrl}{customerId}" );

                var content = await request.Content.ReadAsStringAsync();

                if (request.IsSuccessStatusCode)
                {
                    var data = JsonConvert.DeserializeObject<SummaryResponseDto>(content);

                    return new TransactionSummaryBaseResponseDto
                    {
                        data = new TransactionSummaryResponseDto
                        {
                            TotalTransaction = data.result.data.totalTransaction,
                            TotalAmount = data.result.data.totalAmount
                        }
                    };
                }

                return new TransactionSummaryBaseResponseDto
                {
                    data = new TransactionSummaryResponseDto
                    {
                        TotalTransaction = 0,
                        TotalAmount = 0
                    }
                };
            }
            catch (Exception ex)
            {
                return new TransactionSummaryBaseResponseDto
                {
                    data = new TransactionSummaryResponseDto
                    {
                        TotalTransaction = 0,
                        TotalAmount = 0
                    }
                };
            }
        }

        public async Task <List<Datum>> TransactionHistory(long customerId)
        {
            var response = new List<Datum>();
            try
            {
                var request = await _httpClient.GetAsync($"{_appSettingsConfig.TransactionHistoryUrl}{customerId}");

                var content = await request.Content.ReadAsStringAsync();

                if (request.IsSuccessStatusCode)
                {
                    var data = JsonConvert.DeserializeObject<HistoryResponseDto>(content);

                    response = data.result.data;

                    return response;
                }

                return new List<Datum>
                {
                    
                };
            }
            catch (Exception ex)
            {
                return new List<Datum>
                {
                    
                };
            }
        }

    }
}
