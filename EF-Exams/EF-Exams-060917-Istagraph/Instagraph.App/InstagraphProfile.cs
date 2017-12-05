﻿using AutoMapper;

namespace Instagraph.App
{
    using System.Security.Principal;
    using DataProcessor;
    using Models;

    public class InstagraphProfile : Profile
    {
        public InstagraphProfile()
        {
            CreateMap<UserDto, User>()
                .ForMember(u=>u.ProfilePicture,pp=>pp.UseValue<Picture>(null));

            CreateMap<Post, NonCommentedPostDto>()
                .ForMember(dto => dto.Picture, pp => pp.MapFrom(p => p.Picture.Path))
                .ForMember(dto => dto.User, u => u.MapFrom(p => p.User.Username));

            CreateMap<User, PopularUserDto>()
                .ForMember(dto => dto.Followers, f => f.MapFrom(u => u.Followers.Count));
        }
    }
}
