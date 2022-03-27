﻿using DistanceBetweenAirports.Models;
using System.Runtime.Serialization;

namespace DistanceBetweenAirports.Views
{
    [DataContract]
    public class LocationDto
    {
        [DataMember(Name = "lon")]
        public double Longitude { get; set; }

        [DataMember(Name = "lat")]
        public double Latitude { get; set; }

        public Location ToModel()
        {
            return new Location
            {
                Latitude = Latitude,
                Longitude = Longitude
            };
        }
    }
}
