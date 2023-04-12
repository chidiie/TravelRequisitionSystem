using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using TravelRequisitionSystem.Models;
using TravelRequisitionSystem.Services.Interfaces;

namespace TravelRequisitionSystem.Services.Services
{
    public class CountryService : ICountryService
    {
        private readonly IConfiguration _configuration;

        public CountryService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<WeatherResponse> GetWeather(string location)
        {
            string responseCode = default;
            string accesstoken = _configuration["MailSettings:Authorization"];
            string weatherAPI = _configuration["ExternalAPI:WeatherAPI"];
            string weatherAPIKey = _configuration["ExternalAPI:WeatherKey"];
            weatherAPI = weatherAPI.Replace("{0}", location).Replace("{1}", weatherAPIKey);

            var weatherResponse = new WeatherResponse();
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);
                    ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                    //PhysicalAddress address = new PhysicalAddress();

                    //string serializedBody = JsonConvert.SerializeObject(model);

                    //StringContent content = new StringContent(serializedBody, UTF8Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.GetAsync(weatherAPI/*, content*/);
                    string res = response.Content.ReadAsStringAsync().Result;
                    //var deserializedResponse = JsonConvert.DeserializeObject<WeatherResponse>(res);
                    weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(res);

                }


            }
            catch (Exception)
            {

            }

            return weatherResponse;
        }

        public async Task<CountryResponse> GetCountryDetails(string location)
        {
            string responseCode = default;
            //string accesstoken = _configuration["MailSettings:Authorization"];
            var weather = await GetWeather(location);
            var countryNameFromLocation = weather.sys.country.ToString();
            var countryAPI = _configuration["ExternalAPI:CountryAPI"];
            countryAPI = countryAPI.Replace("{0}", countryNameFromLocation);

            var countryResponse = new CountryResponse();
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken);
                    ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                    //PhysicalAddress address = new PhysicalAddress();

                    //string serializedBody = JsonConvert.SerializeObject(model);

                    //StringContent content = new StringContent(serializedBody, UTF8Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.GetAsync(countryAPI/*, content*/);
                    string res = response.Content.ReadAsStringAsync().Result;
                    //var deserializedResponse = JsonConvert.DeserializeObject<WeatherResponse>(res);
                    countryResponse = JsonConvert.DeserializeObject<CountryResponse>(res);

                }


            }
            catch (Exception)
            {

            }

            return countryResponse;
        }


        public async Task<Response> SearchLocation(string location)
        {
            var response = new Response(false);
            try
            {
                var weatherResponse = await GetWeather(location);
                var countryResponse = await GetCountryDetails(location);
                var country = countryResponse.name.ToString();
                var currency = countryResponse.currencies.FirstOrDefault().name;
                var province = countryResponse.region.ToString();

                var weatherDetails = new WeatherDetails
                {
                    Humidity = weatherResponse.main.humidity.ToString(),
                    Pressure = weatherResponse.main.pressure.ToString(),
                    MinimumTemperature = weatherResponse.main.temp_min.ToString(),
                    MaximumTemperature = weatherResponse.main.temp_max.ToString(),
                };

                var countryDetails = new CountryDetails
                {
                    Country = country,
                    Province = province,
                    WeatherDetails = weatherDetails,
                    Currency = currency
                };

                response.Data = countryDetails;
                response.IsSuccessful = true;

                return response;
            }

            catch (Exception ex)
            {
                response.Error = new ErrorResponse
                {
                    ResponseCode = "99",
                    ResponseDescription = ex.Message,
                };
                response.IsSuccessful = false;

                return response;
            }
            

            

        }
    }
}
