using TravelRequisitionSystem.DTO;
using TravelRequisitionSystem.Models;

namespace TravelRequisitionSystem.Services.Interfaces
{
    public interface IRequisitionService
    {
        Task<Response> CreateTravelRequest(CreateRequestDTO requestDTO);
        Task<Response> GetAllRequests();
        Task<Response> SearchRequestByRequisitionNumber(string requisitionNumber);
        Task<Response> UpdateRequest(string requisitionNumber, UpdateRequestDTO model);
        Task<Response> DeleteRequest(string requisitionNumber);
    }
}
