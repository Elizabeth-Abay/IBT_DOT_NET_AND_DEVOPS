using  AddisEats.Api.Services.Restaurant;
using  AddisEats.Api.Services.MenuItems;

var builder = WebApplication.CreateBuilder(args);



// this is used for service registry and building ur app

// in the service u add the controller
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// in the dependency injection the controller receives an object of this interface
// register this interface as this service
builder.Services.AddScoped< RestaurantServiceInterface , RestaurantService >();
builder.Services.AddScoped< MenuItemsServiceInterface , MenuItemService>();




// service registration before build using Add
// middleware - after build using use



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMapControllers();

app.Run();

