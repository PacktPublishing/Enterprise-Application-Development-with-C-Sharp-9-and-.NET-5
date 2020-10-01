// "//-----------------------------------------------------------------------".
// <copyright file="Rating.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

namespace Packt.Ecommerce.Data.Models
{
    /// <summary>
    /// Rating class.
    /// </summary>
    public class Rating
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
