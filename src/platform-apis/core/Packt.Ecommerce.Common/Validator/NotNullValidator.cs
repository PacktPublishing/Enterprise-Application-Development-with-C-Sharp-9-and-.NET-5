// "//-----------------------------------------------------------------------".
// <copyright file="NotNullValidator.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

namespace Packt.Ecommerce.Common.Validator
{
    using System;

    /// <summary>
    /// Validation extensions.
    /// </summary>
    public static class NotNullValidator
    {
        /// <summary>
        /// Throw argument null exception if value is null.
        /// </summary>
        /// <param name="input">The value to be validated.</param>
        /// <param name="name">The parameter.</param>
        public static void ThrowIfNull([NotNull] object input, string name)
        {
            if (input == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        /// <summary>
        /// Throw argument null exception if value is null or empty.
        /// </summary>
        /// <param name="input">The value to be validated.</param>
        /// <param name="name">The parameter.</param>
        public static void ThrowIfNullOrEmpty([NotNull] string input, string name)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}
