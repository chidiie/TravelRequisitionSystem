namespace TravelRequisitionSystem.Models
{
    public class CountryDetails
    {
        public string Country { get; set; }
        public string Province { get; set; }
        public string Currency { get; set; }
        public WeatherDetails WeatherDetails { get; set; }
    }

    public class WeatherDetails
    {
        public string Humidity { get; set; }
        public string Pressure { get; set; }
        public string MaximumTemperature { get; set; }
        public string MinimumTemperature { get; set; }
    }
}
