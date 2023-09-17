using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using System.Text.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace HelloWorld;

public class Function
{

    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public string FunctionHandler(APIGatewayHttpApiV2ProxyRequest request, ILambdaContext context)
    {
        Console.WriteLine(JsonSerializer.Serialize(request));
        string name = string.Empty;
        if (request != null && request.PathParameters != null)
        {
            request.PathParameters.TryGetValue("name", out name);

        }
        if (string.IsNullOrEmpty(name)) name = "John Doe";
        var message = $"Hello {name}, from AWS Lambda";
        return message;
    }
}
