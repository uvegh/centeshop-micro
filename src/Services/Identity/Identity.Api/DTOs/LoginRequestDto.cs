using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace Identity.Api.DTOs
{
    public record LoginRequestDto(
        string Username,
        string Email,
        [PasswordPropertyText]
        string Password
        
        );
    public record LoginResponseDto(
        string Username,
        string Email

       

        );

    public record SignupRequestDto(
       string Username,
       string Email



       );

    public record SignupResponseDto(
       string Username,
       string Email



       );

}
