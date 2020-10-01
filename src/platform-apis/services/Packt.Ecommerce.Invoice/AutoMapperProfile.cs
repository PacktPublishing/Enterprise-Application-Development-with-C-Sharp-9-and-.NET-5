// "//-----------------------------------------------------------------------".
// <copyright file="AutoMapperProfile.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".
namespace Packt.Ecommerce.Invoice
{
    using AutoMapper;

    /// <summary>
    /// Automapper class.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMapperProfile"/> class.
        /// </summary>
        public AutoMapperProfile()
        {
            this.MapEntity();
        }

        /// <summary>
        /// Maps entities.
        /// </summary>
        private void MapEntity()
        {
            this.CreateMap<Data.Models.Invoice, DTO.Models.InvoiceDetailsViewModel>();
            this.CreateMap<Data.Models.Product, DTO.Models.ProductListViewModel>();
            this.CreateMap<Data.Models.Address, DTO.Models.AddressViewModel>();
            this.CreateMap<Data.Models.SoldBy, DTO.Models.SoldByViewModel>();
        }
    }
}
