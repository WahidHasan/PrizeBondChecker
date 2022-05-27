using AutoMapper;
using PrizeBondChecker.Domain.Prizebond;

namespace PrizeBondChecker.Services
{
    public class BaseMappingProfile : Profile
    {
        public BaseMappingProfile()
        {
            CreateMap<Prizebond, PrizebondCreateModel>().ReverseMap();   
        }

    }
}
