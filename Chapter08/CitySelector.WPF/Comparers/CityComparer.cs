using CitySelector.WPF.Model;
using System.Collections.Generic;

namespace CitySelector.WPF.Comparers
{
    /// <summary>
    /// This class implements IEqualityComparer to carry out precisely-defined
    /// value comparisons and facilitate Linq's OrderBy clause for model classes.
    /// </summary>
    /// <remarks>
    /// This class was created by $username$ at $time$
    /// </remarks>
    internal class CityComparer : IEqualityComparer<City>
    {
        public bool Equals(City x, City y)
        {
            return x.CityId == y.CityId;
        }

        public int GetHashCode(City obj)
        {
            return obj.CityId.GetHashCode();
        }
    }
}