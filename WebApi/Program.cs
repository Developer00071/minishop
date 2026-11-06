var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
builder.Services.AddControllers();
builder.Services.AddScoped<Domain.Interfaces.IProductRepository, Infrastructure.Repositories.ProductRepository>();
builder.Services.AddScoped<Domain.Interfaces.ICategoryRepository, Infrastructure.Repositories.CategoryRepository>();
builder.Services.AddScoped<Applicatiion.Services.IProductService, Applicatiion.Services.ProductService>();
builder.Services.AddScoped<Applicatiion.Services.ICategoryService, Applicatiion.Services.CategoryService>();

var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
