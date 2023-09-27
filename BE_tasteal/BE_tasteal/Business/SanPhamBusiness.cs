﻿using AutoMapper;
using BE_tasteal.Business.Interface;
using BE_tasteal.Entity.DTO;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Interface;

namespace BE_tasteal.Business
{
    public class SanPhamBusiness : IBusiness<SanPhamDto, SanPhamEntity>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<SanPhamBusiness> _logger;
        private readonly ISanPhamResposity _sanphamResposity;
        // private readonly IMapper _mapper;

        public SanPhamBusiness(IMapper mapper,
            ILogger<SanPhamBusiness> logger,
            ISanPhamResposity sanphamResposity)
        {
            _mapper = mapper;
            _logger = logger;
            _sanphamResposity = sanphamResposity;
        }

        #region Implement interface 
        public async Task<List<SanPhamEntity?>> GetAll()
        {
            var sanphams = await _sanphamResposity.GetAll();
            return sanphams;
        }

        public async Task<SanPhamEntity?> Add(SanPhamDto entity)
        {

            var sanpham = await _sanphamResposity.InsertAsync(_mapper.Map<SanPhamEntity>(entity));

            _logger.LogInformation($"Added sanPham ", entity);
            return sanpham;
        }

        #endregion
    }
}
