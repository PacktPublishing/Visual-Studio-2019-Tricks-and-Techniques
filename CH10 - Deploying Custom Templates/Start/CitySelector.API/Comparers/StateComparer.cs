using CitySelector.API.Model;
using System.Collections.Generic;

namespace CitySelector.API.Comparers
{
    /// <summary>
    /// This class implements IEqualityComparer to carry out precisely-defined
    /// value comparisons and facilitate Linq's OrderBy and/or Distinct clauses for model classes.
    /// </summary>
    internal class StateComparer : IEqualityComparer<State>
    {
        public bool Equals(State x, State y)
        {
            return x.StateId == y.StateId;
        }

        public int GetHashCode(State obj)
        {
            return obj.StateId.GetHashCode();
        }
    }
}