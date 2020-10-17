// "//-----------------------------------------------------------------------".
// <copyright file="ErrorHandlingMiddleware.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

namespace Packt.Ecommerce.Common.Middlewares
{
    using System;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Packt.Ecommerce.Common.Models;
    using Packt.Ecommerce.Common.Options;
    using Packt.Ecommerce.Common.Validator;

    /// <summary>
    /// Error handling middleware.
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        /// <summary>
        /// Reference to the next middleware.
        /// </summary>
        private readonly RequestDelegate requestDelegate;

        /// <summary>
        /// Instance of the logger.
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Indicates if the responses should contain exception details.
        /// </summary>
        private readonly bool includeExceptionDetailsInResponse;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorHandlingMiddleware" /> class.
        /// </summary>
        /// <param name="requestDelegate">Next middleware.</param>
        /// <param name="logger">Logger instance.</param>
        /// <param name="applicationSettings">Options accessor.</param>
        public ErrorHandlingMiddleware(RequestDelegate requestDelegate, ILogger<ErrorHandlingMiddleware> logger, IOptions<ApplicationSettings> applicationSettings)
        {
            NotNullValidator.ThrowIfNull(applicationSettings, nameof(applicationSettings));
            this.requestDelegate = requestDelegate;
            this.logger = logger;
            this.includeExceptionDetailsInResponse = applicationSettings.Value.IncludeExceptionStackInResponse;
        }

        /// <summary>
        /// Method is invoked when middleware is switched.
        /// </summary>
        /// <param name="context">Context of the invocation.</param>
        /// <returns>Task of the invocation.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                if (this.requestDelegate != null)
                {
                    // invoking next middleware.
                    await this.requestDelegate.Invoke(context).ConfigureAwait(false);
                }
            }
            catch (Exception innerException)
            {
                this.logger.LogCritical(Constants.ErrorHandlingMiddlewareErrorCode, innerException, Constants.ErrorMiddlewareLog);

                ExceptionResponse currentException = new ExceptionResponse()
                {
                    ErrorMessage = Constants.ErrorMiddlewareLog,
                    CorrelationIdentifier = System.Diagnostics.Activity.Current?.RootId,
                };

                if (this.includeExceptionDetailsInResponse)
                {
                    currentException.InnerException = $"{innerException.Message} {innerException.StackTrace}";
                }

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize<ExceptionResponse>(currentException)).ConfigureAwait(false);
            }
        }
    }
}
