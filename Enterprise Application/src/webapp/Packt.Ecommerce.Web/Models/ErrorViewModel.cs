// "//-----------------------------------------------------------------------".
// <copyright file="ErrorViewModel.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

namespace Packt.Ecommerce.Web.Models
{
    /// <summary>
    /// Error view model.
    /// </summary>
    public record ErrorViewModel
    {
        /// <summary>
        /// Gets or sets the CorrelationId.
        /// </summary>
        /// <value>
        /// The CorrelationId.
        /// </value>
        public string CorrelationId { get; set; }
    }
}
