using CGHClientServer1.WPF.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CGHClientServer1.API.Client.Interface;
using CodeGenHero.DataService;
using xDTO = CGHClientServer1.DTO.CSC;

namespace CGHClientServer1.WPF.Service
{
    public class CitySelectorApiService : CitySelectorServiceBase
    {
        public CitySelectorApiService(
                    ILogger<CitySelectorApiService> logger,
                    IWebApiDataServiceCSC apiDataService) : base(logger)
        {
            DataService = apiDataService;
        }

        protected IWebApiDataServiceCSC DataService
        {
            get;
            set;
        }

        public override async Task<IList<City>> GetCities(Guid stateId)
        {
            var stopwatch = Stopwatch.StartNew();
            List<City> retVal = new List<City>();

            IList<IFilterCriterion> filter = new List<IFilterCriterion>()
            {
                new FilterCriterion()
                {
                    FieldName = nameof(xDTO.City.StateId),
                    FilterOperator = Constants.OPERATOR_ISEQUALTO,
                    Value = stateId
                }
            };

            IPageDataRequest pageDataRequest = new PageDataRequest()
            {
                Page = 1,
                FilterCriteria = filter,
                PageSize = 100,
                Sort = nameof(City.Name)
            };

            var callResult = await DataService.GetCitiesAsync(pageDataRequest);
            while (callResult.IsSuccessStatusCode)
            {
                var pageData = callResult.Data;
                retVal.AddRange(pageData.Data.Select(x => new City() { Name = x.Name, CityId = x.CityId }));

                if (pageDataRequest.Page >= pageData.TotalPages)
                {
                    break;
                }
                else
                {
                    pageDataRequest.Page++; // Get the next page.
                    callResult = await DataService.GetCitiesAsync(pageDataRequest);
                }
            }

            if (!callResult.IsSuccessStatusCode)
            {
                Log.LogWarning(exception: callResult.Exception, message: "Failure occurred while retrieving city data from the API service. StatusCode: {StatusCode}  RequestUri: {RequestUri}", callResult.StatusCode, callResult.RequestUri);
            }

            stopwatch.Stop();
            Log.LogInformation("Executed {ClassName}.{MethodName} in {Elapsed} Milliseconds", nameof(CitySelectorApiService), MethodBase.GetCurrentMethod().Name, stopwatch.Elapsed.TotalMilliseconds);

            return retVal;
        }

        public override async Task<IList<Country>> GetCountries()
        {
            var stopwatch = Stopwatch.StartNew();
            List<Country> retVal = new List<Country>();

            var resultsDto = await DataService.GetAllPagesCountriesAsync(sort: nameof(Country.Name));
            if (resultsDto.Any())
            {
                retVal.AddRange(resultsDto.Select(x => new Country() { Name = x.Name, CountryId = x.CountryId }));
            }

            stopwatch.Stop();
            Log.LogInformation("Executed {ClassName}.{MethodName} in {Elapsed} Milliseconds", nameof(CitySelectorApiService), MethodBase.GetCurrentMethod().Name, stopwatch.Elapsed.TotalMilliseconds);

            return retVal;
        }

        public override async Task<IList<State>> GetStates(Guid countryId)
        {
            var stopwatch = Stopwatch.StartNew();
            List<State> retVal = new List<State>();

            IList<IFilterCriterion> filter = new List<IFilterCriterion>()
            {
                new FilterCriterion()
                {
                    FieldName = nameof(xDTO.State.CountryId),
                    FilterOperator = Constants.OPERATOR_ISEQUALTO,
                    Value = countryId
                }
            };

            IPageDataRequest pageDataRequest = new PageDataRequest()
            {
                Page = 1,
                FilterCriteria = filter,
                PageSize = 100,
                Sort = nameof(State.Name)
            };

            var callResult = await DataService.GetStatesAsync(pageDataRequest);
            while (callResult.IsSuccessStatusCode)
            {
                var pageData = callResult.Data;
                retVal.AddRange(pageData.Data.Select(x => new State() { Name = x.Name, StateId = x.StateId }));

                if (pageDataRequest.Page >= pageData.TotalPages)
                {
                    break;
                }
                else
                {
                    pageDataRequest.Page++; // Get the next page.
                    callResult = await DataService.GetStatesAsync(pageDataRequest);
                }
            }

            if (!callResult.IsSuccessStatusCode)
            {
                Log.LogWarning(exception: callResult.Exception, message: "Failure occurred while retrieving city data from the API service. StatusCode: {StatusCode}  RequestUri: {RequestUri}", callResult.StatusCode, callResult.RequestUri);
            }

            stopwatch.Stop();
            Log.LogInformation("Executed {ClassName}.{MethodName} in {Elapsed} Milliseconds", nameof(CitySelectorApiService), MethodBase.GetCurrentMethod().Name, stopwatch.Elapsed.TotalMilliseconds);

            return retVal;
        }

        //private async Task<T> GetClientResultAsync<T>(string path) where T : class
        //{
        //    T retVal = null;

        //    HttpResponseMessage response = await _client.GetAsync(path).ConfigureAwait(continueOnCapturedContext: false);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        retVal = await response.Content.ReadAsAsync<T>().ConfigureAwait(continueOnCapturedContext: false);
        //    }

        //    return retVal;
        //}
    }
}