using MicroOrm.Dapper.Repositories.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TravelRequisitionSystem.Models;

namespace TravelRequisitionSystem.Data
{
    [Table("TravelRequests")]
    public class TravelRequest
    {
        [Key, Identity]
        public int TravelId { get; set; }
        public string? TravelerName { get; set; }
        public string? SourceLocation { get; set; }
        public string? SourceCountry { get; set; }
        public string? DestinationLocation { get; set; }
        public string? DestinationCountry { get; set; }
        public string? ChargeCode { get; set; }
        public DateTime? DepartureDate { get; set; }

        //[Column(TypeName = "enum")]
        public TripType TripType { get; set; }
        public TravelClass TravelClass { get; set; }
        public RequisitionStatus RequisitionStatus { get; set; }
        public DateTime? RequestDate { get; set; }
        public string? RequisitonNumber { get; set; }

    }
}
