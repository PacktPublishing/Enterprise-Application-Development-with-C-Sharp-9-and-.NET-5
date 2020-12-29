// "//-----------------------------------------------------------------------".
// <copyright file="ProductService.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Packt.Ecommerce.DataAccess.Protos;
using Packt.Ecommerce.DataStore.Contracts;

namespace Packt.Ecommerce.DataAccess.Services
{
    /// <summary>
    /// Products service.
    /// </summary>
    public class ProductService : Protos.ProductManager.ProductManagerBase
    {
        /// <summary>
        /// The products repository.
        /// </summary>
        private readonly IProductRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="repository">products repository.</param>
        public ProductService(IProductRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Get products.
        /// </summary>
        /// <param name="request">request object.</param>
        /// <param name="context">context object.</param>
        /// <returns>specific product.</returns>
        public async override Task<Product> GetProductById(ProductQuery request, ServerCallContext context)
        {
            var result = await this.repository.GetByIdAsync(request.Id, request.Name);
            var response = new Product
            {
                Category = result.Category,
                Etag = result.Etag,
                CreatedDate = result.CreatedDate.ToTimestamp(),
                Id = result.Id,
                Name = result.Name,
                Price = result.Price,
                Quantity = result.Quantity,
            };
            response.Authors.Add(result.Authors ?? new List<string>());
            response.Color.Add(result.Color);
            response.Format.Add(response.Format);
            response.ImageUrls.Add(result.ImageUrls);
            response.Rating.Add(result.Rating.Select(e => new Rating { Percentage = e.Percentage, Stars = e.Stars }));
            response.Size.Add(response.Size);

            return response;
        }
    }
}
