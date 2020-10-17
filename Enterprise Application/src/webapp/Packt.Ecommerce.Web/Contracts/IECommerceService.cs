// "//-----------------------------------------------------------------------".
// <copyright file="IECommerceService.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

namespace Packt.Ecommerce.Web.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Packt.Ecommerce.DTO.Models;

    /// <summary>
    /// Common service interacting with backend.
    /// </summary>
    public interface IECommerceService
    {
        /// <summary>
        /// // Method to retrieve all products and filter.
        /// </summary>
        /// <param name="filterCriteria">Filter criteria.</param>
        /// <returns>List of products.</returns>
        Task<IEnumerable<ProductListViewModel>> GetProductsAsync(string filterCriteria = null);

        /// <summary>
        /// // Method to get details of specific product.
        /// </summary>
        /// <param name="productId">Product Id.</param>
        /// <param name="productName">Product Name.</param>
        /// <returns>Product.</returns>
        Task<ProductDetailsViewModel> GetProductByIdAsync(string productId, string productName);

        /// <summary>
        /// // Method to create and order, this method is primarily used to create a cart which is nothing but an order with order status as “Cart”.
        /// </summary>
        /// <param name="order">Order to be created.</param>
        /// <returns>Order.</returns>
        Task<OrderDetailsViewModel> CreateOrUpdateOrder(OrderDetailsViewModel order);

        /// <summary>
        /// // Method to retrieve order by ID, also used to retrieve cart/order before checkout.
        /// </summary>
        /// <param name="orderId">Order Id.</param>
        /// <returns>Order.</returns>
        Task<OrderDetailsViewModel> GetOrderByIdAsync(string orderId);

        /// <summary>
        /// Method to retrieve invoice details by Id.
        /// </summary>
        /// <param name="invoiceId">Invoice Id.</param>
        /// <returns>Invoice.</returns>
        Task<InvoiceDetailsViewModel> GetInvoiceByIdAsync(string invoiceId);

        /// <summary>
        /// Method to submit cart and create invoice.
        /// </summary>
        /// <param name="order">Order.</param>
        /// <returns>Invoice.</returns>
        Task<InvoiceDetailsViewModel> SubmitOrder(OrderDetailsViewModel order);
    }
}
