using CGHClientServer1.WPF.Model;
using System.Collections.Generic;

namespace CGHClientServer1.WPF.Comparers
{
    /// <summary>
    /// This class implements IEqualityComparer to carry out precisely-defined
    /// value comparisons and facilitate Linq's OrderBy clause for model classes.
    /// </summary>
    /// <remarks>
    /// This class was created by PaulSchroeder at 9/25/2020 9:12:56 PM
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