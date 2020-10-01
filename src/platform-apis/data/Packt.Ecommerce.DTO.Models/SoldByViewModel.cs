// "//-----------------------------------------------------------------------".
// <copyright file="SoldByViewModel.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

namespace Packt.Ecommerce.DTO.Models
{
    /// <summary>
    /// Soldby class.
    /// </summary>
    public class SoldByViewModel
    {
        /// <summary>
        /// Gets or sets the name of the seller.
        /// </summary>
        /// <value>
        /// The name of the seller.
        /// </value>
        public string SellerName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>
        /// The phone.
        /// </value>
        public string Phone { get; set; }
    }
}