using Microsoft.EntityFrameworkCore.Sqlite;
using SportsNotes.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SportsNotes.Entities;
using SportsNotes.DTOs;
namespace SportsNotes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<SportsNotesDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.CreateMap<Workout, ProgressRecordDTO>();
                cfg.CreateMap<Exercise, ProgressRecordDTO>();
                cfg.CreateMap<ProgressRecord, ProgressRecordDTO>();
            });
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { 
                Title = "SportsNotes API",
                Version ="v1",
                Description = "API для управления тренировками",
                
            }));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SportsNotes API v1");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
