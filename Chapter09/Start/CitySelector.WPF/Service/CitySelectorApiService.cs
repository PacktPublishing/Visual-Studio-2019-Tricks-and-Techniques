using CitySelector.WPF.Data;
using CitySelector.WPF.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;

namespace CitySelector.WPF.Service
{
    public class CitySelectorApiService : CitySelectorServiceBase
    {
        private static readonly HttpClient _client = new HttpClient();

        public CitySelectorApiService(ILogger<CitySelectorMockService> logger) : base(logger)
        {
            _client.BaseAddress = new Uri("http://localhost:5000/api/CountryStateCity/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static List<CitySelectorDataItem> MockDataItems { get; set; } = new List<CitySelectorDataItem>();

        public override async Task<IList<City>> GetCities(Guid stateId)
        {
            var stopwatch = Stopwatch.StartNew();
            List<City> retVal = new List<City>();

            string path = $"City/{stateId}";
            var result = await GetClientResultAsync<List<City>>(path);
            if (result != null)
            {
                retVal.AddRange(result.OrderBy(x => x.Name));
            }

            stopwatch.Stop();
            Log.LogInformation($"Executed {nameof(CitySelectorApiService)}.{MethodBase.GetCurrentMethod().Name} in {(stopwatch.Elapsed.TotalMilliseconds)} Milliseconds");
            return retVal;
        }

        public override async Task<IList<Country>> GetCountries()
        {
            var stopwatch = Stopwatch.StartNew();
            List<Country> retVal = new List<Country>();

            string path = $"Country/";
            var result = await GetClientResultAsync<List<Country>>(path);
            if (result != null)
            {
                retVal.AddRange(result.OrderBy(x => x.Name));
            }

            stopwatch.Stop();
            Log.LogInformation($"Executed {nameof(CitySelectorApiService)}.{MethodBase.GetCurrentMethod().Name} in {(stopwatch.Elapsed.TotalMilliseconds)} Milliseconds");
            return retVal;
        }

        public override async Task<IList<State>> GetStates(Guid countryId)
        {
            var stopwatch = Stopwatch.StartNew();
            List<State> retVal = new List<State>();

            string path = $"State/{countryId}";
            var result = await GetClientResultAsync<List<State>>(path);
            if (result != null)
            {
                retVal.AddRange(result.OrderBy(x => x.Name));
            }

            stopwatch.Stop();
            Log.LogInformation($"Executed {nameof(CitySelectorApiService)}.{MethodBase.GetCurrentMethod().Name} in {(stopwatch.Elapsed.TotalMilliseconds)} Milliseconds");
            return retVal;
        }

        private async Task<T> GetClientResultAsync<T>(string path) where T : class
        {
            T retVal = null;

            HttpResponseMessage response = await _client.GetAsync(path).ConfigureAwait(continueOnCapturedContext: false);
            if (response.IsSuccessStatusCode)
            {
                retVal = await response.Content.ReadAsAsync<T>().ConfigureAwait(continueOnCapturedContext: false);
            }

            return retVal;
        }
    }
}