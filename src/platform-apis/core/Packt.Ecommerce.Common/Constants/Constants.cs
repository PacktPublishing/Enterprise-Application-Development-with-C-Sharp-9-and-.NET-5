// "//-----------------------------------------------------------------------".
// <copyright file="Constants.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

namespace Packt.Ecommerce.Common
{
    /// <summary>
    /// Class for reusable magic strings.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// The error handling middleware error code.
        /// </summary>
        public static readonly int ErrorHandlingMiddlewareErrorCode = 1001;

        /// <summary>
        /// The error middleware log.
        /// </summary>
        public static readonly string ErrorMiddlewareLog = "Exception captured in error handling middleware";
    }
}
