using TravelRequisitionSystem.Models;

namespace TravelRequisitionSystem.DTO
{
    public class SearchRequestDTO
    {
        public string? TravelerName { get; set; }
        public string? SourceLocation { get; set; }
        public string? SourceCountry { get; set; }
        public string? DestinationLocation { get; set; }
        public string? DestinationCountry { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? DepartureDate { get; set; }
        public string? TripType { get; set; }
        public string? TravelClass { get; set; }
        public string? RequisitonNumber { get; set; }
    }
}
