// "//-----------------------------------------------------------------------".
// <copyright file="RatingViewModel.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

namespace Packt.Ecommerce.DTO.Models
{
    /// <summary>
    /// Ration view model.
    /// </summary>
    public class RatingViewModel
    {
        /// <summary>
        /// Gets or sets the stars.
        /// </summary>
        /// <value>
        /// The stars.
        /// </value>
        public int Stars { get; set; }

        /// <summary>
        /// Gets or sets the percentage.
        /// </summary>
        /// <value>
        /// The percentage.
        /// </value>
        public int Percentage { get; set; }
    }
}