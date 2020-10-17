// "//-----------------------------------------------------------------------".
// <copyright file="ProductListViewModel.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

namespace Packt.Ecommerce.DTO.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Model bound to UI to list all the products.
    /// </summary>
    public class ProductListViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        public int Price { get; set; }

        /// <summary>
        /// Gets or sets the image url.
        /// </summary>
        /// <value>
        /// The image url.
        /// </value>
        public List<Uri> ImageUrls { get; set; }

        /// <summary>
        /// Gets or sets the average rating.
        /// </summary>
        /// <value>
        /// The rating.
        /// </value>
        public double AverageRating { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The rating.
        /// </value>
        public int Quantity { get; set; }
    }
}
