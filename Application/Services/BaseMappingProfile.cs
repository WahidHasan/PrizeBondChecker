using Application.Models.PrizebondView;
using AutoMapper;
using Domain.Prizebond;
using PrizeBondChecker.Domain.Prizebond;

namespace PrizeBondChecker.Services
{
    public class BaseMappingProfile : Profile
    {
        public BaseMappingProfile()
        {
            CreateMap<Prizebond, PrizebondCreateModel>().ReverseMap();
            CreateMap<Prizebond, PrizebondViewModel>().ReverseMap();
            CreateMap<PrizebondListViewModel, UserPrizebonds>().ReverseMap();
        }

    }
}
