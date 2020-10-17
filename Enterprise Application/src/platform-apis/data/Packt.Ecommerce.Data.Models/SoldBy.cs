// "//-----------------------------------------------------------------------".
// <copyright file="SoldBy.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".
namespace Packt.Ecommerce.Data.Models
{
    /// <summary>
    /// Soldby class.
    /// </summary>
    public class SoldBy
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
