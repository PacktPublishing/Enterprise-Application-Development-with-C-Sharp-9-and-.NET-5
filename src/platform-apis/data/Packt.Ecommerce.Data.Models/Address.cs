// "//-----------------------------------------------------------------------".
// <copyright file="Address.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

namespace Packt.Ecommerce.Data.Models
{
    /// <summary>
    /// Address model.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets or sets the address1.
        /// </summary>
        /// <value>
        /// The address1.
        /// </value>
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string Country { get; set; }
    }
}
