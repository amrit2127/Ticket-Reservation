using AutoMapper;
using NationalParkAPI_116.Models;
using NationalParkAPI_116.Models.DTOs;

namespace NationalParkAPI_116.DTOMapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<NationalPark,NationalParkDto>().ReverseMap();
            CreateMap<TrailDto,Trail>().ReverseMap();
        }
    }
}
