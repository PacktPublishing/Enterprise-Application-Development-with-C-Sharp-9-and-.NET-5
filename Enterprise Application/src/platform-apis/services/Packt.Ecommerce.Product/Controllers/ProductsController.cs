// "//-----------------------------------------------------------------------".
// <copyright file="ProductsController.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

namespace Packt.Ecommerce.Product.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Packt.Ecommerce.DTO.Models;
    using Packt.Ecommerce.Product.Contracts;

    /// <summary>
    /// The products RESTful service.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        /// <summary>
        /// The product service.
        /// </summary>
        private readonly IProductService productService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        /// <param name="productService">The product service.</param>
        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        /// <summary>
        /// Gets the products.
        /// </summary>
        /// <param name="filterCriteria">Product filter criteria.</param>
        /// <returns>Products.</returns>
        [HttpGet]
        public async Task<IActionResult> GetProductsAsync([FromQuery] string filterCriteria = null)
        {
            var products = await this.productService.GetProductsAsync(filterCriteria).ConfigureAwait(false);
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
        /// <param name="name">Product name.</param>
        /// <returns>Product.</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProductById(string id, [FromQuery][Required]string name)
        {
            var product = await this.productService.GetProductByIdAsync(id, name).ConfigureAwait(false);
            if (product != null)
            {
                return this.Ok(product);
            }
            else
            {
                return this.NoContent();
            }
        }

        /// <summary>
        /// Adds the product asynchronously.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns>Product.</returns>
        [HttpPost]
        public async Task<IActionResult> AddProductAsync(ProductDetailsViewModel product)
        {
            // Product null check is to avoid null attribute validation error.
            if (product == null || product.Etag != null)
            {
                return this.BadRequest();
            }

            var result = await this.productService.AddProductAsync(product).ConfigureAwait(false);
            return this.CreatedAtAction(nameof(this.GetProductById), new { id = result.Id, name = result.Name }, result); // HATEOS principle
        }

        /// <summary>
        /// Updates the product asynchronously.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns>HTTP status code.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateProductAsync(ProductDetailsViewModel product)
        {
            if (product == null || product.Etag == null || product.Id == null)
            {
                return this.BadRequest();
            }

            var result = await this.productService.UpdateProductAsync(product).ConfigureAwait(false);
            if (result.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                return this.Accepted();
            }
            else
            {
                return this.NoContent();
            }
        }

        /// <summary>
        /// Deletes the product asynchronous.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <param name="name">Product name.</param>
        /// <returns>The HTTP Response.</returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProductAsync(string id, [FromQuery][Required]string name)
        {
            var result = await this.productService.DeleteProductAsync(id, name).ConfigureAwait(false);
            if (result.StatusCode == System.Net.HttpStatusCode.Accepted)
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
