// "//-----------------------------------------------------------------------".
// <copyright file="User.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".
namespace Packt.Ecommerce.Data.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// User class.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        public int PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public List<Address> Address { get; set; }

        /// <summary>
        /// Gets or sets the etag.
        /// </summary>
        /// <value>
        /// The etag.
        /// </value>
        [JsonProperty("_etag")]
        public string Etag { get; set; }
    }
}
