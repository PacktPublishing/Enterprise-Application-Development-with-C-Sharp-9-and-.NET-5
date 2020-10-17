// "//-----------------------------------------------------------------------".
// <copyright file="UsersController.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

namespace Packt.Ecommerce.DataAccess.Controllers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Packt.Ecommerce.Data.Models;
    using Packt.Ecommerce.DataStore.Contracts;

    /// <summary>
    /// The user controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// The user repository.
        /// </summary>
        private readonly IUserRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public UsersController(IUserRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <param name="filterCriteria">The filter criteria.</param>
        /// <returns>Users.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync(string filterCriteria = null)
        {
            IEnumerable<User> users;
            if (string.IsNullOrEmpty(filterCriteria))
            {
                users = await this.repository.GetAsync(string.Empty).ConfigureAwait(false);
            }
            else
            {
                users = await this.repository.GetAsync(filterCriteria).ConfigureAwait(false);
            }

            if (users.Any())
            {
                return this.Ok(users);
            }
            else
            {
                return this.NoContent();
            }
        }

        /// <summary>
        /// Gets the user by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="email">The email.</param>
        /// <returns>User.</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserById(string id, [FromQuery][Required] string email)
        {
            User result = await this.repository.GetByIdAsync(id, email).ConfigureAwait(false);
            if (result != null)
            {
                return this.Ok(result);
            }
            else
            {
                return this.NoContent();
            }
        }

        /// <summary>
        /// Creates the user asynchronously.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>HTTP Status code.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] User user)
        {
            if (user == null || user.Etag != null)
            {
                return this.BadRequest();
            }

            var result = await this.repository.AddAsync(user, user.Name).ConfigureAwait(false);
            return this.CreatedAtAction(nameof(this.GetUserById), new { id = result.Resource.Id, name = result.Resource.Name }, result.Resource);
        }

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>HTTP Status code.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateUserAsync([FromBody] User user)
        {
            if (user == null || user.Etag != null)
            {
                return this.BadRequest();
            }

            bool result = await this.repository.ModifyAsync(user, user.Etag, user.Name).ConfigureAwait(false);
            if (result)
            {
                return this.Accepted();
            }
            else
            {
                return this.NoContent();
            }
        }

        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="email">The email.</param>
        /// <returns>HTTP Status code.</returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUserAsync(string id, [FromQuery][Required] string email)
        {
            bool result = await this.repository.RemoveAsync(id, email).ConfigureAwait(false);
            if (result)
            {
                return this.Accepted();
            }
            else
            {
                return this.NoContent();
            }
        }
    }
}
