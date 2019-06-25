using System;

namespace ExampleApp.Models
{
    public class Remark
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Note { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime DateCreated { get; set; }
    }
}