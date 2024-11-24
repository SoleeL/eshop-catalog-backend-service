using Catalog.API.Endpoints.Brand;
using Catalog.API.Endpoints.Category;
using Catalog.API.Endpoints.Product;
using Catalog.API.Endpoints.Type;
using Catalog.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios de versionado de la API
builder.AddApiVersioning();

// Agregar servicios de infraestructura y persistencia
builder.AddInfrastructureServices();

// Agregar servicio de mediador
builder.AddMediatorServices();

// Agregar servicio de mensajes de error para el usuario
builder.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "API V2");
    });
}

// Controlar excepciones
app.UseExceptionHandler();
app.UseStatusCodePages();

app.UseHttpsRedirection();

app.NewVersionedApi("Brand")
    .MapBrandApiV1()
    .MapBrandApiV2();

app.NewVersionedApi("Category")
    .MapCategoryApiV1()
    .MapCategoryApiV2();

app.NewVersionedApi("Product")
    .MapProductApiV1()
    .MapProductApiV2();

app.NewVersionedApi("Type")
    .MapTypeApiV1()
    .MapTypeApiV2();

app.Run();