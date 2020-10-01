// "//-----------------------------------------------------------------------".
// <copyright file="IEntitySerializer.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

namespace Packt.Ecommerce.Caching.Interfaces
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Serializer interface.
    /// </summary>
    public interface IEntitySerializer
    {
        /// <summary>
        /// Serializes the specified object to byte array asynchronously.
        /// </summary>
        /// <typeparam name="T">Type of return parameter.</typeparam>
        /// <param name="entity">The object to serialize.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// Serialized object in form of byte array.
        /// </returns>
        Task<byte[]> SerializeEntityAsync<T>(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deserializes the specified stream asynchronously.
        /// </summary>
        /// <typeparam name="T">Type of return parameter.</typeparam>
        /// <param name="entity">The stream.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The serialized data.
        /// </returns>
        Task<T> DeserializeEntityAsync<T>(byte[] entity, CancellationToken cancellationToken = default);
    }
}
