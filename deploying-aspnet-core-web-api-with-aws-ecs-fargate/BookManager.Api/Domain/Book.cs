using Amazon.DynamoDBv2.DataModel;

namespace BookManager.Api.Domain;

[DynamoDBTable("books")]
public class Book
{
    [DynamoDBHashKey("id")]
    public Guid Id { get; set; } = Guid.NewGuid();
    [DynamoDBProperty("title")]
    public string? Title { get; set; }
    [DynamoDBProperty("published_year")]
    public int PublishedYear { get; set; }
    [DynamoDBProperty("pages")]
    public int Pages { get; set; }
    [DynamoDBProperty("summary")]
    public string? Summary { get; set; }
}