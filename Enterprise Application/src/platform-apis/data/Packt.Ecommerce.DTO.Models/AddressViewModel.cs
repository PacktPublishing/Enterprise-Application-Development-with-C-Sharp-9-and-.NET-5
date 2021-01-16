// "//-----------------------------------------------------------------------".
// <copyright file="AddressViewModel.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".
namespace Packt.Ecommerce.DTO.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Address view model.
    /// </summary>
    public class AddressViewModel
    {
        /// <summary>
        /// Gets or sets the address1.
        /// </summary>
        /// <value>
        /// The address1.
        /// </value>
        [Required(ErrorMessage = "Address is required")]
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }
    }
}