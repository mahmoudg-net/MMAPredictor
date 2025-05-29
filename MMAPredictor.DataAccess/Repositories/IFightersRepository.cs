using MMAPredictor.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMAPredictor.DataAccess.Repositories
{
    public interface IFightersRepository
    {
        Task<Fighter?> GetFighterByNameAsync(string name);
        Task<Fighter> AddFighterAsync(Fighter fighter);
        void UpdateFighter(Fighter fighter);
        Task<bool> SaveChangesAsync();
    }
}
