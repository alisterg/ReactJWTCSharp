using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExampleApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ExampleApp.Services
{
    public class RemarkService : IRemarkService
    {
        private AppDbContext _context;
        
        public RemarkService(AppDbContext context)
        {
            _context = context;
        }
        
        /// <inheritdoc />
        public Guid Create(Remark entity)
        {
            entity.DateCreated = DateTime.Now;
            
            _context.Remarks.Add(entity);
            _context.SaveChanges();

            return entity.Id;
        }

        /// <inheritdoc />
        public async Task<List<Remark>> GetAll()
        {
            return await _context.Remarks.ToListAsync();
        }

        /// <inheritdoc />
        public Remark GetById(Guid id)
        {
            return _context.Remarks.Find(id);
        }
        
        // not used
        public bool Update(Remark entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();

            return true;
        }

        // not used
        public bool Delete(Guid id)
        {
            Remark item = _context.Remarks.Find(id);

            if(item == null) 
            {
                throw new Exception("Remark doesn't exist");
            }

            _context.Remarks.Remove(item);
            _context.SaveChangesAsync();

            return true;
        }
    }
}