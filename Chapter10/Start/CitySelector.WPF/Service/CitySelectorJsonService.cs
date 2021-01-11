using CitySelector.WPF.Comparers;
using CitySelector.WPF.Data;
using CitySelector.WPF.Model;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CitySelector.WPF.Service
{
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

    public class CitySelectorJsonService : CitySelectorServiceBase
    {
        public CitySelectorJsonService(ILogger<CitySelectorMockService> logger) : base(logger)
        {
            LoadData();
        }

        public static List<CitySelectorDataItem> DataItems { get; set; } = new List<CitySelectorDataItem>();

        public override async Task<IList<City>> GetCities(Guid stateId)
        {
            List<City> retVal = new List<City>();

            retVal.AddRange(DataItems
                .Where(x => x.StateId == stateId)
                .Select(x => new City() { CityId = x.CityId, Name = x.CityName })
                .Distinct(new CityComparer())
                .OrderBy(x => x.Name));

            return retVal;
        }

        public override async Task<IList<Country>> GetCountries()
        {
            List<Country> retVal = new List<Country>();

            retVal.AddRange(DataItems.Select(x => new Country() { CountryId = x.CountryId, Name = x.CountryName })
                .Distinct(new CountryComparer())
                .OrderBy(x => x.Name));

            return retVal;
        }

        public override async Task<IList<State>> GetStates(Guid countryId)
        {
            List<State> retVal = new List<State>();

            retVal.AddRange(DataItems
                .Where(x => x.CountryId == countryId)
                .Select(x => new State() { StateId = x.StateId, Name = x.StateName })
                .Distinct(new StateComparer())
                .OrderBy(x => x.Name));

            return retVal;
        }

        private void LoadData()
        {
            Log.LogInformation($"Start: {nameof(CitySelectorJsonService)}.{nameof(LoadData)}.");
            DataItems.Clear();

            DirectoryInfo diDataFile = new DirectoryInfo(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Data"));
            using (var reader = new StreamReader(@$"{diDataFile.FullName}\SampleData.json"))
            {
                string json = reader.ReadToEnd();
                var dataItems = JsonConvert.DeserializeObject<IList<CitySelectorDataItem>>(json, new JsonSerializerSettings());
                DataItems.AddRange(dataItems);
            }

            Log.LogInformation($"Finished Loading {DataItems.Count} MockDataItems: {nameof(CitySelectorJsonService)}.{nameof(LoadData)}.");
        }
    }

#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
}