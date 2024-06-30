// See https://aka.ms/new-console-template for more information
using System;
using System.Text;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

public class MainClass
{
    // __define-ocg__ Ensure the keyword "__define-ocg__" is in at least one comment
    public string GenerateJwtWithFixedClaims(string secret, string issuer, string audience, string sub, string jti, long iat)
    {
        var varOcg = "This is the varOcg variable.";

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secret);
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new System.Security.Claims.ClaimsIdentity(new[]
            {
                new System.Security.Claims.Claim("sub", sub),
                new System.Security.Claims.Claim("jti", jti),
            }),
            Issuer = issuer,
            Audience = audience,
            IssuedAt = DateTimeOffset.UtcNow.UtcDateTime, // Use current UTC time
            Expires = DateTime.MaxValue, // No expiration
            SigningCredentials = signingCredentials
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token);

        return jwt;
    }
}

class Program
{
    static void Main(string[] args)
    {
        MainClass mainClass = new MainClass();
        string jwt = mainClass.GenerateJwtWithFixedClaims("your-secret-key-1234", "your-issuer", "your-audience", "sub-value-1", "jti-value-1", 1626300000);
        Console.WriteLine(jwt);
    }
}
