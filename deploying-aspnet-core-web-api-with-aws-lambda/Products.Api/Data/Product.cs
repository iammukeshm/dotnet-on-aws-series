using Amazon.DynamoDBv2.DataModel;

namespace Products.Api.Data;

[DynamoDBTable("products")]
public class Product
{
    [DynamoDBHashKey("id")]
    public string? Id { get; set; }

    [DynamoDBProperty("name")]
    public string? Name { get; set; }

    [DynamoDBProperty("price")]
    public decimal Price { get; set; }
}
