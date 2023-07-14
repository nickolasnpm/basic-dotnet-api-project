﻿using Microsoft.EntityFrameworkCore;
using UdemyProject.Data;
using UdemyProject.Models.Domain;

namespace UdemyProject.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly DBContextClass _dbContextClasses;
        public WalkRepository(DBContextClass dBContextClasses)
        {
            _dbContextClasses = dBContextClasses;
        }

        public async Task<IEnumerable<WalkDomain>> GetAllAsync()
        {
            return await _dbContextClasses.WalkTable 
                .Include(x => x.Region) 
                .Include(x => x.Difficulty) 
                .ToListAsync();
        }

        public async Task<WalkDomain> GetAsync(Guid Id)
        {
            return await _dbContextClasses.WalkTable
                .Include(x => x.Region)
                .Include(x => x.Difficulty)
                .FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<WalkDomain> AddAsync(WalkDomain walkdomain)
        {
            walkdomain.Id = Guid.NewGuid();
            await _dbContextClasses.WalkTable.AddAsync(walkdomain);
            await _dbContextClasses.SaveChangesAsync();
            return walkdomain;
        }

        public async Task<WalkDomain> UpdateAsync(Guid Id, WalkDomain walkdomain)
        {
            WalkDomain? existingwalk = await _dbContextClasses.WalkTable.FindAsync(Id);

            if (existingwalk != null) 
            {
                existingwalk.Name = walkdomain.Name;
                existingwalk.Length = walkdomain.Length;
                existingwalk.RegionId = walkdomain.RegionId;
                existingwalk.DifficultyId = walkdomain.DifficultyId;

                await _dbContextClasses.SaveChangesAsync();
            }

            return existingwalk;
        } 

        public async Task<WalkDomain> DeleteAsync(Guid Id)
        {
            WalkDomain? walkDomain = await _dbContextClasses.WalkTable.FirstOrDefaultAsync(x => x.Id == Id);

            if (walkDomain != null)
            {
                _dbContextClasses.WalkTable.Remove(walkDomain);
                await _dbContextClasses.SaveChangesAsync();
            }

            return walkDomain;
        }
    }
}
