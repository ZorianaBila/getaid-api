﻿namespace GetAidBackend.Domain
{
    public class Address
    {
        public string Region { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string FlatNumber { get; set; }
        public string Text => $"Вулиця {Street}, {HouseNumber} {City}, {District} район, {Region} область";
    }
}