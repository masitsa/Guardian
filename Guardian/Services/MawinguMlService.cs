using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Guardian.Services
{
    public static class MawinguMlService
    {
        public static async Task<double> GetUsage(Statistics statistics)
        {
            var handler = new HttpClientHandler()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback =
                            (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
            using (var client = new HttpClient(handler))
            {
                var message = string.Empty;

                var requestBody =
                    @$"{{
                    ""input_data"": {{
                        ""columns"": [
                          ""Cust No"",
                          ""Status"",
                          ""churned"",
                          ""date_installed"",
                          ""disconnection_date"",
                          ""Days Since Disconnection"",
                          ""Months Stayed"",
                          ""number_of_complaints"",
                          ""number_of_payments"",
                          ""Missed Payments"",
                          ""day"",
                          ""mnth"",
                          ""year""
                        ],
                        ""index"": [0],
                        ""data"": [[""{statistics.CustomerNumber}"",""{statistics.Status}"",{statistics.Churned},""{statistics.DateInstalled}"",""{statistics.DisconnectionDate}"",{statistics.DisconnectionDays},{statistics.MonthsStayed},{statistics.NumberOfComplaints},{statistics.NumberOfPayments},{statistics.MissedPayments},{statistics.Day},{statistics.Month},{statistics.Year}]]
                      }}
                    }}";

                const string apiKey = "";
                if (string.IsNullOrEmpty(apiKey))
                {
                    throw new Exception("A key should be provided to invoke the endpoint");
                }
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                client.BaseAddress = new Uri("https://mawinguml-data-usage.eastus.inference.ml.azure.com/score");

                var content = new StringContent(requestBody);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await client.PostAsync("", content);

                if (response.IsSuccessStatusCode)
                {
                    string resultString = await response.Content.ReadAsStringAsync();

                    string cleaned = resultString.Trim('[', ']');

                    if (double.TryParse(cleaned, out double result))
                    {
                        return result;
                    }
                    else
                    {
                        throw new Exception("Conversion failed");
                    }
                }
                else
                {

                    string errorMessage = await response.Content.ReadAsStringAsync();
                    message = $"Error: {response.StatusCode} - {errorMessage}";
                    throw new Exception(message);
                }
            }
        }

        public static async Task<double> GetProbability(Probability probability)
        {
            var handler = new HttpClientHandler()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback =
                            (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
            using (var client = new HttpClient(handler))
            {
                var message = string.Empty;

                var requestBody =
                    @$"{{
                        ""input_data"": {{
                            ""columns"": [
                                  ""CustNo"",
                                  ""Status"",
                                  ""DaysSinceDisconnection"",
                                  ""MonthsStayed"",
                                  ""NumberOfPayments"",
                                  ""MissedPayments"",
                                  ""NumberOfComplaintsPerMonth""
                            ],
                            ""index"": [0],
                            ""data"": [[""{probability.CustomerNumber}"",""{probability.Status}"",{probability.DaysSinceDisconnection},{probability.MonthsStayed},{probability.NumberOfPayments},{probability.MissedPayments},{probability.NumberOfComplaintsPerMonth}]]
                          }}
                        }}";

                const string apiKey = "";
                if (string.IsNullOrEmpty(apiKey))
                {
                    throw new Exception("A key should be provided to invoke the endpoint");
                }
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                client.BaseAddress = new Uri("https://mawinguml-churn-probability.eastus.inference.ml.azure.com/score");

                var content = new StringContent(requestBody);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await client.PostAsync("", content);

                if (response.IsSuccessStatusCode)
                {
                    string resultString = await response.Content.ReadAsStringAsync();

                    string cleaned = resultString.Trim('[', ']');

                    if (double.TryParse(cleaned, out double result))
                    {
                        return result;
                    }
                    else
                    {
                        throw new Exception("Conversion failed");
                    }
                }
                else
                {

                    string errorMessage = await response.Content.ReadAsStringAsync();
                    message = $"Error: {response.StatusCode} - {errorMessage}";
                    throw new Exception(message);
                }
            }
        }
    }
}
