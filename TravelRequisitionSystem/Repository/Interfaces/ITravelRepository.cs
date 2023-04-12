using TravelRequisitionSystem.Data;
using TravelRequisitionSystem.DTO;

namespace TravelRequisitionSystem.Repository.Interfaces
{
    public interface ITravelRepository
    {
        Task<string> CreateTravelRequest(CreateRequestDTO requestDTO);
        Task<List<SearchRequestDTO>> GetAllTravelRequests();
        Task<SearchRequestDTO> GetRequestByRequsitionNumber(string requisitionNumber);
        Task<string> UpdateRequestByRequisitionNumber(string requisitionNumber, UpdateRequestDTO requestDTO);
    }
}
