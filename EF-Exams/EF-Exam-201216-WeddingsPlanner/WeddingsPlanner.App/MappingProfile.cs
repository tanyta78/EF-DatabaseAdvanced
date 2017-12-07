namespace WeddingsPlanner.App
{
    using AutoMapper;
    using WeddinsPlanner.DataProcessor;
    using WeddinsPlanner.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Cash, PresentDto>();
            CreateMap<Gift, PresentDto>();
            CreateMap<Present, PresentDto>();
            CreateMap<PresentDto, Cash>();
            CreateMap<PresentDto, Gift>();
            CreateMap<PresentDto, Present>();
        }
    }
}