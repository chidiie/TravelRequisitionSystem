using TravelRequisitionSystem.Models;

namespace TravelRequisitionSystem.DTO
{
    public class TravelRequestDTO
    {
        public string? TravelerName { get; set; }
        public string? SourceLocation { get; set; }
        public string? SourceCountry { get; set; }
        public string? DestinationLocation { get; set; }
        public string? DestinationCountry { get; set; }
        public DateTime? DepartureDate { get; set; }
        public TripType TripType { get; set; }
        public TravelClass TravelClass { get; set; }
    }
}
