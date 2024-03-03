using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Products.Api.Dtos;
using Products.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

builder.Services.AddAWSService<IAmazonDynamoDB>();
builder.Services.AddScoped<IDynamoDBContext, DynamoDBContext>();
builder.Services.AddTransient<IProductService, ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/", (CreateProductRequest request, IProductService service) => service.CreateProductAsync(request));
app.MapGet("/{id}", (string id, IProductService service) => service.GetProductByIdAsync(id));
app.MapGet("/", (IProductService service) => service.GetAllProductsAsync());
app.MapDelete("/{id}", (string id, IProductService service) => service.DeleteProductAsync(id));
app.MapPut("/", (UpdateProductRequest request, IProductService service) => service.UpdateProductAsync(request));


app.UseHttpsRedirection();

app.Run();