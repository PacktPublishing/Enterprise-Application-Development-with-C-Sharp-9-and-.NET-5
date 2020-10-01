// "//-----------------------------------------------------------------------".
// <copyright file="IOrderService.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

namespace Packt.Ecommerce.Order.Contracts
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Packt.Ecommerce.DTO.Models;

    /// <summary>
    /// Orders service contract for CRUD operations.
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Gets the orders asynchronously.
        /// </summary>
        /// <param name="filterCriteria">Criteria to filter orders.</param>
        /// <returns>List of orders with minimum details.</returns>
        Task<IEnumerable<OrderDetailsViewModel>> GetOrdersAsync(string filterCriteria = null);

        /// <summary>
        /// Gets the order by identifier asynchronously.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <returns>The order.</returns>
        Task<OrderDetailsViewModel> GetOrderByIdAsync(string orderId);

        /// <summary>
        /// Adds the order asynchronously.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <returns>Order.</returns>
        Task<OrderDetailsViewModel> AddOrderAsync(OrderDetailsViewModel order);

        /// <summary>
        /// Updates the order asynchronously.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <returns>The HTTP Response.</returns>
        Task<HttpResponseMessage> UpdateOrderAsync(OrderDetailsViewModel order);
    }
}
