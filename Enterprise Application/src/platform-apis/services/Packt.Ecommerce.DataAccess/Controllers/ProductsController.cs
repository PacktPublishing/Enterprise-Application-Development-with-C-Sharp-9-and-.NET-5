// "//-----------------------------------------------------------------------".
// <copyright file="ProductsController.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

namespace Packt.Ecommerce.DataAccess.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Packt.Ecommerce.Data.Models;
    using Packt.Ecommerce.DataStore.Contracts;

    /// <summary>
    /// Products controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        /// <summary>
        /// The products repository.
        /// </summary>
        private readonly IProductRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public ProductsController(IProductRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <param name="filterCriteria">The filter criteria.</param>
        /// <returns>Products.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllProductAsync(string filterCriteria = null)
        {
            IEnumerable<Product> products;
            if (string.IsNullOrEmpty(filterCriteria))
            {
                products = await this.repository.GetAsync(string.Empty).ConfigureAwait(false);
            }
            else
            {
                products = await this.repository.GetAsync(filterCriteria).ConfigureAwait(false);
            }

            if (products.Any())
            {
                return this.Ok(products);
            }
            else
            {
                return this.NoContent();
            }
        }

        /// <summary>
        /// Gets the product by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <returns>Product.</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProductById(string id, [FromQuery][Required] string name)
        {
            Product result = await this.repository.GetByIdAsync(id, name).ConfigureAwait(false);
            if (result != null)
            {
                return this.Ok(result);
            }
            else
            {
                return this.NoContent();
            }
        }

        /// <summary>
        /// Create the product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns>HTTP Status code.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromBody] Product product)
        {
            if (product == null || product.Etag != null)
            {
                return this.BadRequest();
            }

            var result = await this.repository.AddAsync(product, product.Name).ConfigureAwait(false);
            return this.CreatedAtAction(nameof(this.GetProductById), new { id = result.Resource.Id, name = result.Resource.Name }, result.Resource);
        }

        /// <summary>
        /// Updates the product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns>HTTP Status code.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateProductAsync([FromBody] Product product)
        {
            // Product null check is to avoid null attribute validation error.
            if (product == null || product.Etag == null || product.Id == null)
            {
                return this.BadRequest();
            }

            bool result = await this.repository.ModifyAsync(product, product.Etag, product.Name).ConfigureAwait(false);
            if (result)
            {
                return this.Accepted();
            }
            else
            {
                return this.NoContent();
            }
        }

        /// <summary>
        /// Deletes the product.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <returns>HTTP Status code.</returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProductAsync(string id, [FromQuery][Required] string name)
        {
            bool result = await this.repository.RemoveAsync(id, name).ConfigureAwait(false);
            if (result)
            {
                return this.Accepted();
            }
            else
            {
                return this.NoContent();
            }
        }
    }
}
