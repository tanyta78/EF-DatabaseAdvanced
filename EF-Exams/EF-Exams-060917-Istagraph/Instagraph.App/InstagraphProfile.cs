using AutoMapper;

namespace Instagraph.App
{
    using DataProcessor;
    using Models;

    public class InstagraphProfile : Profile
    {
        public InstagraphProfile()
        {
            CreateMap<UserDto, User>()
                .ForMember(u=>u.ProfilePicture,pp=>pp.UseValue<Picture>(null));

        }
    }
}
