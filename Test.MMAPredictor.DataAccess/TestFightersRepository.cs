using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MMAPredictor.DataAccess;
using MMAPredictor.DataAccess.Entities;
using MMAPredictor.DataAccess.Repositories;
using System.Runtime.CompilerServices;

namespace Test.MMAPredictor.DataAccess
{
    public class TestFightersRepository
    {
        private IFightersRepository InitializeFighterRepository(string dbName)
        {
            DbContextOptions<MMAPredictorDbContext> options = new DbContextOptionsBuilder<MMAPredictorDbContext>()
                .UseInMemoryDatabase("dbName").Options;

            MMAPredictorDbContext dbContext = new MMAPredictorDbContext(options);

            IFightersRepository fightersRepo = new FightersRepository(dbContext);
            return fightersRepo;
        }

        [Fact]
        public async Task Test_That_Get_Non_Existent_Fighter_By_Name_Returns_Null()
        {
            var fightersRepo = InitializeFighterRepository("db_test_return_null");

            var fighter = await fightersRepo.GetFighterByNameAsync("Non existant");
            Assert.Null(fighter);
        }

        [Fact]
        public void Test_That_Add_Fighter_Works()
        {
            var fightersRepo = InitializeFighterRepository("db_test_add");
            var fighter = fightersRepo.AddFighterAsync(new Fighter() { Name = "Damian Maya " });

            Assert.True(true);
        }

        [Fact]
        public async Task Test_That_Get_Existent_Fighter_By_Name_Returns_NotNull()
        {
            var fightersRepo = InitializeFighterRepository("db_test_return_not_null");

            string fighterName = "Damian Maya";
            await fightersRepo.AddFighterAsync(new Fighter() { Name = fighterName });
            await fightersRepo.SaveChangesAsync();
            
            var fighter = await fightersRepo.GetFighterByNameAsync(fighterName);
            Assert.NotNull(fighter);
        }

        [Fact]
        public async Task Test_That_Update_Fighter_Works()
        {
            var fightersRepo = InitializeFighterRepository("db_test_update");
            string name = "Damian Maya";
            var fighter = await fightersRepo.AddFighterAsync(new Fighter() { Name = name });
            await fightersRepo.SaveChangesAsync();

            fighter.Height = 180;
            fightersRepo.UpdateFighter(fighter);

            var updatedFighter = await fightersRepo.GetFighterByNameAsync(name);

            Assert.Equal(updatedFighter?.Height, 180);
        }
    }
}
