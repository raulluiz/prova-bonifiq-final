using Microsoft.EntityFrameworkCore;
using ProvaPub.Application.Provider;
using ProvaPub.Application.Services;
using ProvaPub.Application.Services.Strategy;
using ProvaPub.Domain.Interfaces;
using ProvaPub.Infrastructure.Context;
using ProvaPub.Infrastructure.Repositorys;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);


// Registrando repositórios
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<INumbersRepository, NumbersRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IDateTimeProvider, SystemDateTimeProvider>();

// Estratégias de pagamento
builder.Services.AddScoped<IPaymentStrategy, PixPaymentStrategy>();
builder.Services.AddScoped<IPaymentStrategy, CreditCardPaymentStrategy>();
builder.Services.AddScoped<IPaymentStrategy, PaypalPaymentStrategy>();

//Utilizando scoped para o DbContext
builder.Services.AddScoped<PaymentService>();
builder.Services.AddScoped<RandomService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddDbContext<TestDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ctx")));
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
