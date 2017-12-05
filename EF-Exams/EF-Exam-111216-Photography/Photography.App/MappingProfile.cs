namespace Photography.App
{
    using AutoMapper;
    using Import.ImportDtos;
    using Models;

    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Len, LenDto>();
            CreateMap<LenDto, Len>();
            CreateMap<Camera, CameraDto>();
            CreateMap<CameraDto, Camera>();
            CreateMap<Photographer, PhotographerDto>();
            CreateMap<PhotographerDto, Photographer>();
        }
    }
}
