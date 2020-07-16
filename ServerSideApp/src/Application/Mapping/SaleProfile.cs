using AutoMapper;
using ServerSideApp.Application.DTO;
using ServerSideApp.Domain.Entities;

namespace ServerSideApp.Application.Mapping
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