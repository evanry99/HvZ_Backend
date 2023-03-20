using HvZ.Data;
using HvZ.Model;
using HvZ.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HvZ
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add DbContext
            builder.Services.AddDbContext<HvZDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            //Swagger documentation
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Human Vs Zombie",
                    Description = "ASP.NET Core Web API for a Human vs Zombie game"
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            builder.Services.AddSignalR();

            builder.Services.AddCors(policyBuilder =>
                policyBuilder.AddDefaultPolicy(policy =>
                    policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin())
            );

            //Automapper service for DTO and mapper
            builder.Services.AddAutoMapper(typeof(Program));

            //Add services to access DB
            builder.Services.AddScoped(typeof(IUserService), typeof(UserService));
            builder.Services.AddScoped(typeof(IGameService), typeof(GameService));
            builder.Services.AddScoped(typeof(IPlayerService), typeof(PlayerService));
            builder.Services.AddScoped(typeof(IKillService), typeof(KillService));
            builder.Services.AddScoped(typeof(IChatService), typeof(ChatService));
            builder.Services.AddScoped(typeof(ISquadCheckInService), typeof(SquadCheckInService));
            builder.Services.AddScoped(typeof(ISquadService), typeof(SquadService));
            builder.Services.AddScoped(typeof(IMissionService), typeof(MissionService));

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {

            }
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseCors();
            app.UseAuthorization();
            app.MapHub<BroadcastHub>("/notify");
            app.MapControllers();

            app.Run();
        }
    }
}