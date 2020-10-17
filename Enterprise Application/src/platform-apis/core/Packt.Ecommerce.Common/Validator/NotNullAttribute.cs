// "//-----------------------------------------------------------------------".
// <copyright file="NotNullAttribute.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".
namespace Packt.Ecommerce.Common.Validator
{
    using System;

    /// <summary>
    /// The not null validation attribute.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public sealed class NotNullAttribute : Attribute
    {
    }
}
