using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using AlumniWCF.DTO;
using System.Net;
using AlumniWCF.DBML;

namespace AlumniWCF.DTO
{
    public static class Mapping
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<ModelMapping>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });
        public static IMapper Mapper => Lazy.Value;
    }
    public class ModelMapping : Profile
    {
        public ModelMapping()
        {
            CreateMap<Alumni, AlumniDTO>().ReverseMap();
            CreateMap<District, DistrictDTO>().ReverseMap();
            CreateMap<Faculty, FacultyDTO>().ReverseMap();
            CreateMap<Major, MajorDTO>().ReverseMap();
            CreateMap<JobHistory, JobHistoryDTO>().ReverseMap();
            CreateMap<State, StateDTO>().ReverseMap();
            CreateMap<AlumniImage, AlumniImageDTO>().ReverseMap();
        }
    }
}