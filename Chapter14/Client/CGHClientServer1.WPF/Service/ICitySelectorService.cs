using CGHClientServer1.WPF.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CGHClientServer1.WPF.Service
{
    public interface ICitySelectorService
    {
        Task<IList<City>> GetCities(Guid stateId);

        Task<IList<Country>> GetCountries();

        Task<IList<State>> GetStates(Guid countryId);
    }
}