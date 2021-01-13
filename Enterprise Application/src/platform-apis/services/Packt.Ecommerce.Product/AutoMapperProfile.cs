// "//-----------------------------------------------------------------------".
// <copyright file="AutoMapperProfile.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".
namespace Packt.Ecommerce.Product
{
    using System.Linq;
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
            this.CreateMap<Data.Models.Product, DTO.Models.ProductDetailsViewModel>();
            this.CreateMap<Data.Models.Rating, DTO.Models.RatingViewModel>();
            this.CreateMap<Data.Models.Product, DTO.Models.ProductListViewModel>()
                .ForMember(x => x.AverageRating, o => o.MapFrom(a => a.Rating != null && a.Rating.Count > 0 ? a.Rating.Average(y => y.Stars) : 0));
        }
    }
}