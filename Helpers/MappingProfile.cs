using AutoMapper;
using Movies.DTOs;
using Movies.Models;

namespace Movies.Helpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryDTO, Category>()
                .ForMember(src => src.Id, opt=>opt.Ignore());
        }
    }
}
