using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace TokenGeneration;

public class Function
{

    private const string key = "S0M3RAN0MS3CR3T!1!MAG1C!1!12345678";
    public string FunctionHandler(APIGatewayHttpApiV2ProxyRequest request)
    {
        var user = new
        {
            Name = "Mukesh",
            Email = "hello@codewithmukesh.com"
        };
        var claims = new List<Claim> {
    new(ClaimTypes.Email, user.Email),
    new(ClaimTypes.Name, user.Name)
};
        byte[] secret = Encoding.UTF8.GetBytes(key);
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(secret),
            SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(5),
            signingCredentials: signingCredentials);
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
}
