// "//-----------------------------------------------------------------------".
// <copyright file="IProductService.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

namespace Packt.Ecommerce.Product.Contracts
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Packt.Ecommerce.DTO.Models;

    /// <summary>
    /// Products service contract for CRUD operations.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Gets the products asynchronously.
        /// </summary>
        /// <param name="filterCriteria">Criteria to filter products.</param>
        /// <returns>List of products with minimum details.</returns>
        Task<IEnumerable<ProductListViewModel>> GetProductsAsync(string filterCriteria = null);

        /// <summary>
        /// Gets the product by identifier asynchronously.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="productName">Product name.</param>
        /// <returns>The product.</returns>
        Task<ProductDetailsViewModel> GetProductByIdAsync(string productId, string productName);

        /// <summary>
        /// Adds the product asynchronously.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns>Product.</returns>
        Task<ProductDetailsViewModel> AddProductAsync(ProductDetailsViewModel product);

        /// <summary>
        /// Updates the product asynchronously.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns>The HTTP Response.</returns>
        Task<HttpResponseMessage> UpdateProductAsync(ProductDetailsViewModel product);

        /// <summary>
        /// Deletes the product asynchronous.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="productName">Product name.</param>
        /// <returns>The HTTP Response.</returns>
        Task<HttpResponseMessage> DeleteProductAsync(string productId, string productName);
    }
}
