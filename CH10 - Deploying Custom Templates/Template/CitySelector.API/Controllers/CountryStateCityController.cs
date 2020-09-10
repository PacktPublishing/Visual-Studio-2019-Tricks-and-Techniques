using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using $safeprojectname$.Comparers;
using $safeprojectname$.Model;

namespace $safeprojectname$.Controllers
{
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

    [RoutePrefix("api")]
    public class CountryStateCityController : ApiController
    {
        private static readonly Lazy<List<CitySelectorDataItem>> _citySelectorDataItems = new Lazy<List<CitySelectorDataItem>>(LoadSampleData);

        public List<CitySelectorDataItem> SampleData
        {
            get
            {
                return _citySelectorDataItems.Value;
            }
        }

        [Route("Cities/{stateId:guid}")]
        [ResponseType(typeof(List<City>))]
        public async Task<IHttpActionResult> GetCities(Guid stateId)
        {
            var retVal = new List<City>();

            retVal.AddRange(SampleData.Where((x => x.StateId == stateId))
                .Select(x => new City()
                {
                    CityId = x.CityId,
                    Name = x.CityName
                }).Distinct(new CityComparer()).ToList());

            return Json(retVal);
        }

        [Route("Countries")]
        [ResponseType(typeof(List<City>))]
        public async Task<IHttpActionResult> GetCountries()
        {
            var retVal = new List<Country>();

            retVal.AddRange(SampleData
                .Select(x => new Country()
                {
                    CountryId = x.CountryId,
                    Name = x.CountryName
                }).Distinct(new CountryComparer()).ToList());

            return Json(retVal);
        }

        [Route("States/{countryId:guid}")]
        [ResponseType(typeof(List<City>))]
        public async Task<IHttpActionResult> GetStates(Guid countryId)
        {
            var retVal = new List<State>();

            retVal.AddRange(SampleData.Where((x => x.CountryId == countryId))
                .Select(x => new State()
                {
                    StateId = x.StateId,
                    Name = x.StateName
                }).Distinct(new StateComparer()).ToList());

            return Json(retVal);
        }

        private static List<CitySelectorDataItem> LoadSampleData()
        {
            var retVal = new List<CitySelectorDataItem>();

            for (int i = 1; i < 10; i++)
            {
                Guid countryId = Guid.NewGuid();
                string countryName = $"Sample Country {i}";

                for (int j = 1; j < 10; j++)
                {
                    Guid stateId = Guid.NewGuid();
                    string stateName = $"Sample State {i}:{j}";

                    for (int k = 1; k < 10; k++)
                    {
                        Guid cityId = Guid.NewGuid();
                        string cityName = $"Sample City {i}:{j}:{k}";

                        retVal.Add(new CitySelectorDataItem()
                        {
                            CityId = cityId,
                            CityName = cityName,
                            CountryId = countryId,
                            CountryName = countryName,
                            StateId = stateId,
                            StateName = stateName
                        });
                    }
                }
            }

            return retVal;
        }
    }

#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
}