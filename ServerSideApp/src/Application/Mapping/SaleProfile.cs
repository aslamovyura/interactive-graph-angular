using Application.DTO;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    /// <summary>
    /// Define mapping profile for Sale entity & Sale DTO.
    /// </summary>
    public class SaleProfile : Profile
    {
        /// <summary>
        /// Mapping rule for Sale entity & Sale DTO.
        /// </summary>
        public SaleProfile()
        {
            CreateMap<Sale, SaleDTO>().ReverseMap();
        }
    }
}