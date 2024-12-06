using AutoMapper;
using FinalData.Data.Entitys;
using WebApiMMNP.Dtos;

namespace WebApiMMNP.AutoMapperProfile
{
    public class TipoDeporteProfile : Profile
    {
        public TipoDeporteProfile()
        {
            CreateMap<TipoDeporte, TipoDeporteDtos>()
                .ForMember(dest => dest.IdTipo, opt => opt.MapFrom(src => src.IdTipo))
                .ForMember(dest => dest.NombreTipo, opt => opt.MapFrom(src => src.NombreTipo))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
                .ReverseMap();
        }
    }
}
