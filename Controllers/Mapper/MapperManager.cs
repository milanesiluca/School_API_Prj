using API_School_own_prj.Models.DTOs;
using API_School_own_prj.Models.Entities;
using AutoMapper;

namespace API_School_own_prj.API.Models.Dtos.Mapper
{
    public class MapperManager : Profile
    {
        public MapperManager()
        {
            CreateMap<Activity, ActivityDto>().ReverseMap();
            CreateMap<Activity, ActivityPutDto>().ReverseMap();

            CreateMap<Activity, ActivityListDto>()
                .ForMember(dest => dest.ActivityType, opt =>
                                                      opt.MapFrom(src => $"{src.Type!.Name}"));


            CreateMap<ActivityType, ActivityTypeDto>().ReverseMap();
            CreateMap<Module, ModuleDto>().ReverseMap();
            CreateMap<Module, ModuleManipulationDto>().ReverseMap();
            CreateMap<Module, ModuleForUpdateDto>().ReverseMap();
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Course, CourseForUpdateDto>().ReverseMap();
            CreateMap<ModuleForUpdateDto, Module>().ReverseMap();

            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<Student, StudentForListDto>().ReverseMap();

            CreateMap<Teacher, TeacherDto>().ReverseMap();
            CreateMap<Teacher, TeacherForListDto>().ReverseMap();

            CreateMap<Teacher, User>();
            CreateMap<Student, User>();
        }
    }
}
