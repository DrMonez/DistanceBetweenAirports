using DistanceBetweenAirports.Models;
using System.Runtime.Serialization;

namespace DistanceBetweenAirports.Views
{
    [DataContract]
    public class AirportInfoDto
    {
        [DataMember(Name = "country")]
        public string Country { get; set; }

        [DataMember(Name = "city_iata")]
        public string CityIata { get; set; }

        [DataMember(Name = "iata")]
        public string Iata { get; set; }

        [DataMember(Name = "city")]
        public string City { get; set; }

        [DataMember(Name = "timezone_region_name")]
        public string TimezoneRegionName { get; set; }

        [DataMember(Name = "country_iata")]
        public string CountryIata { get; set; }

        [DataMember(Name = "rating")]
        public int Rating { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "location")]
        public LocationDto Location { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "hubs")]
        public int Hubs { get; set; }

        public AirportInfo ToModel()
        {
            return new AirportInfo
            {
                Country = Country,
                City = City,
                CityIata = CityIata,
                Iata = Iata,
                CountryIata = CountryIata,
                TimezoneRegionName = TimezoneRegionName,
                Rating = Rating,
                Name = Name,
                Location = Location.ToModel(),
                Type = Type,
                Hubs = Hubs
            };
        }
    }
}