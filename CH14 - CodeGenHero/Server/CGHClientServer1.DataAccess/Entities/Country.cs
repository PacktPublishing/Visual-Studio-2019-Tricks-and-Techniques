using System;
using System.Collections.Generic;

namespace CGHClientServer1.DB.Entities
{
    public partial class Country
    {
        public Country()
        {
            States = new HashSet<State>();
        }

        public Guid CountryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<State> States { get; set; }
    }
}
