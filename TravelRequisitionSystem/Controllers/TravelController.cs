using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelRequisitionSystem.DTO;
using TravelRequisitionSystem.Models;
using TravelRequisitionSystem.Repository.Interfaces;
using TravelRequisitionSystem.Services.Interfaces;

namespace TravelRequisitionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class TravelController : ControllerBase
    {
        private readonly ITravelRepository _repository;
        private readonly ILogger<TravelController> _logger;
        private readonly IRequisitionService _requisitionService;
        private readonly ICountryService _countryService;

        public TravelController(ITravelRepository repository, ILogger <TravelController> logger,
            IRequisitionService requisitionService, ICountryService countryService)
        {
            _repository = repository;
            _logger = logger;
            _requisitionService = requisitionService;
            _countryService = countryService;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitTravelRequest(CreateRequestDTO requestDTO)
        {
            var response = await _requisitionService.CreateTravelRequest(requestDTO);
            return Ok(response);
            //try
            //{
            //    var response = await _repository.CreateTravelRequest(requestDTO);
            //    var returnResponse = new ReturnResponse();
            //    returnResponse.Response = response;
            //    return Ok(returnResponse);
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, ex.Message);
            //}
        }

        [HttpGet("GetAllRequests")]
        public async Task<IActionResult> GetAllRequests()
        {
            //var requests = await _repository.GetAllTravelRequests();
            //return Ok(requests);
            var requests = await _requisitionService.GetAllRequests();
            return Ok(requests);

        }

        [HttpGet("SearchRequest")]
        public async Task<IActionResult> GetRequestByRequisitionNumber(string requisitionNumber)
        {
            //var request = await _repository.GetRequestByRequsitionNumber(requisitionNumber);
            //return Ok(request);

            var response = await _requisitionService.SearchRequestByRequisitionNumber(requisitionNumber);
            return Ok(response);
        }

        [HttpPut("UpdateRequest")]
        public async Task<IActionResult> UpdateRequestByRequisitionNumber(string requisitionNumber, UpdateRequestDTO requestDTO)
        {

            //if (requestDTO == null)
            //{
            //    return StatusCode(400);
            //}
            //else
            //{
            //    var response = await _repository.UpdateRequestByRequisitionNumber(requisitionNumber, requestDTO);
            //    if (response == null)
            //    {
            //        return StatusCode(400);
            //    }

            //    else
            //    {
            //        var returnResponse = new ReturnResponse();
            //        returnResponse.Response = response;
            //        return Ok(returnResponse);
            //    }
            //}

            var update = await _requisitionService.UpdateRequest(requisitionNumber, requestDTO);
            return Ok(update);

        }

        [HttpDelete("DeleteRequest")] 
        public async Task<IActionResult> DeleteRequest(string requisitionNumber)
        {
            var response = await _requisitionService.DeleteRequest(requisitionNumber);
            return Ok(response);
        }

        [HttpGet("GetLocationWeather")]
        public async Task<IActionResult> GetLocationWeather(string location)
        {
            var name1 = "abayomi@ubagroup.com";
            string mail = name1.Split('@')[0];
            var names1 = mail.Split(".");
            var fullName = names1[0] + " " + names1[1];
            var fullName2 = CapitalizeNames(fullName);
            var cFullName = fullName2.Split(" ");
            var cFirstName = cFullName[0];
            var response = await _countryService.SearchLocation(location);
            return Ok(response);
        }

        private string CapitalizeNames(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return username; // return the input string if it's null or empty
            }

            string[] words = username.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                if (!string.IsNullOrEmpty(words[i]))
                {
                    words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1);
                    break;
                }
            }
            return string.Join(' ', words);
        }
    }
}
