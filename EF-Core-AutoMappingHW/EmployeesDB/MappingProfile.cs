namespace Employees.App
{
    using AutoMapper;
    using Models;
    using ModelsDTO;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();
            CreateMap<Employee, EmployeePersonalDto>();
            CreateMap<ManagerDto, Employee>();
            CreateMap<Employee, ManagerDto>()
                .ForMember(dto => dto.SubordinatesCount, opt => opt.MapFrom(src => src.Subordinates.Count));
            CreateMap<Employee, EmployeeListDto>()
                .ForMember(dto=>dto.ManagerFirstName,opt=>opt.MapFrom(src=>src.Manager.FirstName));
        }
    }
}
