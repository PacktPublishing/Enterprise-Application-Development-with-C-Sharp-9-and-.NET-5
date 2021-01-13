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
    public record ProductListViewModel(string Id, string Name, double Price)
    {
        /// <summary>
        /// Gets or sets the image url.
        /// </summary>
        /// <value>
        /// The image url.
        /// </value>
        public List<Uri> ImageUrls { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The rating.
        /// </value>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the average rating.
        /// </summary>
        /// <value>
        /// The rating.
        /// </value>
        public double AverageRating { get; set; }
    }
}
