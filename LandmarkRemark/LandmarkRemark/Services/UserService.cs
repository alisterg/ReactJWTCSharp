using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LandmarkRemarks.Models;
using Microsoft.EntityFrameworkCore;

namespace LandmarkRemarks.Services
{
    public class UserService : IUserService
    {
        private LandmarkDbContext _context;

        public UserService(LandmarkDbContext context)
        {
            _context = context;
        }
        
        /// <inheritdoc/>
        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        /// <inheritdoc/>
        public User GetByUsername(string username)
        {
            return _context.Users.FirstOrDefault(user => user.Username == username);
        }
        
        /// <inheritdoc/>
        public User GetById(Guid id)
        {
            return _context.Users.Find(id);
        }
    }
}