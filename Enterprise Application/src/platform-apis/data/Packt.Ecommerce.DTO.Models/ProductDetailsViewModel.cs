// "//-----------------------------------------------------------------------".
// <copyright file="ProductDetailsViewModel.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

namespace Packt.Ecommerce.DTO.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    /// <summary>
    /// Product view model that will be bound to UI to show product details.
    /// </summary>
    public class ProductDetailsViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Required]
        public string Id { get; init; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        [Required]
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        [Required]
        [Range(0, 9999)]
        public int Price { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        [Required]
        [Range(0, 999, ErrorMessage = "Large quantity, please reach out to support to process request.")]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the image urls.
        /// </summary>
        /// <value>
        /// The image urls.
        /// </value>
        public List<string> ImageUrls { get; set; }

        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        /// <value>
        /// The rating.
        /// </value>
        public List<RatingViewModel> Rating { get; set; }

        /// <summary>
        /// Gets or sets the format.
        /// </summary>
        /// <value>
        /// The format.
        /// </value>
        public List<string> Format { get; set; }

        /// <summary>
        /// Gets or sets the authors.
        /// </summary>
        /// <value>
        /// The authors.
        /// </value>
        public List<string> Authors { get; set; }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        public List<int> Size { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public List<string> Color { get; set; }

        /// <summary>
        /// Gets or sets the etag.
        /// </summary>
        /// <value>
        /// The etag.
        /// </value>
        public string Etag { get; set; }
    }
}
