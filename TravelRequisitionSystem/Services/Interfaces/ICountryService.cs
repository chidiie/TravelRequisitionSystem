using TravelRequisitionSystem.Models;

namespace TravelRequisitionSystem.Services.Interfaces
{
    public interface ICountryService
    {
        Task<Response> SearchLocation(string location);
    }
}
