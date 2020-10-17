// "//-----------------------------------------------------------------------".
// <copyright file="OrdersController.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

namespace Packt.Ecommerce.Order.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Packt.Ecommerce.DTO.Models;
    using Packt.Ecommerce.Order.Contracts;

    /// <summary>
    /// The orders RESTful service.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/Orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        /// <summary>
        /// The order service.
        /// </summary>
        private readonly IOrderService orderService;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersController"/> class.
        /// </summary>
        /// <param name="orderService">The order service.</param>
        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        /// <summary>
        /// Gets the orders.
        /// </summary>
        /// <param name="filterCriteria">Order filter criteria.</param>
        /// <returns>Orders.</returns>
        [HttpGet]
        public async Task<IActionResult> GetOrdersAsync([FromQuery] string filterCriteria = null)
        {
            var orders = await this.orderService.GetOrdersAsync(filterCriteria).ConfigureAwait(false);
            if (orders.Any())
            {
                return this.Ok(orders);
            }
            else
            {
                return this.NoContent();
            }
        }

        /// <summary>
        /// Gets the order by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Order.</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOrderById(string id)
        {
            var order = await this.orderService.GetOrderByIdAsync(id).ConfigureAwait(false);
            if (order != null)
            {
                return this.Ok(order);
            }
            else
            {
                return this.NoContent();
            }
        }

        /// <summary>
        /// Adds the order asynchronously.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <returns>Order.</returns>
        [HttpPost]
        public async Task<IActionResult> AddOrderAsync(OrderDetailsViewModel order)
        {
            // Order null check is to avoid null attribute validation error.
            ////if (order == null || order.Etag != null)
            ////{
            ////    return this.BadRequest();
            ////}

            var result = await this.orderService.AddOrderAsync(order).ConfigureAwait(false);
            return this.CreatedAtAction(nameof(this.GetOrderById), new { id = result.Id }, result); // HATEOS principle
        }

        /// <summary>
        /// Updates the order asynchronously.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <returns>HTTP status code.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateOrderAsync(OrderDetailsViewModel order)
        {
            if (order == null || order.Etag == null || order.Id == null)
            {
                return this.BadRequest();
            }

            var result = await this.orderService.UpdateOrderAsync(order).ConfigureAwait(false);
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
