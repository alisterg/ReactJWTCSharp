using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LandmarkRemarks.Models;

namespace LandmarkRemarks.Services
{
    public interface IRemarkService
    {
        /// <summary>
        /// Creates a new remark
        /// </summary>
        /// <param name="entity"> The remark to create</param>
        /// <returns>The ID of the remark</returns>
        Guid Create(Remark entity);

        /// <summary>
        /// Gets all remarks
        /// </summary>
        /// <returns></returns>
        Task<List<Remark>> GetAll();

        /// <summary>
        /// Gets a single remark
        /// </summary>
        /// <param name="id">The ID of the remark</param>
        /// <returns></returns>
        Remark GetById(Guid id);
    }
}