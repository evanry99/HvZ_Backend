﻿using HvZ.Data;
using HvZ.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace HvZ.Services
{
    public class KillService : IKillService
    {

        public readonly HvZDbContext _context;

        public KillService(HvZDbContext context)
        {
            _context = context;
        }

        public async Task<KillDomain> AddKillAsync(KillDomain kill)
        {
            _context.Kills.Add(kill);
            await _context.SaveChangesAsync();
            return kill;
        }

        public async Task DeleteKillAsync(int killId)
        {
            var kill = await _context.Kills.FindAsync(killId);
            _context.Kills.Remove(kill);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<KillDomain>> GetAllKillsAsync()
        {
            return await _context.Kills.ToListAsync();
        }

        public async Task<KillDomain> GetKillAsync(int killId)
        {
            return await _context.Kills.FindAsync(killId);
        }

        public bool KillExists(int killId)
        {
            return _context.Kills.Any(k => k.Id == killId);
        }

        public async Task UpdateKillAsync(KillDomain kill)
        {
            _context.Entry(kill).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}