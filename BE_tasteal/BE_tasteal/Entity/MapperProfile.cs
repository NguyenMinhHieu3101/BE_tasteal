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

            // To Map from DTO List of Int to Entity List we need t o create a mapper between int and PersonEntity
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
