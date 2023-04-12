using AutoMapper;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using TravelRequisitionSystem.Data;
using TravelRequisitionSystem.DTO;
using TravelRequisitionSystem.Models;
using TravelRequisitionSystem.Repository.Interfaces;
using TravelRequisitionSystem.Services.Interfaces;

namespace TravelRequisitionSystem.Services.Services
{
    public class RequisitionService : IRequisitionService
    {
        private readonly IRequisitionRepository _requisitionRepository;
        private readonly IMapper _mapper;
        private readonly IRandomGenerator _random;
        private readonly IConfiguration _configuration;

        public RequisitionService(IRequisitionRepository requisitionRepository, IMapper mapper,
            IRandomGenerator random, IConfiguration configuration )
        {
            _requisitionRepository = requisitionRepository;
            _mapper = mapper;
            _random = random;
            _configuration = configuration;
        }

        public async Task<Response> CreateTravelRequest(CreateRequestDTO requestDTO)
        {
            var response = new Response(false);
            try
            {
                var request = _mapper.Map<TravelRequest>(requestDTO);
                request.RequisitonNumber = GenerateRequisitionNumber();
                request.ChargeCode = GenerateChargeCode();
                request.RequestDate = DateTime.Now;
                request.RequisitionStatus = RequisitionStatus.Submitted;
                await _requisitionRepository.AddAsync(request);

                response.Data = request.RequisitonNumber;
                response.IsSuccessful = true;

                return response;
            }
            catch (Exception ex)
            {
                response.Error = new ErrorResponse
                {
                    ResponseCode = "99",
                    ResponseDescription = ex.Message,
                };
                response.IsSuccessful = false;

                return response;
            }
            
        }

        public async Task<Response> GetAllRequests()
        {
            var response = new Response(false);


            var requests = await _requisitionRepository.GetAllAsync();
            var getAll = _mapper.Map<List<SearchRequestDTO>>(requests);

            response.Data = getAll;
            response.IsSuccessful = true;

            return response;


        }

        public async Task<Response> SearchRequestByRequisitionNumber(string requisitionNumber)
        {
            
            var response = new Response(false);
            try
            {
                var request = await _requisitionRepository.GetAsync(x => x.RequisitonNumber == requisitionNumber);
                if (request == null)
                {
                    response.Error = new ErrorResponse
                    {
                        ResponseCode = "99",
                        ResponseDescription = "Travel Request Not Found",
                    };
                    response.IsSuccessful = false;

                    return response;
                }

                var searchRequest = _mapper.Map<SearchRequestDTO>(request);

                response.Data = searchRequest;
                response.IsSuccessful = true;

                return response;
            }
            catch (Exception ex)
            {
                response.Error = new ErrorResponse
                {
                    ResponseCode = "99",
                    ResponseDescription = ex.Message,
                };
                response.IsSuccessful = false;

                return response;
            }
            
        }

        public async Task<Response> UpdateRequest(string requisitionNumber,UpdateRequestDTO model)
        {
            var response = new Response(false);
            try
            {
                var request = await _requisitionRepository.GetAsync(x => x.RequisitonNumber == requisitionNumber);

                if(request == null)
                {
                    response.Error = new ErrorResponse
                    {
                        ResponseCode = "99",
                        ResponseDescription = "Travel Request Not Found",
                    };
                    response.IsSuccessful = false;

                    return response;
                }

                if(request.RequisitionStatus != RequisitionStatus.Booked)
                {
                    request.SourceLocation = model.SourceLocation;
                    request.DepartureDate = model.DepartureDate;
                    request.RequestDate = DateTime.Now;
                    request.DestinationLocation = model.DestinationLocation;
                    request.DestinationCountry = model.DestinationCountry;
                    request.SourceCountry = model.SourceCountry;
                    request.TravelClass = model.TravelClass;
                    request.TripType = model.TripType;
                    request.TravelerName = model.TravelerName;

                    await _requisitionRepository.UpdateAsync(request);

                    response.Data = "Update Completed Successfully";
                    response.IsSuccessful = true;

                    return response;
                }

                else
                {
                    response.Error = new ErrorResponse
                    {
                        ResponseCode = "99",
                        ResponseDescription = "Request Has Been Booked",
                    };
                    response.IsSuccessful = false;

                    return response;
                }

                
            }
            catch (Exception ex)
            {
                response.Error = new ErrorResponse
                {
                    ResponseCode = "99",
                    ResponseDescription = ex.Message,
                };
                response.IsSuccessful = false;

                return response;
            }
            
        }

        public async Task<Response> DeleteRequest(string requisitionNumber)
        {
            var response = new Response(false);
            try
            {
                var request = await _requisitionRepository.GetAsync(x => x.RequisitonNumber == requisitionNumber);

                if(request == null)
                {
                    response.Error = new ErrorResponse
                    {
                        ResponseCode = "99",
                        ResponseDescription = "Travel Request Not Found",
                    };
                    response.IsSuccessful = false;

                    return response;
                }

                await _requisitionRepository.DeleteAsync(request.TravelId);

                response.Data = "Request Deleted Successfully";
                response.IsSuccessful = true;

                return response;
            }
            catch (Exception ex)
            {
                response.Error = new ErrorResponse
                {
                    ResponseCode = "99",
                    ResponseDescription = ex.Message,
                };
                response.IsSuccessful = false;

                return response;
            }
            


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
