using $safeprojectname$.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace $safeprojectname$.Service
{
    public abstract class CitySelectorServiceBase : ICitySelectorService
    {
        public CitySelectorServiceBase(ILogger logger)
        {
            Log = logger;
        }

        protected ILogger Log { get; set; }

        public virtual Task<IList<City>> GetCities(Guid stateId)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IList<Country>> GetCountries()
        {
            throw new NotImplementedException();
        }

        public virtual Task<IList<State>> GetStates(Guid countryId)
        {
            throw new NotImplementedException();
        }
    }
}