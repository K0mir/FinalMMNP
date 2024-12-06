using AutoMapper;
using FinalData.Data.Entitys;
using WebApiMMNP.Dtos;

namespace WebApiMMNP.AutoMapperProfile
{
    public class DeporteProfile : Profile
    {
        public DeporteProfile()
        {
            CreateMap<Deporte, DeporteDtos>();
            CreateMap<DeporteDtos, Deporte>();
        }
    }
}
