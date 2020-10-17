// "//-----------------------------------------------------------------------".
// <copyright file="OrdersController.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

namespace Packt.Ecommerce.DataAccess.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Packt.Ecommerce.Data.Models;
    using Packt.Ecommerce.DataStore.Contracts;

    /// <summary>
    /// Orders controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        /// <summary>
        /// The orders repository.
        /// </summary>
        private readonly IOrderRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersController"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public OrdersController(IOrderRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Gets all order.
        /// </summary>
        /// <param name="filterCriteria">The filter criteria.</param>
        /// <returns>Order.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllOrdersync(string filterCriteria = null)
        {
            IEnumerable<Order> order;
            if (string.IsNullOrEmpty(filterCriteria))
            {
                order = await this.repository.GetAsync(string.Empty).ConfigureAwait(false);
            }
            else
            {
                order = await this.repository.GetAsync(filterCriteria).ConfigureAwait(false);
            }

            if (order.Any())
            {
                return this.Ok(order);
            }
            else
            {
                return this.NoContent();
            }
        }

        /// <summary>
        /// Gets the Order by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Order.</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOrderById(string id)
        {
            Order result = await this.repository.GetByIdAsync(id, id).ConfigureAwait(false);
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
        /// Create the order.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <returns>HTTP Status code.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync([FromBody] Order order)
        {
            if (order == null || order.Etag != null)
            {
                return this.BadRequest();
            }

            var result = await this.repository.AddAsync(order, order.Id).ConfigureAwait(false);
            return this.CreatedAtAction(nameof(this.GetOrderById), new { id = result.Resource.Id }, result.Resource);
        }

        /// <summary>
        /// Updates the order.
        /// </summary>
        /// <param name="order">The Order.</param>
        /// <returns>HTTP Status code.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateOrderAsync([FromBody] Order order)
        {
            // Order null check is to avoid null attribute validation error.
            if (order == null || order.Etag == null || order.Id == null)
            {
                return this.BadRequest();
            }

            bool result = await this.repository.ModifyAsync(order, order.Etag, order.Id).ConfigureAwait(false);
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
        /// Deletes the order.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>HTTP Status code.</returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteOrderAsync(string id)
        {
            bool result = await this.repository.RemoveAsync(id, id).ConfigureAwait(false);
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
