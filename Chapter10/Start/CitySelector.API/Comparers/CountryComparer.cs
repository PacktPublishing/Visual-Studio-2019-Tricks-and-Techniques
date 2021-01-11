using CitySelector.API.Model;
using System.Collections.Generic;

namespace CitySelector.API.Comparers
{
    /// <summary>
    /// This class implements IEqualityComparer to carry out precisely-defined
    /// value comparisons and facilitate Linq's OrderBy clause for model classes.
    /// </summary>
    /// <remarks>
    /// This class was created by $username$ at $time$
    /// </remarks>
    internal class CountryComparer : IEqualityComparer<Country>
    {
        public bool Equals(Country x, Country y)
        {
            return x.CountryId == y.CountryId;
        }

        public int GetHashCode(Country obj)
        {
            return obj.CountryId.GetHashCode();
        }
    }
}