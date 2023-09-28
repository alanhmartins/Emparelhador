using Emparelhador.Contexto;
using Emparelhador.Repositories;
using Emparelhador.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionStringSql = builder.Configuration.GetConnectionString("SomeeSql");
builder.Services.AddDbContext<AppContexto>(options =>
{ 
    options.UseSqlServer(connectionStringSql);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    });
builder.Services.AddControllers();  
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());    
builder.Services.AddScoped<IJogadorRepositories,JogadorRepository>();   
builder.Services.AddScoped<IJogadorService,JogadorService>();
builder.Services.AddScoped<IJogadorTorneioRepositories,JogadorTorneioRepository>();
builder.Services.AddScoped<IJogadorService, JogadorService>();
builder.Services.AddScoped<ITorneioRepositories, TorneioRepository>();
builder.Services.AddScoped<ITorneioService, TorneioService>();
builder.Services.AddScoped<IMesaRepositories,MesaRepository>();
builder.Services.AddScoped<IPontosJogadorMesaRepositories,PontosJogadorMesaRepository>();
builder.Services.AddScoped<IConfrontosDiretosRepositories,ConfrontosDiretosRepository>();   

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
