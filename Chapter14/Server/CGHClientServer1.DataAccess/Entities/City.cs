using System;
using System.Collections.Generic;

namespace CGHClientServer1.DB.Entities
{
    public partial class City
    {
        public Guid CityId { get; set; }
        public Guid StateId { get; set; }
        public string Name { get; set; }

        public virtual State State { get; set; }
    }
}
