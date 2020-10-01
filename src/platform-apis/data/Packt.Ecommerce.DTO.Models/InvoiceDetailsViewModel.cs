// "//-----------------------------------------------------------------------".
// <copyright file="InvoiceDetailsViewModel.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

namespace Packt.Ecommerce.DTO.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Invoice view model.
    /// </summary>
    public class InvoiceDetailsViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the order identifier.
        /// </summary>
        /// <value>
        /// The order identifier.
        /// </value>
        public string OrderId { get; set; }

        /// <summary>
        /// Gets or sets the payment mode.
        /// </summary>
        /// <value>
        /// The payment mode.
        /// </value>
        public string PaymentMode { get; set; }

        /// <summary>
        /// Gets or sets the shipping address.
        /// </summary>
        /// <value>
        /// The shipping address.
        /// </value>
        public AddressViewModel ShippingAddress { get; set; }

        /// <summary>
        /// Gets or sets the sold by.
        /// </summary>
        /// <value>
        /// The sold by.
        /// </value>
        public SoldByViewModel SoldBy { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public List<ProductListViewModel> Products { get; set; }

        /// <summary>
        /// Gets or sets the etag.
        /// </summary>
        /// <value>
        /// The etag.
        /// </value>
        public string Etag { get; set; }
    }
}
