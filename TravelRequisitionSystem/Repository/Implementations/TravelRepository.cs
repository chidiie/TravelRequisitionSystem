using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TravelRequisitionSystem.Data;
using TravelRequisitionSystem.DTO;
using TravelRequisitionSystem.Repository.Interfaces;
using TravelRequisitionSystem.Services;
using TravelRequisitionSystem.Models;

namespace TravelRequisitionSystem.Repository.Implementations
{
    public class TravelRepository : ITravelRepository
    {
        private readonly TravelDbContext _context;
        private readonly ILogger<TravelRepository> _logger;
        private readonly IMapper _mapper;
        private readonly IRandomGenerator _random;

        public TravelRepository(TravelDbContext context, ILogger<TravelRepository> logger, IMapper mapper,
            IRandomGenerator random)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _random = random;
        }
        public async Task<string> CreateTravelRequest(CreateRequestDTO requestDTO)
        {
            try
            {
                var request = _mapper.Map<TravelRequest>(requestDTO);
                request.RequisitonNumber = GenerateRequisitionNumber();
                request.RequisitionStatus = RequisitionStatus.Submitted;
                request.RequestDate = DateTime.Now;
                request.ChargeCode = GenerateChargeCode();
                await _context.AddAsync(request);
                await _context.SaveChangesAsync();
                return request.RequisitonNumber;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal Server Error Occured");
                throw;
            }
            //TravelRequest request = new TravelRequest();
                     
        }

        public async Task<List<SearchRequestDTO>> GetAllTravelRequests()
        {
            var requests = await _context.TravelRequests.ToListAsync();
            var response = _mapper.Map<List<SearchRequestDTO>>(requests);

            return response;
        }

        public async Task<SearchRequestDTO> GetRequestByRequsitionNumber(string requisitionNumber)
        {
            var request = await _context.TravelRequests.Where(x => x.RequisitonNumber == requisitionNumber).FirstOrDefaultAsync();
            var response = _mapper.Map<SearchRequestDTO>(request);
            response.TripType = Enum.GetName(typeof(TripType), response.TripType);
            response.TravelClass = Enum.GetName(typeof(TravelClass), response.TravelClass);
            return response;
        }

        public async Task<string> UpdateRequestByRequisitionNumber(string requisitionNumber, UpdateRequestDTO requestDTO)
        {
            if (string.IsNullOrEmpty(requisitionNumber))
            {
                return null;
            }
            else
            {
                var request = await _context.TravelRequests.FirstOrDefaultAsync(x => x.RequisitonNumber == requisitionNumber);
                
                if (request != null && request.RequisitionStatus != RequisitionStatus.Booked)
                {
                    //var mapped = _mapper.Map<TravelRequest>(requestDTO);
                    //_context.TravelRequests.Update(mapped);
                    //await _context.SaveChangesAsync();
                    var response = _mapper.Map(requestDTO, request);
                    _context.TravelRequests.Update(response);
                    await _context.SaveChangesAsync();
                    return "Updated Successfully";
                }
                else
                {
                    if (request == null)
                    {
                        return null;
                    }

                    else
                    {
                        return "Request Status is currently booked";
                    }
                }
            }
        }

        public async Task<string> DeleteRequestByRequisitionNumber(string requisitionNumber)
        {
            var request = await _context.TravelRequests.FirstOrDefaultAsync(x => x.RequisitonNumber == requisitionNumber);
            _context.Remove(request);
            await _context.SaveChangesAsync();
            return "Deleted Successfully";

        }

        private string GenerateRequisitionNumber()
        {
            var select = _random.RandomPassword();
            string requisition = $"R{select}";
            return requisition;

        }

        private string GenerateChargeCode()
        {
            var select = _random.RandomPassword();
            string charge = $"C{select}";
            return charge;
        }
    }
}
