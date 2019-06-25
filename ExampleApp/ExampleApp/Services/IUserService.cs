using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExampleApp.Models;

namespace ExampleApp.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        Task<List<User>> GetAll();

        /// <summary>
        /// Gets a user by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns>the User model</returns>
        User GetByUsername(string username);

        /// <summary>
        /// Gets user by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        User GetById(Guid id);
    }
}