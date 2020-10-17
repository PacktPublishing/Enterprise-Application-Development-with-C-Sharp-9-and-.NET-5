// "//-----------------------------------------------------------------------".
// <copyright file="ApplicationSettings.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

namespace Packt.Ecommerce.Common.Options
{
    /// <summary>
    /// General Application settings POCO.
    /// </summary>
    public class ApplicationSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether [include exception stack in response].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [include exception stack in response]; otherwise, <c>false</c>.
        /// </value>
        public bool IncludeExceptionStackInResponse { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [use redis cache].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use redis cache]; otherwise, <c>false</c>.
        /// </value>
        public bool UseRedisCache { get; set; }

        /// <summary>
        /// Gets or sets the data store URL.
        /// </summary>
        /// <value>
        /// The data store URL.
        /// </value>
        public string DataStoreEndpoint { get; set; }

        /// <summary>
        /// Gets or sets the Products Api.
        /// </summary>
        /// <value>
        /// The Products Api.
        /// </value>
        public string ProductsApiEndpoint { get; set; }

        /// <summary>
        /// Gets or sets the Invoice Api.
        /// </summary>
        /// <value>
        /// The Invoice Api.
        /// </value>
        public string InvoiceApiEndpoint { get; set; }

        /// <summary>
        /// Gets or sets the Orders Api.
        /// </summary>
        /// <value>
        /// The Orders Api.
        /// </value>
        public string OrdersApiEndpoint { get; set; }
    }
}
