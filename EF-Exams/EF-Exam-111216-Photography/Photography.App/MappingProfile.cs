namespace Photography.App
{
    using AutoMapper;
    using DataProcessor.ImportDtos;
    using Models;

    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Len, LenDto>();
            CreateMap<LenDto, Len>();
            CreateMap<Camera, CameraDto>();
            CreateMap<CameraDto, Camera>();
            CreateMap<CameraDto, DSLRCamera>();
            CreateMap<CameraDto, MirrorlessCamera>();
            CreateMap<Photographer, PhotographerDto>();
            CreateMap<PhotographerDto, Photographer>();
            CreateMap<Workshop, WorkshopDto>();
            CreateMap<Photographer, ParticipantDto>();
        }
    }
}
