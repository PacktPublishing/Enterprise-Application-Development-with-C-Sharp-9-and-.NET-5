// "//-----------------------------------------------------------------------".
// <copyright file="OrderDetailsViewModel.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

namespace Packt.Ecommerce.DTO.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Orders view model.
    /// </summary>
    public class OrderDetailsViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [Required]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        [Required]
        public List<ProductListViewModel> Products { get; set; }

        /// <summary>
        /// Gets or sets the order status.
        /// </summary>
        /// <value>
        /// The order status.
        /// </value>
        public string OrderStatus { get; set; }

        /// <summary>
        /// Gets or sets the order placed date.
        /// </summary>
        /// <value>
        /// The order placed date.
        /// </value>
        public string OrderPlacedDate { get; set; }

        /// <summary>
        /// Gets or sets the shipping address.
        /// </summary>
        /// <value>
        /// The shipping address.
        /// </value>
        public AddressViewModel ShippingAddress { get; set; }

        /// <summary>
        /// Gets or sets the tracking identifier.
        /// </summary>
        /// <value>
        /// The tracking identifier.
        /// </value>
        public int TrackingId { get; set; }

        /// <summary>
        /// Gets or sets the delivery date.
        /// </summary>
        /// <value>
        /// The delivery date.
        /// </value>
        public string DeliveryDate { get; set; }

        /// <summary>
        /// Gets or sets the etag.
        /// </summary>
        /// <value>
        /// The etag.
        /// </value>
        public string Etag { get; set; }

        /// <summary>
        /// Gets or sets the Order Total.
        /// </summary>
        /// <value>
        /// The etag.
        /// </value>
        public double OrderTotal { get; set; }

        /// <summary>
        /// Gets or sets the payment mode.
        /// </summary>
        /// <value>
        /// The payment mode.
        /// </value>
        public string PaymentMode { get; set; }
    }
}
