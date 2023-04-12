using AutoMapper;
using TravelRequisitionSystem.Data;
using TravelRequisitionSystem.DTO;

namespace TravelRequisitionSystem.Profiles
{
    public class AutoMapperInitializer : Profile
    {
        public AutoMapperInitializer()
        {
            CreateMap<TravelRequest, TravelRequestDTO>().ReverseMap();
            CreateMap<TravelRequest, CreateRequestDTO>().ReverseMap();
            CreateMap<TravelRequest, SearchRequestDTO>().ReverseMap();
            CreateMap<TravelRequest, UpdateRequestDTO>().ReverseMap();
        }
    }
}
