namespace WeddingsPlanner.App
{
    using AutoMapper;
    using WeddinsPlanner.DataProcessor;
    using WeddinsPlanner.DataProcessor.ImportDtos;
    using WeddinsPlanner.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Cash, PresentDto>();
            CreateMap<Gift, PresentDto>();
            CreateMap<Present, PresentDto>();
               
            CreateMap<PresentDto, Cash>();
            CreateMap<PresentDto, Gift>()
                .ForMember(dest=>dest.Name,opt=>opt.MapFrom(src=>src.Name));
            CreateMap<PresentDto, Present>();
        }
    }
}