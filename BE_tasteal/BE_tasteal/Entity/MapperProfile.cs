using AutoMapper;
using BE_tasteal.Entity.DTO;
using BE_tasteal.Entity.Entity;

namespace BE_tasteal.Entity
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            #region Request DTO to Entity
            CreateMap<RecipeDto, RecipeEntity>()
                .ForMember(destination => destination.id, destination => destination.Ignore())
                .ForMember(source => source.name, destination => destination.MapFrom(src => src.name))
                .ForMember(source => source.rating, destination => destination.MapFrom(src => src.rating))
                .ForMember(source => source.totalTime, destination => destination.MapFrom(src => src.totalTime))
                .ForMember(source => source.active_time, destination => destination.MapFrom(src => src.active_time))
                .ForMember(source => source.serving_size, destination => destination.MapFrom(src => src.serving_size))
                .ForMember(source => source.introduction, destination => destination.MapFrom(src => src.introduction))
                .ForMember(source => source.author_note, destination => destination.MapFrom(src => src.author_note))
                .ForMember(source => source.is_private, destination => destination.MapFrom(src => src.is_private))
                .ForMember(source => source.image, destination => destination.MapFrom(src => src.image))
                .ForMember(source => source.author, destination => destination.MapFrom(src => src.author))
                .ForMember(source => source.nutrition_info_id, destination => destination.MapFrom(src => src.nutrition_info_id));

            CreateMap<SanPhamDto, SanPhamEntity>()
                .ForMember(destination => destination.MaSanPham, destination => destination.Ignore())
                .ForMember(source => source.MaLoaiSanPham, destination => destination.MapFrom(src => src.MaLoaiSanPham))
                .ForMember(source => source.TenSanPham, destination => destination.MapFrom(src => src.TenSanPham));

            CreateMap<LoaiSanPhamDto, LoaiSanPhamEntity>()
                .ForMember(destination => destination.MaLoaiSanPham, destination => destination.Ignore())
                .ForMember(source => source.TenLoaiSanPham, destination => destination.MapFrom(src => src.TenLoaiSanPham))
                .ForMember(source => source.MoTa, destination => destination.MapFrom(src => src.MoTa))
                .ForMember(source => source.TrangThai, destination => destination.MapFrom(src => src.TrangThai));
            #endregion
        }
    }
}
