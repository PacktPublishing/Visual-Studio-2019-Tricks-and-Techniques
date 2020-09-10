using System;

namespace CitySelector.API.Model
{
    public class CitySelectorDataItem
    {
        public Guid CityId { get; set; }

        public string CityName { get; set; }

        public Guid CountryId { get; set; }

        public string CountryName { get; set; }

        public Guid StateId { get; set; }

        public string StateName { get; set; }
    }
}