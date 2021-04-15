using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MetaWeatherAutomation
{
    public class MetaWeatherTests
    {
        [TestCase("min", "Minsk")]
        public async Task SearchPatternTest(string searchPattern, string expectedLocation)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"https://www.metaweather.com/api/location/search/?query={searchPattern}"))
                {
                    var response = await httpClient.SendAsync(request);
                    using (HttpContent content = response.Content)
                    {
                        var jsonString = await content.ReadAsStringAsync();
                        List<WeatherReport> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<WeatherReport>>(jsonString);

                        Assert.True(data.Exists(x => x.title == expectedLocation));
                    }
                }
            }
        }

        [TestCase(53.893009, 27.567444, "Minsk")]
        [TestCase(51.481583, -3.179090, "Cardiff")]
        public async Task LattLongMatchTest(double lattitude, double longitude, string expectedLocation)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"https://www.metaweather.com/api/location/search/?lattlong={lattitude},{longitude}"))
                {
                    var response = await httpClient.SendAsync(request);
                    using (HttpContent content = response.Content)
                    {
                        var jsonString = await content.ReadAsStringAsync();
                        List<WeatherReport> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<WeatherReport>>(jsonString);

                        Assert.AreEqual(data[0].title, expectedLocation);
                    }
                }
            }
        }

        [Test]
        public async Task ActualMinskWeatherReportTest()
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://www.metaweather.com/api/location/834463/"))
                {
                    var response = await httpClient.SendAsync(request);
                    using (HttpContent content = response.Content)
                    {
                        var jsonString = await content.ReadAsStringAsync();
                        WeatherReport data = Newtonsoft.Json.JsonConvert.DeserializeObject<WeatherReport>(jsonString);

                        Assert.True(data.title == "Minsk");
                    }
                }
            }
        }

        [TestCase(44418, 1, 20)]
        public async Task TemperatureTest(int woeid, double minTemperature, double maxTemperature)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"https://www.metaweather.com/api/location/{woeid}/"))
                {
                    var response = await httpClient.SendAsync(request);
                    using (HttpContent content = response.Content)
                    {
                        var jsonString = await content.ReadAsStringAsync();
                        WeatherReport data = Newtonsoft.Json.JsonConvert.DeserializeObject<WeatherReport>(jsonString);

                        foreach(ConsolidatedWeather weather in data.consolidated_weather)
                        {
                            Assert.True(minTemperature < weather.the_temp && weather.the_temp < maxTemperature);
                        }
                    }
                }
            }
        }

        [TestCase(44418)]
        public async Task FiveYearsagoTest(int woeid)
        {
            using (var httpClient = new HttpClient())
            {
                using (var fiveYearsRequest = new HttpRequestMessage(new HttpMethod("GET"), $"https://www.metaweather.com/api/location/{woeid}/{DateTime.Today.AddYears(-5).ToString("yyyy/M/d")}/"))
                {
                    var fiveYearsResponse = await httpClient.SendAsync(fiveYearsRequest);
                    using (HttpContent fiveYearsContent = fiveYearsResponse.Content)
                    {
                        var fiveYearsJsonString = await fiveYearsContent.ReadAsStringAsync();
                        List<ConsolidatedWeather> fiveYearsData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ConsolidatedWeather>>(fiveYearsJsonString);

                        using (var todayRequest = new HttpRequestMessage(new HttpMethod("GET"), $"https://www.metaweather.com/api/location/{woeid}/{DateTime.Today.ToString("yyyy/M/d")}/"))
                        {
                            var todayResponse = await httpClient.SendAsync(todayRequest);
                            using (HttpContent todayContent = todayResponse.Content)
                            {
                                var todayJsonString = await todayContent.ReadAsStringAsync();
                                List<ConsolidatedWeather> todayData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ConsolidatedWeather>>(todayJsonString);

                                Assert.True(fiveYearsData.Exists(x => todayData.Exists(item => x.weather_state_name == item.weather_state_name)));
                            }
                        }
                    }
                }
            }
        }
    }
}