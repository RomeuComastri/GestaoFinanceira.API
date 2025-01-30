using GestaoFinanceira.Repositorio;
using GestaoFinanceira.Repositorio.Contexto;
using Microsoft.EntityFrameworkCore;
using GestaoFinanceira.Aplicacao;
using GestaoFinanceira.Servicos.Interfaces;
using GestaoFinanceira.Servicos.RecuperarSenhaServico;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Converters;


var builder = WebApplication.CreateBuilder(args);

// Adicione as interfaces de aplicações
builder.Services.AddScoped<IUsuarioAplicacao, UsuarioAplicacao>();
builder.Services.AddScoped<ICategoriaAplicacao, CategoriaAplicacao>();
builder.Services.AddScoped<ITransacaoAplicacao, TransacaoAplicacao>();
builder.Services.AddScoped<IRelatorioAplicacao, RelatorioAplicacao>();
builder.Services.AddScoped<IRecuperarSenhaAplicacao, RecuperarSenhaAplicacao>();

//Adicione as interfaces de banco de dados
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<ICategoriaRepositorio, CategoriaRepositorio>();
builder.Services.AddScoped<ITransacaoRepositorio, TransacaoRepositorio>();
builder.Services.AddScoped<IRelatorioRepositorio, RelatorioRepositorio>();
builder.Services.AddScoped<IRecuperarSenhaRepositorio, RecuperarSenhaRepositorio>();

builder.Services.AddScoped<IRecuperarSenhaServico, RecuperarSenhaServico>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000")
        .SetIsOriginAllowedToAllowWildcardSubdomains()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.Converters.Add(new DateOnlyJsonConverter());
    });

//Adicionar o serviço de banco de dados
builder.Services.AddDbContext<GestaoFinanceiraContexto>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Saiba mais sobre a configuração do Swagger/OpenAPI em https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure o pipeline de solicitação HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
