using BlockChain.BAL;
using BlockChain.BAL.BlockService;
using BlockChain.BAL.Services;
using BlockChain.BAL.TransactionService;
using BlockChain.BAL.UserService;
using BlockChain.BAL.UTXOService;
using BlockChain.DAL.BlockRepository;
using BlockChain.DAL.Entities;
using BlockChain.DAL.TransactionRepository;
using BlockChain.DAL.UserRepository;
using BlockChain.DAL.UTXORepository;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfile));
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BlockChainContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IBlockRepository, BlockRepository>();
builder.Services.AddScoped<IBlockChainService, BlockChainService>();
builder.Services.AddScoped<IUTXORepository, UTXORepository>();
builder.Services.AddScoped<IUtxoService, UTXOService>();
builder.Services.AddControllers();

//builder.Services.AddControllers()
//    .AddJsonOptions(x =>
//        x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve);

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
