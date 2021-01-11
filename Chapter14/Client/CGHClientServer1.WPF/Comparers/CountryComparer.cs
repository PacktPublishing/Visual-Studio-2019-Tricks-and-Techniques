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