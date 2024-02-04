using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace LambdaAuth;

public class Function
{

    private const string key = "S0M3RAN0MS3CR3T!1!MAG1C!1!12345678";
    public APIGatewayCustomAuthorizerV2IamResponse FunctionHandler(APIGatewayCustomAuthorizerV2Request request)
    {
        Console.WriteLine(JsonSerializer.Serialize(request));
        var authToken = request.Headers["authorization"];
        var claimsPrincipal = GetClaimsPrincipal(authToken);
        var effect = "Deny";
        var principalId = "unauthorized";
        if (claimsPrincipal is not null)
        {
            effect = "Allow";
            principalId = claimsPrincipal?.FindFirst(ClaimTypes.Name)?.Value;
        }
        return new APIGatewayCustomAuthorizerV2IamResponse()
        {
            PrincipalID = principalId,
            PolicyDocument = new APIGatewayCustomAuthorizerPolicy()
            {
                Statement = new List<APIGatewayCustomAuthorizerPolicy.IAMPolicyStatement>
            {
                new APIGatewayCustomAuthorizerPolicy.IAMPolicyStatement()
                {
                    Effect = effect,
                    Resource = new HashSet<string> { request.RouteArn },
                    Action = new HashSet<string> { "execute-api:Invoke" }
                }
            }
            }
        };
    }
    private ClaimsPrincipal GetClaimsPrincipal(string authToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParams = new TokenValidationParameters()
        {
            ValidateLifetime = true,
            ValidateAudience = false,
            ValidateIssuer = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
        };
        try
        {
            return tokenHandler.ValidateToken(authToken, validationParams, out SecurityToken securityToken);
        }
        catch
        {
            return null;
        }
    }
}
