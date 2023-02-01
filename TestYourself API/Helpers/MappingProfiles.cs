using AutoMapper;
using TestYourself_API.Dto;
using TestYourself_API.Models;

namespace TestYourself_API.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Test, TestDto>();
            CreateMap<Test, EditTestDto>();
        }
    }
}
