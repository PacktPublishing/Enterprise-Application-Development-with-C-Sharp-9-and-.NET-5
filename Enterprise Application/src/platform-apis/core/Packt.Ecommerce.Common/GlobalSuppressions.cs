// "//-----------------------------------------------------------------------".
// <copyright file="GlobalSuppressions.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".
// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Required for exception handling middleware.", Scope = "member", Target = "~M:Packt.Ecommerce.Common.Middlewares.ErrorHandlingMiddleware.InvokeAsync(Microsoft.AspNetCore.Http.HttpContext)~System.Threading.Tasks.Task")]
