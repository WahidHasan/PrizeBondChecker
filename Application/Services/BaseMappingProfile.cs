using AutoMapper;
using PrizeBondChecker.Domain;
using PrizeBondChecker.Domain.Prizebond;

namespace PrizeBondChecker.Services
{
    public class BaseMappingProfile : Profile
    {
        public BaseMappingProfile()
        {
            CreateMap<PrizeBond, PrizebondCreateModel>().ReverseMap();   
        }

    }
}
