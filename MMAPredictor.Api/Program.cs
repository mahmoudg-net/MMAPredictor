using Microsoft.EntityFrameworkCore;
using MMAPredictor.Core.Mapping;
using MMAPredictor.DataAccess;
using MMAPredictor.DataScrapper;
using MMAPredictor.Services;

namespace MMAPredictor.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            //builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<MMAPredictorDbContext>(options => options.UseSqlite("Data Source=MMAPredictor.db"));

            builder.Services.AddScoped<IUFCScrapperService, UFCScrapperService>();
            builder.Services.AddScoped<IFighterService, FighterService>();

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
