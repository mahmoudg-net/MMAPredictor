using Microsoft.EntityFrameworkCore;
using MMAPredictor.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMAPredictor.DataAccess.Repositories
{
    public class FightersRepository : IFightersRepository
    {
        private MMAPredictorDbContext _dbContext;

        public FightersRepository(MMAPredictorDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Fighter?> GetFighterByNameAsync(string name)
        {
            return await _dbContext.Fighters.FirstOrDefaultAsync(f => f.Name.ToLowerInvariant() == name.ToLowerInvariant());
        }

        public async Task<Fighter> AddFighterAsync(Fighter fighter)
        {
            var res = await _dbContext.Fighters.AddAsync(fighter);
            return res.Entity;
        }

        public void UpdateFighter(Fighter fighter)
        {
            _dbContext.Update(fighter);
        }

        public async Task<bool> SaveChangesAsync()
        {
            var nbModifications = await _dbContext.SaveChangesAsync();
            return nbModifications > 0;
        }
    }
}
